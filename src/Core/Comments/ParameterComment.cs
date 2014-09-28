using System;
using System.Collections.Generic;
using ICSharpCode.NRefactory.CSharp;
using System.Text.RegularExpressions;


namespace NClass.Core
{
	public class ParameterComment
	{
		#region Variables
		#region Private
		public string Name = string.Empty;
		public List<string> Comment = new List<string>();
		private bool isGeneric = false;
		#endregion Private
		#endregion Variables


		#region Constructors
		// To create a blank comment
		public ParameterComment (string _name, List<string> _comment, bool _isGeneric)
		{
			Name = _name;
			if (_comment != null)
				Comment.AddRange(_comment);
			isGeneric = _isGeneric;
		}
		#endregion Constructors


		#region Methods
		#region Public
		// To get the text formatted to documentation style
		public List<string> GetText()
		{
			List<string> OutputComment = Comment;

			if (Comment.Count == 0)
				OutputComment.Add("description");

			return OutputComment;
		}

		#region Tag
		// Le tag de début de documentation 
		public string StartTag()
		{
			if (isGeneric == true)
				return string.Format(" <typeparam name=\"{0}\">", Name);

			return string.Format(" <param name=\"{0}\">", Name);
		}

		// Le tag de fin de documentation 
		public string EndTag()
		{
			if (isGeneric == true)
				return "</typeparam>";

			return "</param>";
		}

		// La description est sur une seule ligne
		public string InOneLine()
		{
			string tmp = Comment[0];
			return string.Format("{0}{1}{2}", StartTag(), tmp, EndTag());
		}
		#endregion Tag

		// Add a parameter remark
		public string AddParamterRef(string text)
		{
			if (isGeneric == true)
				return ReplaceParameterRef(text, "typeparamref");

			return ReplaceParameterRef(text, "paramref");
		}

		// Add comment content
		public void AddComment(string _comment)
		{
			Comment.Add(_comment);
		}
		#endregion Public

		#region Private
		// Pour remplacer la référence au paramètre
		private string ReplaceParameterRef(string text, string tag)
		{
			// Between whitespace
			text = ReplaceParameterRef(text, tag, "(?<param> {0} )|(?<paramdot> *\\.{0} )", " ", " ");
			// Start of line
			text = ReplaceParameterRef(text, tag, "(?<param>^{0} )|(?<paramdot>^\\S\\.{0} )", string.Empty, " ");
			// End of line
			text = ReplaceParameterRef(text, tag, "(?<param> {0}$)|(?<paramdot> {0}.\\S$)", " ", string.Empty);
			// Alone on the line
			return ReplaceParameterRef(text, tag, "(?<param>^{0}$)|(?<paramdot>^{0}.\\S$)", string.Empty, string.Empty);
		}

		// Pour remplacer les tags et gérer le formattage de manière précise
		private string ReplaceParameterRef(string text, string tag, string regex, string start, string end)
		{
			Regex TagRegEx = new Regex(string.Format(regex, Name));
			Match m = TagRegEx.Match(text);
			
			if (m.Success == false)
				return text;

			// Pour le paramètre seul
			text = ReplaceAloneParameterRef(text, m.Groups["param"].Value, tag, start, end);
			// Pour le paramètre avec une classe
			return ReplaceDotParameterRef(text, m.Groups["paramdot"].Value, tag, start, end);
		}
		
		// Pour remplacer la référence au paramètre seul
		private string ReplaceAloneParameterRef(string text, string value, string tag, string start, string end)
		{
			if (string.IsNullOrEmpty(value) == false)
				return text.Replace(value, string.Format("{0}<{1} name='{2}'/>{3}", start, tag, Name, end));
		
			return text;
		}

		// Pour remplacer la référence au paramètre avec une classe
		private string ReplaceDotParameterRef(string text, string value, string tag, string start, string end)
		{
			// Si aucune valeur de trouvé
			if (string.IsNullOrEmpty(value) == true)
				return text;
			
			// On enléve le point et le code avant
			int index = value.IndexOf(Name);
			
			// Ce n'est pas normal
			if (index == 0)
				return text;
			
			// On extrait le code présent devant le paramètre
			string subvalue = value.Substring(0, value.Length - index);
			
			return text.Replace(string.Format("{0}.{1}", subvalue, Name), string.Format("{0}{1}<{2} name='{3}'/>{4}", start, subvalue, tag, Name, end));
		}
		#endregion Private
		#endregion Methods
	}
}

