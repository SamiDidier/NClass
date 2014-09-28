using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ICSharpCode.NRefactory.CSharp;
using System.Linq;


namespace NClass.Core
{
	#region Class Comments
	// To generate a comments
	public class Comments
	{
		#region Variables
		#region Public
		public delegate void OpenCompareUI(Comments newCmt, List<string> oldText, ref List<string> newText);  
		public delegate void OpenNewUI(Comments newCmt, List<string> oldText, ref List<string> newText);  
		#endregion Public

		#region Private
		private event OpenCompareUI DisplayCompareUI; 
		private event OpenNewUI DisplayNewUI; 

		private List<string> comments = new List<string>();
		private string codeName = string.Empty;
		private List<ParameterComment> parameters = new List<ParameterComment>();
		private bool hasReturn = false;
		private bool isProperty = false;
		private List<string> remarksText = new List<string>();
		private List<string> returnText = new List<string>();
		private List<string> valueText = new List<string>();
		private List<string> exampleText = new List<string>();


		// Pour connaitre le tag en cours de parsing
		// Par défault le texte parser va dans les commentaires
		private TagParsingEnum TagParsing = TagParsingEnum.Summary;
		private bool isDefault = false;
		private string codeType = string.Empty;
		// Pour connaitre le tag en cours de modification par l'utilisateur
		private TagParsingEnum TagModified = TagParsingEnum.Summary;
		// Pour connaitre le commentraire de paramètre en cours de modification
		private ParameterComment ParamModified = null;
		#endregion Private
		#endregion Variables


		#region Properties 
		public string CodeName
		{
			set
			{
				codeName = value;
			}
		}

		public string CodeType
		{
			set
			{
				codeType = value;
			}
		}
		#endregion Properties 


		#region Enums
		private enum TagParsingEnum
		{
			Summary = 0,
			Parameters,
			Return,
			Property,
			Remark,
			Example
		}
		#endregion Enums


		#region Constructors
		// To create a blank comment
		public Comments (OpenCompareUI openUI, OpenNewUI newU)
		{
			DisplayCompareUI += openUI;
			DisplayNewUI += newU;
		}

		// To create a Method or an enum comment
		public Comments (OpenCompareUI openUI, OpenNewUI newUI, string _comments, string _codeName, bool _isDefault = false, List<ParameterComment> _parameters = null, bool _hasReturn = false, string _return = "", bool _isProperty = false)
		{
			DisplayCompareUI += openUI;
			DisplayNewUI += newUI;

			comments.Add(_comments);
			codeName = _codeName;
			if (_parameters != null)
				parameters = _parameters;
			hasReturn = _hasReturn;
			if (string.IsNullOrEmpty(_return) == false)
			{
				if (hasReturn == true)
					returnText.Add(_return);
			}
			isProperty = _isProperty;
			isDefault = _isDefault;
		}
		#endregion Constructors


		#region Methods
		#region Public
		// To write the comment
        public void Write(CSharpOutputVisitor formatter)
		{
			// Add Summary tag
			AddTag(formatter, "summary", ref comments);

			// Add all parameters description
			foreach (ParameterComment param in parameters) 
				AddParameterTag(formatter, param);

			AddTag(formatter, "remarks", ref remarksText);

			// Add the return
			if (hasReturn == true)
				AddTag(formatter, "returns", ref returnText);

			// Add the value tag for properties
			if (isProperty == true)
				AddValueTag(formatter);

			// Add the example tag
			AddTag(formatter, "example", ref exampleText);
		}

		// To parse a comment
		public void Parse(string Content, bool isComment = false)
		{
			// Les chaines de caractéres vides ne sont pas gérés
			if (string.IsNullOrEmpty(Content) == true ||
			    string.IsNullOrWhiteSpace(Content) == true)
			{
				comments.Add(string.Empty);
				return;
			}

			string cleanContent = CleanTagCode(Content);

			// Les summary
			string Result = string.Empty;
			// Ce n'est pas la peine d'aller plus loin
			if (StartEndTagAlone("summary", cleanContent) == true)
			{
				TagParsing = TagParsingEnum.Summary;
				return;
			}

			// Est-ce un summary de documentation?
			if (CheckTags("summary", cleanContent, TagParsingEnum.Summary, ref Result) == true)
			{
				if (string.IsNullOrEmpty(Result) == false && 
				    string.IsNullOrWhiteSpace(Result) == false)
					comments.Add(Result); 

				return;
			}

			// Si c'est juste un commentaire 
			if (isComment == true)
			{
				comments.Add(cleanContent);
				// Pas la peine d'aller plus loin
				return;
			}
			
			bool multiline = false;
			string Name = string.Empty;
			// Ce n'est pas la peine d'aller plus loin
			if (StartEndTagAlone("typeparam", Content) == true)
			{
				TagParsing = TagParsingEnum.Parameters;
				return;
			}

			// Les paramètres génériques
			if (CheckTagParameters("typeparam", Content, ref Name, ref Result, ref multiline) == true)
			{
				AddParameter(Name, Result, multiline, true);
				return;
			}

			// Les paramètres
			// Ce n'est pas la peine d'aller plus loin
			if (StartEndTagAlone("param", Content) == true)
			{
				TagParsing = TagParsingEnum.Parameters;
				return;
			}

			if (CheckTagParameters("param", Content, ref Name, ref Result, ref multiline) == true)
			{
				AddParameter(Name, Result, multiline, false);
				return;
			}

			// Les remarques
			// Ce n'est pas la peine d'aller plus loin
			if (StartEndTagAlone("remark", cleanContent) == true)
			{
				TagParsing = TagParsingEnum.Remark;
				return;
			}

			if (CheckTags("remark", cleanContent, TagParsingEnum.Remark, ref Result) == true)
			{
				if (string.IsNullOrEmpty(Result) == false && 
				    string.IsNullOrWhiteSpace(Result) == false)
					remarksText.Add(Result);
				return;
			}

			// Les returns
			// Ce n'est pas la peine d'aller plus loin
			if (StartEndTagAlone("returns", cleanContent) == true)
			{
				hasReturn = true;
				TagParsing = TagParsingEnum.Return;
				return;
			}

			if (CheckTags("returns", cleanContent, TagParsingEnum.Return, ref Result) == true)
			{
				hasReturn = true;
				if (string.IsNullOrEmpty(Result) == false && 
				    string.IsNullOrWhiteSpace(Result) == false)
					returnText.Add(Result); 
				return;
			}

			// Les propriétés
			// Ce n'est pas la peine d'aller plus loin
			if (StartEndTagAlone("value", cleanContent) == true)
			{
				TagParsing = TagParsingEnum.Property;
				return;
			}

			if (CheckTags("value", cleanContent, TagParsingEnum.Property, ref Result) == true)
			{
				isProperty = true;
				if (string.IsNullOrEmpty(Result) == false && 
				    string.IsNullOrWhiteSpace(Result) == false)
					valueText.Add(Result); 
				return;
			}

			// Les examples de code
			if (StartEndTagAlone("example", cleanContent) == true)
			{
				TagParsing = TagParsingEnum.Example;
				return;
			}

			if (CheckTags("example", cleanContent, TagParsingEnum.Example, ref Result) == true)
			{
				if (string.IsNullOrEmpty(Result) == false && 
				    string.IsNullOrWhiteSpace(Result) == false)
					exampleText.Add(Result); 
				return;
			}

			switch (TagParsing)
			{
				case TagParsingEnum.Summary:
					comments.Add(cleanContent); 
					return;
				case TagParsingEnum.Parameters:
					// Pour ajouter une description d'un paramètre multiligne
					int nb = parameters.Count;
					
					if (nb == 0)
						return;
					
					parameters[nb - 1].AddComment(CleanTagCode(Content));
					return;
				case TagParsingEnum.Return:
					returnText.Add(cleanContent); 
					return;
				case TagParsingEnum.Property:
					valueText.Add(cleanContent); 
					return;
				case TagParsingEnum.Remark:
					remarksText.Add(cleanContent); 
					return;
				case TagParsingEnum.Example:
					exampleText.Add(cleanContent); 
					return;
				default:
					// On ne connait cet état!!!
					return;
			}
		}

		// To merge a original documentation with the new one
		public bool Merge(Comments others)
		{
			bool isMerged = false;

			// Est-ce-que les 2 codenames sont égaux?
			if (this.codeName != others.codeName ||
			    string.IsNullOrEmpty(this.codeName) == true ||
			    string.IsNullOrWhiteSpace(this.codeName) == true ||
			    string.IsNullOrEmpty(others.codeName) == true ||
			    string.IsNullOrWhiteSpace(others.codeName) == true)
			{	
				// Ce n'est pas normal!
				return false;
			}

			others.SetDefaultForEmpty();

			// On compare les textes
			// Si texte pareil, ne rien faire
			// Si texte différent, Réaliser une interface pour l'utilisateur pour réaliser le choix	
			//                   , Si texte d'origine ou vierge est la traduction par défaut 
			//                     proposé une interface pour saisir commentaire
			DisplayCompareWin(others.isDefault, others.comments, ref this.comments, TagParsingEnum.Summary);

			// Si pas de return, on vide le texte
			if (this.hasReturn == false &&
			    others.hasReturn == false)
				returnText.Clear();
			else
			{
				// Est-ce-que les 2 commentaires ont des return?
				if (others.hasReturn == true) 
				{
					// On doit comparer le texte des returns
					// Le texte des valeurs de retour
					// Texte pareil, ne rien faire
					// Si texte différent, Réaliser une interface pour l'utilisateur pour réaliser le choix
					//                   , Si texte d'origine ou vierge est la traduction par défaut 
					//                     proposé une interface pour saisir commentaire 
					if (this.returnText.SequenceEqual(others.returnText) == false)
					{
						// Si le texte du précédent return est vierge
						if (returnText.Count == 0)
						{
							// Selon la valeur du commentaire pour le return existant
							if (others.returnText.Count == 0)
								returnText.Add(GetDefaultReturn());
							else
								this.returnText = others.returnText;

							TagModified = TagParsingEnum.Return;
							ParamModified = null;

							DisplayNewWin(this.returnText, ref this.returnText);
							this.hasReturn = true;
						}
					}
					else
						ReturnDefaultText();				
				}
				// Sinon on met une valeur par défaut si nécessaire
				else
					ReturnDefaultText();
			}

			// Si pas de propriété on vide le texte
			if (this.isProperty == false)
				valueText.Clear();
			// Est-ce-que ceux sont 2 propriétés ou pas?
			else if (others.isProperty == this.isProperty)
				// Le texte des valeurs des propriétés
				// Si texte pareil, ne rien faire
				// Si texte différent, Réaliser une interface pour l'utilisateur pour réaliser le choix	
				//                   , Si texte d'origine ou vierge est la traduction par défaut 
				//                     proposé une interface pour saisir commentaire
				DisplayCompareWin(others.isDefault, others.valueText, ref this.valueText, TagParsingEnum.Property, false);

			// Les paramètres à vérifier
			// Si paramètres pareil, ne rien faire.
			// Si paramètre différent, les paramètres bon sont dans this obligatoirement
			//                       , Recopier les commentaires des memes paramètres nom et type de valeur     
			//                       , Les paramètres supprimés, la doc est à supprimer aussi
			if (this.parameters.SequenceEqual(others.parameters) == false)
			{
				// Pour chaque paramètres dans la nouvelle documentation
				foreach(ParameterComment newparam in parameters)		
				{
					// On recherche le meme paramétre dans l'ancienne documentation
					List<ParameterComment> Found = others.parameters.FindAll(delegate(ParameterComment itm)
																         	{
																                return itm.Name == newparam.Name;
																            });

					// Si plusieurs paramètre, il y a un problème
					switch (Found.Count)
					{
						case 0:
							TagModified = TagParsingEnum.Parameters;
							ParamModified = newparam;

							// Paramètre vraiment nouveau!

							DisplayNewWin(newparam.GetText(), ref newparam.Comment);
							continue;
						case 1:
							DisplayCompareWin(others.isDefault, Found[0].Comment, ref newparam.Comment, TagParsingEnum.Parameters, false, newparam);
							/*
							// Si aucun commentaire dans le commentaire précédent
							if (string.IsNullOrEmpty(newparam.Comment) == true ||
						        string.IsNullOrWhiteSpace(newparam.Comment) == true)
									// On merge les commentaires
									newparam.Comment = Found[0].Comment;
							*/
							continue;
						default:
							// ERROR: Impossible to have same name for multiple parameters
							continue;
					}
				}
			}

			// Le texte des remarques
			// Si texte pareil, ne rien faire
			// Si texte différent, Réaliser une interface pour l'utilisateur pour réaliser le choix	
			//                   , Si texte d'origine ou vierge est la traduction par défaut 
			//                     proposé une interface pour saisir commentaire 
			DisplayCompareWin(others.isDefault, others.remarksText, ref this.remarksText, TagParsingEnum.Remark, false);

			// Le texte exemples
			DisplayCompareWin(others.isDefault, others.exampleText, ref this.exampleText, TagParsingEnum.Example, false);

			return isMerged;
		}

		// Pour appliquer le texte sur le bon commentaire
		public void ApplyText(string text)
		{
			switch (TagModified)
			{
				case TagParsingEnum.Summary:
					ListExtension.TextToList(ref comments, text);
					break;
				case TagParsingEnum.Parameters:
					if (ParamModified != null)
						ListExtension.TextToList(ref ParamModified.Comment, text);
					break;
				case TagParsingEnum.Return:
					ListExtension.TextToList(ref returnText, text);
					break;
				case TagParsingEnum.Property:
					ListExtension.TextToList(ref valueText, text);
					break;
				case TagParsingEnum.Remark:
					ListExtension.TextToList(ref remarksText, text);
					break; 
				case TagParsingEnum.Example:
					ListExtension.TextToList(ref exampleText, text);
					break;
			}
		}

		#region Default
		// Pour avoir des valeurs par défaut
		public void SetDefaultForEmpty()
		{
			if (this.comments.Count == 0)
				this.comments.Add(GetDefaultComment(this.codeType, this.codeName));
			
			if (this.returnText.Count == 0 && 
			    this.hasReturn == true &&
			    this.hasReturn == true)
				this.returnText.Add (GetDefaultReturn());
		}

		public static string GetDefaultComment(string defaultText, string name)
		{
			return string.Format(" Enter description for {0} {1}", defaultText, name);
		}

		public static string GetDefaultReturn()
		{
			return " Description of return value";
		}
		#endregion Default
		#endregion Public

		#region Private
		#region Parsing
		// Retourne le commentaire sans les tags
		// Sinon retourne une chaine de caractère vierge
		private bool CheckTags(string tag, string content, TagParsingEnum current, ref string RetValue)
		{
			// Tag de documentation seul
			if (string.Format("<{0}></{0}>", tag) == content.Trim())
			{
				RetValue = string.Empty;
				return true;
			}

			// Tag de documentation encadrant sur la meme ligne
			bool Result = CheckTag(string.Format("(<{0}>)(?<name>.*)(</{0}>)", tag), content, current, ref RetValue);

			if (Result == true)
				return true;

			// Tag de documentation encadrant commencant sur la ligne
			Result = CheckTag(string.Format("(<{0}>)(?<name>.*[^(</{0}>)])", tag), content, current, ref RetValue);

			if (Result == true)
				return true;

			// Tag de documentation encadrant finisant sur la ligne
			Result = CheckTag(string.Format("(?<name>.*[^(<{0}>)])(</{0}>)", tag), content, current, ref RetValue);

			if (Result == true)
				return true;

			return false;
		}
		
		private bool CheckTag(string search, string content, TagParsingEnum current, ref string RetValue)
		{
			Regex TagRegEx = new Regex(search);
			Match m = TagRegEx.Match(content);

			if(m.Success == true)
			{
				TagParsing = current;
				// On extrait entre les tags de début et de fin le contenu
				RetValue = m.Groups["name"].Value;

				if (string.IsNullOrEmpty(RetValue) == true)
					return false;

				return true;
			}

			RetValue = string.Empty;
			return false;
		}

		// Gestion des quotes
		private bool CheckTagParameters(string tag, string content, ref string RetName, ref string RetValue, ref bool multiLine)
		{
			multiLine = false;

			// Les simples quotes
			bool Result = CheckTagParameters(tag, content, "'", ref RetName, ref RetValue, ref multiLine);

			if (Result == true)
				return true;

			// Les doubles quotes
			return CheckTagParameters(tag, content, "\"", ref RetName, ref RetValue, ref multiLine);
		}
		
		// Pour vérifier les 5 cas possibles d'un tag sur une ligne de commentaire
		private bool CheckTagParameters(string tag, string content, string quote, ref string RetName, ref string RetValue, ref bool multiLine)
		{
			// Sans valeur
			// Tag de documentation seul sans valeur
			string format = string.Format("(<{0} name={1})(?<name>.*)({1}></{0}>)", tag, quote);
			bool Result = CheckTag(format, content, TagParsingEnum.Parameters, ref RetName);
			if (Result == true)
			{
				RetValue = string.Empty;
				multiLine = false;
				return true;
			}

			// Avec Valeur
			// Tag de documentation encadrant commencant sur la ligne
			format = string.Format("(<{0} name={1})(?<name>.*)({1}>)(\\s*)$", tag, quote);
			Result = CheckTag(format, content, TagParsingEnum.Parameters, ref RetName);
			
			if (Result == true)
			{
				RetValue = string.Empty; 
				multiLine = true;
				return true;
			}
			
			// Tag de documentation encadrant finisant sur la ligne
			format = string.Format("([^<{0} name={1}.*{1}>])(</{0}>)", tag, quote);
			Result = CheckTag(format, content, TagParsingEnum.Parameters, ref RetName);
			
			if (Result == true)
			{
				RetValue = string.Empty; 
				multiLine = true;
				return true;
			}

			// Avec Valeur
			// Tag de documentation encadrant sur la meme ligne
			format = string.Format("(<{0} name={1})(?<name>.*)({1}>)(?<value>.*)(</{0}>)", tag, quote);
			Result = CheckTagParameter(format, content, ref RetName, ref RetValue);
			
			if (Result == true)
				return true;
			
			// Tag de documentation encadrant commencant sur la ligne
			format = string.Format("(<{0} name={1})(?<name>.*)({1}>)(?<value>.*)(\\s*)$", tag, quote);
			Result = CheckTagParameter(format, content, ref RetName, ref RetValue);
			
			if (Result == true)
			{
				multiLine = true;
				return true;
			}

			// Tag de documentation encadrant finisant sur la ligne
			format = string.Format("(?<value>.*)([^<{0} name={1}.*{1}>])(</{0}>)", tag, quote);
			Result = CheckTagParameter(format, content, ref RetName, ref RetValue);

			if (Result == true)
			{
				multiLine = true;
				return true;
			}

			return false;
		}

		private bool CheckTagParameter(string search, string content, ref string RetName, ref string RetValue)
		{
			Regex TagRegEx = new Regex(search);
			Match m = TagRegEx.Match(content);

			if(m.Success == true)
			{
				TagParsing = TagParsingEnum.Parameters;
				// On extrait entre les tags de début et de fin le contenu
				RetName = m.Groups["name"].Value;
				RetValue = CleanTagCode(m.Groups["value"].Value);

				// Si rien n'a été trouvé
				if (string.IsNullOrEmpty(RetName) == true && 
				    string.IsNullOrEmpty(RetValue) == true)
					return false;

				return true;
			}

			return false;
		}

		// Pour nettoyer le contenu des tags paramètres et code
		private string CleanTagCode(string Content)
		{
			// Les chaines à trouver
			Regex CodeTagRegEx = new Regex("(<c>|</c>|<paramref name='|<typeparamref name='|'/>|<paramref name=\"|<typeparamref name=\"|\"/>)");
			// Suppression des tags codes
			return CodeTagRegEx.Replace(Content, string.Empty);
		}

		// On vérifie que la chaine de caractéres ne contient que le tag de début ou de fin
		private bool StartEndTagAlone(string tag, string content)
		{
			Regex TagRegEx = new Regex(string.Format ("^(<{0}>|</{0}>)$", tag));
			Match m = TagRegEx.Match(content.Trim());

			return m.Success;
		}

		// To add a parameter 
		private void AddParameter(string Name, string Result, bool multiline, bool generic)
		{
			List<string> tmp = null;

			if (string.IsNullOrEmpty(Result) == false)
			{
				// Conversion en liste
				tmp = new List<string>();
				if (multiline == true)
					ListExtension.TextToList(ref tmp, Result);
				else
					tmp.Add(Result);
			}
			
			ParameterComment Temp = new ParameterComment(Name, tmp, generic);
			parameters.Add(Temp);
		}
		#endregion Parsing

		#region Merge
		private void DisplayCompareWin(bool _isDefault, List<string> otherTxt, ref List<string> CurrentTxt, TagParsingEnum tag, bool displayNew = true, ParameterComment paramModified = null)
		{
			TagModified = tag;
			ParamModified = paramModified;

			// On affiche une interface utilisateur
			if (displayNew == true /*&& MainWindow.IsClose == false*/)
			{
				// Si nouveau texte vierge 
				if (IsEmptyList(CurrentTxt) == true)
				{
					// On affiche la boite de dialogue nouveau
					DisplayNewWin (otherTxt, ref CurrentTxt);
					return;
				}
				// Si c'est le texte par défaut pour le précédent commentaire
				else if (_isDefault == true)
					// On garde le texte
					return;

				// On compare l'ancien et le nouveau texte
				if (DisplayCompareUI != null)
					DisplayCompareUI(this, otherTxt, ref CurrentTxt);
			}
			else
			{
				// Pas de texte à fusionner
				if (IsEmptyList(otherTxt) == true && IsEmptyList(CurrentTxt) == true)
					return;
				// On garde le texte précédent
				if (IsEmptyList(otherTxt) == false && IsEmptyList(CurrentTxt) == true)
				{
					CurrentTxt.AddRange(otherTxt);
					return;
				}

				// Pour le cas inverse on ne fait rien!
			}
		}

		private void DisplayNewWin(List<string> oldText, ref List<string> newText)
		{
			if (DisplayNewUI != null /*&& MainWindow.IsClose == false*/)
				DisplayNewUI(this, oldText, ref newText);
			else
			{
				// On applique le texte par defaut
				if (IsEmptyList(newText) == true)
					newText = oldText;
			}
		}

		private void ReturnDefaultText()
		{
			if (returnText.Count == 0)
			{
				TagModified = TagParsingEnum.Return;
				ParamModified = null;

				returnText.Add(GetDefaultReturn());
				DisplayNewWin(this.returnText, ref this.returnText);
				this.hasReturn = true;
			}
		}

		// Pour savoir si la chaine de caractére contient quelque chose!
		private bool IsEmptyList(List<string> lst)
		{
			if (lst.Count == 0)
				return false;

			foreach(string str in lst)
			{
				if (string.IsNullOrEmpty(str) == false)
					return false;

				if (string.IsNullOrWhiteSpace(str) == false)
					return false;
			}

			return true;
		}
		#endregion Merge

		#region Write
		// Add a documentation tag
        private void AddTag(CSharpOutputVisitor formatter, string tag, ref List<string> content, bool alwaysPresent = false)
		{
			// Don't add the tag if not have text
			if (content.Count == 0 && alwaysPresent == false)
				return;

			// Summary tag
			formatter.WriteComment(CommentType.Documentation, string.Format (" <{0}>", tag));

			// La description du bloc
			WriteMultipleDocumentationLine(formatter, ref content);

			// End of the tag
			formatter.WriteComment(CommentType.Documentation, string.Format (" </{0}>", tag));
		}

		// Pour écrire un commentaire multilines
        private void WriteMultipleDocumentationLine(CSharpOutputVisitor formatter, ref List<string> Lines)
		{
			// Pour chaque ligne
			foreach(string str in Lines)
			{
				string docText;

				// Don't add an empty comments
				if (string.IsNullOrEmpty(str) == true ||
				    string.IsNullOrWhiteSpace(str) == true)
					docText = " ";	
				else
					// Add all code references
				 	docText = AddCodeTag(str, codeName);

				// Add all parameter references
				foreach(ParameterComment param in parameters)
					docText = param.AddParamterRef(docText);

				if (docText.Contains('\n') == true)
					// Multiple line description
					formatter.WriteComment(CommentType.MultiLineDocumentation, docText);
				else
					// Single description
					formatter.WriteComment(CommentType.Documentation, docText);
			}
		}

		// Add the code tag into a text to summary tag
		private string AddCodeTag(string text, string codeText)
		{
			if (string.IsNullOrEmpty(codeText) == true)
				return text;

			// Between whitespace
			text = ReplaceCodeTag(text, codeText, "(?<code> {0} )|(?<codedot> {0}.\\S )", " ", " ");
			// Start of line
			text = ReplaceCodeTag(text, codeText, "(?<code>^{0} )|(?<codedot>^{0}.\\S )", string.Empty, " ");
			// End of line
			text = ReplaceCodeTag(text, codeText, "(?<code> {0}$)|(?<codedot> {0}.\\S$)", " ", string.Empty);
			// Alone on the line
			return ReplaceCodeTag(text, codeText, "(?<code>^{0}$)|(?<codedot>^{0}.\\S$)", " ", string.Empty);
		}

		// Pour remplacer les tags et gérer le formattage de manière précise
		private string ReplaceCodeTag(string text, string codeText, string regex, string start, string end)
		{
			Regex TagRegEx = new Regex(string.Format(regex, codeText));
			Match m = TagRegEx.Match(text);
			
			if(m.Success == false)
				return text;

			text = ReplaceCodeTag(text, m.Groups["code"].Value, start, end);
			return ReplaceCodeTag(text, m.Groups["codedot"].Value, start, end);
		}

		// Pour remplacer les tags codes en tenant compte du . et du code vierge
		private string ReplaceCodeTag(string text, string code, string start, string end)
		{
			if (string.IsNullOrEmpty(code) == false)
				return text.Replace(code, string.Format("{0}<c>{1}</c>{2}", start, code.Trim(), end));

			return text;
		}

		// Add a parameter tag
        private void AddParameterTag(CSharpOutputVisitor formatter, ParameterComment name)
		{
			List<string> tmp = name.GetText();

			// Pour un commentaire paramètre multi-ligne
			if (IsMultipleTextLines(ref tmp) == true)
			{
				// Start of the tag
				formatter.WriteComment(CommentType.Documentation, name.StartTag());

				// La description du bloc
				WriteMultipleDocumentationLine(formatter, ref tmp);

				// End of the tag
				formatter.WriteComment(CommentType.Documentation, name.EndTag());
			}
			else
				formatter.WriteComment(CommentType.Documentation, name.InOneLine());
		}

		// Add a value tag
        private void AddValueTag(CSharpOutputVisitor formatter)
		{
			if (valueText.Count == 0)
			{
				formatter.WriteComment(CommentType.Documentation, " <value>To fill.</value>");
				return;
			}

			formatter.WriteComment(CommentType.Documentation, " <value>");
			WriteMultipleDocumentationLine(formatter, ref valueText);
			formatter.WriteComment(CommentType.Documentation, " </value>");
		}

		// To know if the list comment is multiple line
		private bool IsMultipleTextLines(ref List<string> list)
		{
			if (list == null)
				return false;

			// Une seule entrée texte dans la liste
			if (list.Count == 1)
			{
				// Une entrée texte mais avec des retours chariots
				if (list[0].Contains('\n') == true)
					return true;

				return false;

			}

			// On a forcément plusieurs lignes
			return true;
		}
		#endregion Write
		#endregion Private
		#endregion Methods	
	}
	#endregion Class Comments
}

