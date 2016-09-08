using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Xml;
using ICSharpCode.NRefactory.CSharp;

namespace NClass.CodeGenerator
{
    [DefaultProperty("C# formatting Options")]
    public class CSharpFormattingOptionsUI
    {
        public string Name { get; set; }

        public bool IsBuiltIn { get; set; }

        #region Keep formatting

        [Category("Keep formatting"),
         Description("Keep Comments At First Column"),
         DefaultValue(true)]
        public bool KeepCommentsAtFirstColumn { get; set; }

        #endregion

        #region Using Declarations

        [Category("Using Declarations"),
         Description("Using Placement"),
         DefaultValue(UsingPlacement.InsideNamespace)]
        public UsingPlacement UsingPlacement { get; set; }

        #endregion

        public CSharpFormattingOptionsUI Clone()
        {
            return (CSharpFormattingOptionsUI) MemberwiseClone();
        }

        public static CSharpFormattingOptionsUI Load(string selectedFile)
        {
            using (var stream = File.OpenRead(selectedFile))
            {
                return Load(stream);
            }
        }

        public static CSharpFormattingOptionsUI Load(Stream input)
        {
            var result = FormattingOptionsFactoryUI.CreateMono();
            result.Name = "noname";
            using (var reader = new XmlTextReader(input))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.LocalName == "Property")
                        {
                            var info = typeof (CSharpFormattingOptionsUI).GetProperty(reader.GetAttribute("name"));
                            var valString = reader.GetAttribute("value");
                            object value;
                            if (info.PropertyType == typeof (bool))
                            {
                                value = bool.Parse(valString);
                            }
                            else if (info.PropertyType == typeof (int))
                            {
                                value = int.Parse(valString);
                            }
                            else
                            {
                                value = Enum.Parse(info.PropertyType, valString);
                            }
                            info.SetValue(result, value, null);
                        }
                        else if (reader.LocalName == "FormattingProfile")
                        {
                            result.Name = reader.GetAttribute("name");
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == "FormattingProfile")
                    {
                        //Console.WriteLine ("result:" + result.Name);
                        return result;
                    }
                }
            }
            return result;
        }

        public void Save(string fileName)
        {
            using (var writer = new XmlTextWriter(fileName, Encoding.Default))
            {
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 1;
                writer.IndentChar = '\t';
                writer.WriteStartElement("FormattingProfile");
                writer.WriteAttributeString("name", Name);
                foreach (var info in typeof (CSharpFormattingOptionsUI).GetProperties())
                {
                    //if (info.GetCustomAttributes (false).Any (o => o.GetType () == typeof(ItemPropertyAttribute))) {
                    writer.WriteStartElement("Property");
                    writer.WriteAttributeString("name", info.Name);
                    writer.WriteAttributeString("value", info.GetValue(this, null).ToString());
                    writer.WriteEndElement();
                    //}
                }
                writer.WriteEndElement();
            }
        }

        public bool Equals(CSharpFormattingOptionsUI other)
        {
            foreach (var info in typeof (CSharpFormattingOptionsUI).GetProperties())
            {
                //if (info.GetCustomAttributes (false).Any (o => o.GetType () == typeof(ItemPropertyAttribute))) {
                var val = info.GetValue(this, null);
                var otherVal = info.GetValue(other, null);
                if (val == null)
                {
                    if (otherVal == null)
                        continue;
                    return false;
                }
                if (!val.Equals(otherVal))
                {
                    //Console.WriteLine ("!equal");
                    return false;
                }
                //}
            }
            //Console.WriteLine ("== equal");
            return true;
        }

        public static explicit operator CSharpFormattingOptions(CSharpFormattingOptionsUI a)
        {
            var b = FormattingOptionsFactory.CreateEmpty();

            // Indentation
            b.IndentNamespaceBody = a.IndentNamespaceBody;
            b.IndentClassBody = a.IndentClassBody;
            b.IndentInterfaceBody = a.IndentInterfaceBody;
            b.IndentStructBody = a.IndentStructBody;
            b.IndentEnumBody = a.IndentEnumBody;
            b.IndentMethodBody = a.IndentMethodBody;
            b.IndentPropertyBody = a.IndentPropertyBody;
            b.IndentEventBody = a.IndentEventBody;
            b.IndentBlocks = a.IndentBlocks;
            b.IndentSwitchBody = a.IndentSwitchBody;
            b.IndentCaseBody = a.IndentCaseBody;
            b.IndentBreakStatements = a.IndentBreakStatements;
            b.AlignEmbeddedStatements = a.AlignEmbeddedStatements;
            b.AlignElseInIfStatements = a.AlignElseInIfStatements;
            b.AutoPropertyFormatting = a.AutoPropertyFormatting;
            b.SimplePropertyFormatting = a.SimplePropertyFormatting;
            b.EmptyLineFormatting = a.EmptyLineFormatting;
            b.IndentPreprocessorDirectives = a.IndentPreprocessorDirectives;
            b.AlignToMemberReferenceDot = a.AlignToMemberReferenceDot;
            b.IndentBlocksInsideExpressions = a.IndentBlocksInsideExpressions;

            // Braces
            b.NamespaceBraceStyle = a.NamespaceBraceStyle;
            b.ClassBraceStyle = a.ClassBraceStyle;
            b.InterfaceBraceStyle = a.InterfaceBraceStyle;
            b.StructBraceStyle = a.StructBraceStyle;
            b.EnumBraceStyle = a.EnumBraceStyle;
            b.MethodBraceStyle = a.MethodBraceStyle;
            b.AnonymousMethodBraceStyle = a.AnonymousMethodBraceStyle;
            b.ConstructorBraceStyle = a.ConstructorBraceStyle;
            b.DestructorBraceStyle = a.DestructorBraceStyle;
            b.PropertyBraceStyle = a.PropertyBraceStyle;
            b.PropertyGetBraceStyle = a.PropertyGetBraceStyle;
            b.PropertySetBraceStyle = a.PropertySetBraceStyle;
            b.SimpleGetBlockFormatting = a.SimpleGetBlockFormatting;
            b.SimpleSetBlockFormatting = a.SimpleSetBlockFormatting;
            b.EventBraceStyle = a.EventBraceStyle;
            b.EventAddBraceStyle = a.EventAddBraceStyle;
            b.EventRemoveBraceStyle = a.EventRemoveBraceStyle;
            b.AllowEventAddBlockInline = a.AllowEventAddBlockInline;
            b.AllowEventRemoveBlockInline = a.AllowEventRemoveBlockInline;
            b.StatementBraceStyle = a.StatementBraceStyle;
            b.AllowIfBlockInline = a.AllowIfBlockInline;
            b.AllowOneLinedArrayInitialziers = a.AllowOneLinedArrayInitialziers;

            // NewLines
            b.ElseNewLinePlacement = a.ElseNewLinePlacement;
            b.ElseIfNewLinePlacement = a.ElseIfNewLinePlacement;
            b.CatchNewLinePlacement = a.CatchNewLinePlacement;
            b.FinallyNewLinePlacement = a.FinallyNewLinePlacement;
            b.WhileNewLinePlacement = a.WhileNewLinePlacement;
            b.EmbeddedStatementPlacement = a.EmbeddedStatementPlacement;

            // Spaces
            b.SpaceBeforeMethodDeclarationParentheses = a.SpaceBeforeMethodDeclarationParentheses;
            b.SpaceBetweenEmptyMethodDeclarationParentheses = a.SpaceBetweenEmptyMethodDeclarationParentheses;
            b.SpaceBeforeMethodDeclarationParameterComma = a.SpaceBeforeMethodDeclarationParameterComma;
            b.SpaceAfterMethodDeclarationParameterComma = a.SpaceAfterMethodDeclarationParameterComma;
            b.SpaceWithinMethodDeclarationParentheses = a.SpaceWithinMethodDeclarationParentheses;

            // Method calls
            b.SpaceBeforeMethodCallParentheses = a.SpaceBeforeMethodCallParentheses;
            b.SpaceBetweenEmptyMethodCallParentheses = a.SpaceBetweenEmptyMethodCallParentheses;
            b.SpaceBeforeMethodCallParameterComma = a.SpaceBeforeMethodCallParameterComma;
            b.SpaceAfterMethodCallParameterComma = a.SpaceAfterMethodCallParameterComma;
            b.SpaceWithinMethodCallParentheses = a.SpaceWithinMethodCallParentheses;

            // fields
            b.SpaceBeforeFieldDeclarationComma = a.SpaceBeforeFieldDeclarationComma;
            b.SpaceAfterFieldDeclarationComma = a.SpaceAfterFieldDeclarationComma;

            // local variables
            b.SpaceBeforeLocalVariableDeclarationComma = a.SpaceBeforeLocalVariableDeclarationComma;
            b.SpaceAfterLocalVariableDeclarationComma = a.SpaceAfterLocalVariableDeclarationComma;

            // constructors
            b.SpaceBeforeConstructorDeclarationParentheses = a.SpaceBeforeConstructorDeclarationParentheses;
            b.SpaceBetweenEmptyConstructorDeclarationParentheses = a.SpaceBetweenEmptyConstructorDeclarationParentheses;
            b.SpaceBeforeConstructorDeclarationParameterComma = a.SpaceBeforeConstructorDeclarationParameterComma;
            b.SpaceAfterConstructorDeclarationParameterComma = a.SpaceAfterConstructorDeclarationParameterComma;
            b.SpaceWithinConstructorDeclarationParentheses = a.SpaceWithinConstructorDeclarationParentheses;
            b.NewLineBeforeConstructorInitializerColon = a.NewLineBeforeConstructorInitializerColon;
            b.NewLineAfterConstructorInitializerColon = a.NewLineAfterConstructorInitializerColon;

            // indexer
            b.SpaceBeforeIndexerDeclarationBracket = a.SpaceBeforeIndexerDeclarationBracket;
            b.SpaceWithinIndexerDeclarationBracket = a.SpaceWithinIndexerDeclarationBracket;
            b.SpaceBeforeIndexerDeclarationParameterComma = a.SpaceBeforeIndexerDeclarationParameterComma;
            b.SpaceAfterIndexerDeclarationParameterComma = a.SpaceAfterIndexerDeclarationParameterComma;

            // delegates
            b.SpaceBeforeDelegateDeclarationParentheses = a.SpaceBeforeDelegateDeclarationParentheses;
            b.SpaceBetweenEmptyDelegateDeclarationParentheses = a.SpaceBetweenEmptyDelegateDeclarationParentheses;
            b.SpaceBeforeDelegateDeclarationParameterComma = a.SpaceBeforeDelegateDeclarationParameterComma;
            b.SpaceAfterDelegateDeclarationParameterComma = a.SpaceAfterDelegateDeclarationParameterComma;
            b.SpaceWithinDelegateDeclarationParentheses = a.SpaceWithinDelegateDeclarationParentheses;
            b.SpaceBeforeNewParentheses = a.SpaceBeforeNewParentheses;
            b.SpaceBeforeIfParentheses = a.SpaceBeforeIfParentheses;
            b.SpaceBeforeWhileParentheses = a.SpaceBeforeWhileParentheses;
            b.SpaceBeforeForParentheses = a.SpaceBeforeForParentheses;
            b.SpaceBeforeForeachParentheses = a.SpaceBeforeForeachParentheses;
            b.SpaceBeforeCatchParentheses = a.SpaceBeforeCatchParentheses;
            b.SpaceBeforeSwitchParentheses = a.SpaceBeforeSwitchParentheses;
            b.SpaceBeforeLockParentheses = a.SpaceBeforeLockParentheses;
            b.SpaceBeforeUsingParentheses = a.SpaceBeforeUsingParentheses;
            b.SpaceAroundAssignment = a.SpaceAroundAssignment;
            b.SpaceAroundLogicalOperator = a.SpaceAroundLogicalOperator;
            b.SpaceAroundEqualityOperator = a.SpaceAroundEqualityOperator;
            b.SpaceAroundRelationalOperator = a.SpaceAroundRelationalOperator;
            b.SpaceAroundBitwiseOperator = a.SpaceAroundBitwiseOperator;
            b.SpaceAroundAdditiveOperator = a.SpaceAroundAdditiveOperator;
            b.SpaceAroundMultiplicativeOperator = a.SpaceAroundMultiplicativeOperator;
            b.SpaceAroundShiftOperator = a.SpaceAroundShiftOperator;
            b.SpaceAroundNullCoalescingOperator = a.SpaceAroundNullCoalescingOperator;
            b.SpaceAfterUnsafeAddressOfOperator = a.SpaceAfterUnsafeAddressOfOperator;
            b.SpaceAfterUnsafeAsteriskOfOperator = a.SpaceAfterUnsafeAsteriskOfOperator;
            b.SpaceAroundUnsafeArrowOperator = a.SpaceAroundUnsafeArrowOperator;
            b.SpacesWithinParentheses = a.SpacesWithinParentheses;
            b.SpacesWithinIfParentheses = a.SpacesWithinIfParentheses;
            b.SpacesWithinWhileParentheses = a.SpacesWithinWhileParentheses;
            b.SpacesWithinForParentheses = a.SpacesWithinForParentheses;
            b.SpacesWithinForeachParentheses = a.SpacesWithinForeachParentheses;
            b.SpacesWithinCatchParentheses = a.SpacesWithinCatchParentheses;
            b.SpacesWithinSwitchParentheses = a.SpacesWithinSwitchParentheses;
            b.SpacesWithinLockParentheses = a.SpacesWithinLockParentheses;
            b.SpacesWithinUsingParentheses = a.SpacesWithinUsingParentheses;
            b.SpacesWithinCastParentheses = a.SpacesWithinCastParentheses;
            b.SpacesWithinSizeOfParentheses = a.SpacesWithinSizeOfParentheses;
            b.SpaceBeforeSizeOfParentheses = a.SpaceBeforeSizeOfParentheses;
            b.SpacesWithinTypeOfParentheses = a.SpacesWithinTypeOfParentheses;
            b.SpacesWithinNewParentheses = a.SpacesWithinNewParentheses;
            b.SpacesBetweenEmptyNewParentheses = a.SpacesBetweenEmptyNewParentheses;
            b.SpaceBeforeNewParameterComma = a.SpaceBeforeNewParameterComma;
            b.SpaceAfterNewParameterComma = a.SpaceAfterNewParameterComma;
            b.SpaceBeforeTypeOfParentheses = a.SpaceBeforeTypeOfParentheses;
            b.SpacesWithinCheckedExpressionParantheses = a.SpacesWithinCheckedExpressionParantheses;
            b.SpaceBeforeConditionalOperatorCondition = a.SpaceBeforeConditionalOperatorCondition;
            b.SpaceAfterConditionalOperatorCondition = a.SpaceAfterConditionalOperatorCondition;
            b.SpaceBeforeConditionalOperatorSeparator = a.SpaceBeforeConditionalOperatorSeparator;
            b.SpaceAfterConditionalOperatorSeparator = a.SpaceAfterConditionalOperatorSeparator;

            // brackets
            b.SpacesWithinBrackets = a.SpacesWithinBrackets;
            b.SpacesBeforeBrackets = a.SpacesBeforeBrackets;
            b.SpaceBeforeBracketComma = a.SpaceBeforeBracketComma;
            b.SpaceAfterBracketComma = a.SpaceAfterBracketComma;
            b.SpaceBeforeForSemicolon = a.SpaceBeforeForSemicolon;
            b.SpaceAfterForSemicolon = a.SpaceAfterForSemicolon;
            b.SpaceAfterTypecast = a.SpaceAfterTypecast;
            b.SpaceBeforeArrayDeclarationBrackets = a.SpaceBeforeArrayDeclarationBrackets;
            b.SpaceInNamedArgumentAfterDoubleColon = a.SpaceInNamedArgumentAfterDoubleColon;
            b.RemoveEndOfLineWhiteSpace = a.RemoveEndOfLineWhiteSpace;
            b.SpaceBeforeSemicolon = a.SpaceBeforeSemicolon;


            // Blank Lines
            b.MinimumBlankLinesBeforeUsings = a.MinimumBlankLinesBeforeUsings;
            b.MinimumBlankLinesAfterUsings = a.MinimumBlankLinesAfterUsings;
            b.MinimumBlankLinesBeforeFirstDeclaration = a.MinimumBlankLinesBeforeFirstDeclaration;
            b.MinimumBlankLinesBetweenTypes = a.MinimumBlankLinesBetweenTypes;
            b.MinimumBlankLinesBetweenFields = a.MinimumBlankLinesBetweenFields;
            b.MinimumBlankLinesBetweenEventFields = a.MinimumBlankLinesBetweenEventFields;
            b.MinimumBlankLinesBetweenMembers = a.MinimumBlankLinesBetweenMembers;
            b.MinimumBlankLinesAroundRegion = a.MinimumBlankLinesAroundRegion;
            b.MinimumBlankLinesInsideRegion = a.MinimumBlankLinesInsideRegion;

            // formatting
            b.KeepCommentsAtFirstColumn = a.KeepCommentsAtFirstColumn;

            // Wrapping
            b.ArrayInitializerWrapping = a.ArrayInitializerWrapping;
            b.ArrayInitializerBraceStyle = a.ArrayInitializerBraceStyle;
            b.ChainedMethodCallWrapping = a.ChainedMethodCallWrapping;
            b.MethodCallArgumentWrapping = a.MethodCallArgumentWrapping;
            b.NewLineAferMethodCallOpenParentheses = a.NewLineAferMethodCallOpenParentheses;
            b.MethodCallClosingParenthesesOnNewLine = a.MethodCallClosingParenthesesOnNewLine;
            b.IndexerArgumentWrapping = a.IndexerArgumentWrapping;
            b.NewLineAferIndexerOpenBracket = a.NewLineAferIndexerOpenBracket;
            b.IndexerClosingBracketOnNewLine = a.IndexerClosingBracketOnNewLine;
            b.MethodDeclarationParameterWrapping = a.MethodDeclarationParameterWrapping;
            b.NewLineAferMethodDeclarationOpenParentheses = a.NewLineAferMethodDeclarationOpenParentheses;
            b.MethodDeclarationClosingParenthesesOnNewLine = a.MethodDeclarationClosingParenthesesOnNewLine;
            b.IndexerDeclarationParameterWrapping = a.IndexerDeclarationParameterWrapping;
            b.NewLineAferIndexerDeclarationOpenBracket = a.NewLineAferIndexerDeclarationOpenBracket;
            b.IndexerDeclarationClosingBracketOnNewLine = a.IndexerDeclarationClosingBracketOnNewLine;
            b.AlignToFirstIndexerArgument = a.AlignToFirstIndexerArgument;
            b.AlignToFirstIndexerDeclarationParameter = a.AlignToFirstIndexerDeclarationParameter;
            b.AlignToFirstMethodCallArgument = a.AlignToFirstMethodCallArgument;
            b.AlignToFirstMethodDeclarationParameter = a.AlignToFirstMethodDeclarationParameter;
            b.NewLineBeforeNewQueryClause = a.NewLineBeforeNewQueryClause;

            // Using Declarations
            b.UsingPlacement = a.UsingPlacement;

            return b;
        }


        public static explicit operator CSharpFormattingOptionsUI(CSharpFormattingOptions a)
        {
            var b = FormattingOptionsFactoryUI.CreateEmpty();

            // Indentation
            b.IndentNamespaceBody = a.IndentNamespaceBody;
            b.IndentClassBody = a.IndentClassBody;
            b.IndentInterfaceBody = a.IndentInterfaceBody;
            b.IndentStructBody = a.IndentStructBody;
            b.IndentEnumBody = a.IndentEnumBody;
            b.IndentMethodBody = a.IndentMethodBody;
            b.IndentPropertyBody = a.IndentPropertyBody;
            b.IndentEventBody = a.IndentEventBody;
            b.IndentBlocks = a.IndentBlocks;
            b.IndentSwitchBody = a.IndentSwitchBody;
            b.IndentCaseBody = a.IndentCaseBody;
            b.IndentBreakStatements = a.IndentBreakStatements;
            b.AlignEmbeddedStatements = a.AlignEmbeddedStatements;
            b.AlignElseInIfStatements = a.AlignElseInIfStatements;
            b.AutoPropertyFormatting = a.AutoPropertyFormatting;
            b.SimplePropertyFormatting = a.SimplePropertyFormatting;
            b.EmptyLineFormatting = a.EmptyLineFormatting;
            b.IndentPreprocessorDirectives = a.IndentPreprocessorDirectives;
            b.AlignToMemberReferenceDot = a.AlignToMemberReferenceDot;
            b.IndentBlocksInsideExpressions = a.IndentBlocksInsideExpressions;

            // Braces
            b.NamespaceBraceStyle = a.NamespaceBraceStyle;
            b.ClassBraceStyle = a.ClassBraceStyle;
            b.InterfaceBraceStyle = a.InterfaceBraceStyle;
            b.StructBraceStyle = a.StructBraceStyle;
            b.EnumBraceStyle = a.EnumBraceStyle;
            b.MethodBraceStyle = a.MethodBraceStyle;
            b.AnonymousMethodBraceStyle = a.AnonymousMethodBraceStyle;
            b.ConstructorBraceStyle = a.ConstructorBraceStyle;
            b.DestructorBraceStyle = a.DestructorBraceStyle;
            b.PropertyBraceStyle = a.PropertyBraceStyle;
            b.PropertyGetBraceStyle = a.PropertyGetBraceStyle;
            b.PropertySetBraceStyle = a.PropertySetBraceStyle;
            b.SimpleGetBlockFormatting = a.SimpleGetBlockFormatting;
            b.SimpleSetBlockFormatting = a.SimpleSetBlockFormatting;
            b.EventBraceStyle = a.EventBraceStyle;
            b.EventAddBraceStyle = a.EventAddBraceStyle;
            b.EventRemoveBraceStyle = a.EventRemoveBraceStyle;
            b.AllowEventAddBlockInline = a.AllowEventAddBlockInline;
            b.AllowEventRemoveBlockInline = a.AllowEventRemoveBlockInline;
            b.StatementBraceStyle = a.StatementBraceStyle;
            b.AllowIfBlockInline = a.AllowIfBlockInline;
            b.AllowOneLinedArrayInitialziers = a.AllowOneLinedArrayInitialziers;

            // NewLines
            b.ElseNewLinePlacement = a.ElseNewLinePlacement;
            b.ElseIfNewLinePlacement = a.ElseIfNewLinePlacement;
            b.CatchNewLinePlacement = a.CatchNewLinePlacement;
            b.FinallyNewLinePlacement = a.FinallyNewLinePlacement;
            b.WhileNewLinePlacement = a.WhileNewLinePlacement;
            b.EmbeddedStatementPlacement = a.EmbeddedStatementPlacement;

            // Spaces
            b.SpaceBeforeMethodDeclarationParentheses = a.SpaceBeforeMethodDeclarationParentheses;
            b.SpaceBetweenEmptyMethodDeclarationParentheses = a.SpaceBetweenEmptyMethodDeclarationParentheses;
            b.SpaceBeforeMethodDeclarationParameterComma = a.SpaceBeforeMethodDeclarationParameterComma;
            b.SpaceAfterMethodDeclarationParameterComma = a.SpaceAfterMethodDeclarationParameterComma;
            b.SpaceWithinMethodDeclarationParentheses = a.SpaceWithinMethodDeclarationParentheses;

            // Method calls
            b.SpaceBeforeMethodCallParentheses = a.SpaceBeforeMethodCallParentheses;
            b.SpaceBetweenEmptyMethodCallParentheses = a.SpaceBetweenEmptyMethodCallParentheses;
            b.SpaceBeforeMethodCallParameterComma = a.SpaceBeforeMethodCallParameterComma;
            b.SpaceAfterMethodCallParameterComma = a.SpaceAfterMethodCallParameterComma;
            b.SpaceWithinMethodCallParentheses = a.SpaceWithinMethodCallParentheses;

            // fields
            b.SpaceBeforeFieldDeclarationComma = a.SpaceBeforeFieldDeclarationComma;
            b.SpaceAfterFieldDeclarationComma = a.SpaceAfterFieldDeclarationComma;

            // local variables
            b.SpaceBeforeLocalVariableDeclarationComma = a.SpaceBeforeLocalVariableDeclarationComma;
            b.SpaceAfterLocalVariableDeclarationComma = a.SpaceAfterLocalVariableDeclarationComma;

            // constructors
            b.SpaceBeforeConstructorDeclarationParentheses = a.SpaceBeforeConstructorDeclarationParentheses;
            b.SpaceBetweenEmptyConstructorDeclarationParentheses = a.SpaceBetweenEmptyConstructorDeclarationParentheses;
            b.SpaceBeforeConstructorDeclarationParameterComma = a.SpaceBeforeConstructorDeclarationParameterComma;
            b.SpaceAfterConstructorDeclarationParameterComma = a.SpaceAfterConstructorDeclarationParameterComma;
            b.SpaceWithinConstructorDeclarationParentheses = a.SpaceWithinConstructorDeclarationParentheses;
            b.NewLineBeforeConstructorInitializerColon = a.NewLineBeforeConstructorInitializerColon;
            b.NewLineAfterConstructorInitializerColon = a.NewLineAfterConstructorInitializerColon;

            // indexer
            b.SpaceBeforeIndexerDeclarationBracket = a.SpaceBeforeIndexerDeclarationBracket;
            b.SpaceWithinIndexerDeclarationBracket = a.SpaceWithinIndexerDeclarationBracket;
            b.SpaceBeforeIndexerDeclarationParameterComma = a.SpaceBeforeIndexerDeclarationParameterComma;
            b.SpaceAfterIndexerDeclarationParameterComma = a.SpaceAfterIndexerDeclarationParameterComma;

            // delegates
            b.SpaceBeforeDelegateDeclarationParentheses = a.SpaceBeforeDelegateDeclarationParentheses;
            b.SpaceBetweenEmptyDelegateDeclarationParentheses = a.SpaceBetweenEmptyDelegateDeclarationParentheses;
            b.SpaceBeforeDelegateDeclarationParameterComma = a.SpaceBeforeDelegateDeclarationParameterComma;
            b.SpaceAfterDelegateDeclarationParameterComma = a.SpaceAfterDelegateDeclarationParameterComma;
            b.SpaceWithinDelegateDeclarationParentheses = a.SpaceWithinDelegateDeclarationParentheses;
            b.SpaceBeforeNewParentheses = a.SpaceBeforeNewParentheses;
            b.SpaceBeforeIfParentheses = a.SpaceBeforeIfParentheses;
            b.SpaceBeforeWhileParentheses = a.SpaceBeforeWhileParentheses;
            b.SpaceBeforeForParentheses = a.SpaceBeforeForParentheses;
            b.SpaceBeforeForeachParentheses = a.SpaceBeforeForeachParentheses;
            b.SpaceBeforeCatchParentheses = a.SpaceBeforeCatchParentheses;
            b.SpaceBeforeSwitchParentheses = a.SpaceBeforeSwitchParentheses;
            b.SpaceBeforeLockParentheses = a.SpaceBeforeLockParentheses;
            b.SpaceBeforeUsingParentheses = a.SpaceBeforeUsingParentheses;
            b.SpaceAroundAssignment = a.SpaceAroundAssignment;
            b.SpaceAroundLogicalOperator = a.SpaceAroundLogicalOperator;
            b.SpaceAroundEqualityOperator = a.SpaceAroundEqualityOperator;
            b.SpaceAroundRelationalOperator = a.SpaceAroundRelationalOperator;
            b.SpaceAroundBitwiseOperator = a.SpaceAroundBitwiseOperator;
            b.SpaceAroundAdditiveOperator = a.SpaceAroundAdditiveOperator;
            b.SpaceAroundMultiplicativeOperator = a.SpaceAroundMultiplicativeOperator;
            b.SpaceAroundShiftOperator = a.SpaceAroundShiftOperator;
            b.SpaceAroundNullCoalescingOperator = a.SpaceAroundNullCoalescingOperator;
            b.SpaceAfterUnsafeAddressOfOperator = a.SpaceAfterUnsafeAddressOfOperator;
            b.SpaceAfterUnsafeAsteriskOfOperator = a.SpaceAfterUnsafeAsteriskOfOperator;
            b.SpaceAroundUnsafeArrowOperator = a.SpaceAroundUnsafeArrowOperator;
            b.SpacesWithinParentheses = a.SpacesWithinParentheses;
            b.SpacesWithinIfParentheses = a.SpacesWithinIfParentheses;
            b.SpacesWithinWhileParentheses = a.SpacesWithinWhileParentheses;
            b.SpacesWithinForParentheses = a.SpacesWithinForParentheses;
            b.SpacesWithinForeachParentheses = a.SpacesWithinForeachParentheses;
            b.SpacesWithinCatchParentheses = a.SpacesWithinCatchParentheses;
            b.SpacesWithinSwitchParentheses = a.SpacesWithinSwitchParentheses;
            b.SpacesWithinLockParentheses = a.SpacesWithinLockParentheses;
            b.SpacesWithinUsingParentheses = a.SpacesWithinUsingParentheses;
            b.SpacesWithinCastParentheses = a.SpacesWithinCastParentheses;
            b.SpacesWithinSizeOfParentheses = a.SpacesWithinSizeOfParentheses;
            b.SpaceBeforeSizeOfParentheses = a.SpaceBeforeSizeOfParentheses;
            b.SpacesWithinTypeOfParentheses = a.SpacesWithinTypeOfParentheses;
            b.SpacesWithinNewParentheses = a.SpacesWithinNewParentheses;
            b.SpacesBetweenEmptyNewParentheses = a.SpacesBetweenEmptyNewParentheses;
            b.SpaceBeforeNewParameterComma = a.SpaceBeforeNewParameterComma;
            b.SpaceAfterNewParameterComma = a.SpaceAfterNewParameterComma;
            b.SpaceBeforeTypeOfParentheses = a.SpaceBeforeTypeOfParentheses;
            b.SpacesWithinCheckedExpressionParantheses = a.SpacesWithinCheckedExpressionParantheses;
            b.SpaceBeforeConditionalOperatorCondition = a.SpaceBeforeConditionalOperatorCondition;
            b.SpaceAfterConditionalOperatorCondition = a.SpaceAfterConditionalOperatorCondition;
            b.SpaceBeforeConditionalOperatorSeparator = a.SpaceBeforeConditionalOperatorSeparator;
            b.SpaceAfterConditionalOperatorSeparator = a.SpaceAfterConditionalOperatorSeparator;

            // brackets
            b.SpacesWithinBrackets = a.SpacesWithinBrackets;
            b.SpacesBeforeBrackets = a.SpacesBeforeBrackets;
            b.SpaceBeforeBracketComma = a.SpaceBeforeBracketComma;
            b.SpaceAfterBracketComma = a.SpaceAfterBracketComma;
            b.SpaceBeforeForSemicolon = a.SpaceBeforeForSemicolon;
            b.SpaceAfterForSemicolon = a.SpaceAfterForSemicolon;
            b.SpaceAfterTypecast = a.SpaceAfterTypecast;
            b.SpaceBeforeArrayDeclarationBrackets = a.SpaceBeforeArrayDeclarationBrackets;
            b.SpaceInNamedArgumentAfterDoubleColon = a.SpaceInNamedArgumentAfterDoubleColon;
            b.RemoveEndOfLineWhiteSpace = a.RemoveEndOfLineWhiteSpace;
            b.SpaceBeforeSemicolon = a.SpaceBeforeSemicolon;


            // Blank Lines
            b.MinimumBlankLinesBeforeUsings = a.MinimumBlankLinesBeforeUsings;
            b.MinimumBlankLinesAfterUsings = a.MinimumBlankLinesAfterUsings;
            b.MinimumBlankLinesBeforeFirstDeclaration = a.MinimumBlankLinesBeforeFirstDeclaration;
            b.MinimumBlankLinesBetweenTypes = a.MinimumBlankLinesBetweenTypes;
            b.MinimumBlankLinesBetweenFields = a.MinimumBlankLinesBetweenFields;
            b.MinimumBlankLinesBetweenEventFields = a.MinimumBlankLinesBetweenEventFields;
            b.MinimumBlankLinesBetweenMembers = a.MinimumBlankLinesBetweenMembers;
            b.MinimumBlankLinesAroundRegion = a.MinimumBlankLinesAroundRegion;
            b.MinimumBlankLinesInsideRegion = a.MinimumBlankLinesInsideRegion;

            // formatting
            b.KeepCommentsAtFirstColumn = a.KeepCommentsAtFirstColumn;

            // Wrapping
            b.ArrayInitializerWrapping = a.ArrayInitializerWrapping;
            b.ArrayInitializerBraceStyle = a.ArrayInitializerBraceStyle;
            b.ChainedMethodCallWrapping = a.ChainedMethodCallWrapping;
            b.MethodCallArgumentWrapping = a.MethodCallArgumentWrapping;
            b.NewLineAferMethodCallOpenParentheses = a.NewLineAferMethodCallOpenParentheses;
            b.MethodCallClosingParenthesesOnNewLine = a.MethodCallClosingParenthesesOnNewLine;
            b.IndexerArgumentWrapping = a.IndexerArgumentWrapping;
            b.NewLineAferIndexerOpenBracket = a.NewLineAferIndexerOpenBracket;
            b.IndexerClosingBracketOnNewLine = a.IndexerClosingBracketOnNewLine;
            b.MethodDeclarationParameterWrapping = a.MethodDeclarationParameterWrapping;
            b.NewLineAferMethodDeclarationOpenParentheses = a.NewLineAferMethodDeclarationOpenParentheses;
            b.MethodDeclarationClosingParenthesesOnNewLine = a.MethodDeclarationClosingParenthesesOnNewLine;
            b.IndexerDeclarationParameterWrapping = a.IndexerDeclarationParameterWrapping;
            b.NewLineAferIndexerDeclarationOpenBracket = a.NewLineAferIndexerDeclarationOpenBracket;
            b.IndexerDeclarationClosingBracketOnNewLine = a.IndexerDeclarationClosingBracketOnNewLine;
            b.AlignToFirstIndexerArgument = a.AlignToFirstIndexerArgument;
            b.AlignToFirstIndexerDeclarationParameter = a.AlignToFirstIndexerDeclarationParameter;
            b.AlignToFirstMethodCallArgument = a.AlignToFirstMethodCallArgument;
            b.AlignToFirstMethodDeclarationParameter = a.AlignToFirstMethodDeclarationParameter;
            b.NewLineBeforeNewQueryClause = a.NewLineBeforeNewQueryClause;

            // Using Declarations
            b.UsingPlacement = a.UsingPlacement;

            return b;
        }

        #region Indentation

        [Category("Indent"),
         Description("Indent Namespace Body"),
         DefaultValue(true)]
        public bool IndentNamespaceBody { // tested
            get; set; }

        [Category("Indent"),
         Description("Indent Class Body"),
         DefaultValue(true)]
        public bool IndentClassBody { // tested
            get; set; }

        [Category("Indent"),
         Description("Indent Interface Body"),
         DefaultValue(true)]
        public bool IndentInterfaceBody { // tested
            get; set; }

        [Category("Indent"),
         Description("Indent Struct Body"),
         DefaultValue(true)]
        public bool IndentStructBody { // tested
            get; set; }

        [Category("Indent"),
         Description("Indent Enum Body"),
         DefaultValue(true)]
        public bool IndentEnumBody { // tested
            get; set; }

        [Category("Indent"),
         Description("Indent Method Body"),
         DefaultValue(true)]
        public bool IndentMethodBody { // tested
            get; set; }

        [Category("Indent"),
         Description("Indent Property Body"),
         DefaultValue(true)]
        public bool IndentPropertyBody { // tested
            get; set; }

        [Category("Indent"),
         Description("Indent Event Body"),
         DefaultValue(true)]
        public bool IndentEventBody { // tested
            get; set; }

        [Category("Indent"),
         Description("Indent Blocks"),
         DefaultValue(true)]
        public bool IndentBlocks { // tested
            get; set; }

        [Category("Indent"),
         Description("Indent Switch Body"),
         DefaultValue(true)]
        public bool IndentSwitchBody { // tested
            get; set; }

        [Category("Indent"),
         Description("Indent Case Body"),
         DefaultValue(true)]
        public bool IndentCaseBody { // tested
            get; set; }

        [Category("Indent"),
         Description("Indent Break Statements"),
         DefaultValue(true)]
        public bool IndentBreakStatements { // tested
            get; set; }

        [Category("Indent"),
         Description("Align Embedded Statements"),
         DefaultValue(true)]
        public bool AlignEmbeddedStatements { // tested
            get; set; }

        [Category("Indent"),
         Description("Align Else In If Statements"),
         DefaultValue(true)]
        public bool AlignElseInIfStatements { get; set; }

        [Category("Indent"),
         Description("Auto Property Formatting"),
         DefaultValue(true)]
        public PropertyFormatting AutoPropertyFormatting { // tested
            get; set; }

        [Category("Indent"),
         Description("Simple Property Formatting"),
         DefaultValue(true)]
        public PropertyFormatting SimplePropertyFormatting { // tested
            get; set; }

        [Category("Indent"),
         Description("Empty Line Formatting"),
         DefaultValue(true)]
        public EmptyLineFormatting EmptyLineFormatting { get; set; }

        [Category("Indent"),
         Description("Indent Preprocessor Directives"),
         DefaultValue(true)]
        public bool IndentPreprocessorDirectives { // tested
            get; set; }

        [Category("Indent"),
         Description("Align To Member Reference Dot"),
         DefaultValue(true)]
        public bool AlignToMemberReferenceDot { // TODO!
            get; set; }

        [Category("Indent"),
         Description("Indent Blocks Inside Expressions"),
         DefaultValue(true)]
        public bool IndentBlocksInsideExpressions { get; set; }

        #endregion

        #region Braces

        [Category("Braces"),
         Description("Namespace Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle NamespaceBraceStyle { // tested
            get; set; }

        [Category("Braces"),
         Description("Class Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle ClassBraceStyle { // tested
            get; set; }

        [Category("Braces"),
         Description("Interface Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle InterfaceBraceStyle { // tested
            get; set; }

        [Category("Braces"),
         Description("Struct Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle StructBraceStyle { // tested
            get; set; }

        [Category("Braces"),
         Description("Enum Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle EnumBraceStyle { // tested
            get; set; }

        [Category("Braces"),
         Description("Method Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle MethodBraceStyle { // tested
            get; set; }

        [Category("Braces"),
         Description("Anonymous Method Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle AnonymousMethodBraceStyle { get; set; }

        [Category("Braces"),
         Description("Constructor Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle ConstructorBraceStyle { // tested
            get; set; }

        [Category("Braces"),
         Description("Destructor Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle DestructorBraceStyle { // tested
            get; set; }

        [Category("Braces"),
         Description("Property Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle PropertyBraceStyle { // tested
            get; set; }

        [Category("Braces"),
         Description("Property Get Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle PropertyGetBraceStyle { // tested
            get; set; }

        [Category("Braces"),
         Description("Property Set Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle PropertySetBraceStyle { // tested
            get; set; }

        [Category("Braces"),
         Description("Simple Get Block Formatting"),
         DefaultValue(BraceStyle.DoNotChange)]
        public PropertyFormatting SimpleGetBlockFormatting { // tested
            get; set; }

        [Category("Braces"),
         Description("Simple Set Block Formatting"),
         DefaultValue(BraceStyle.DoNotChange)]
        public PropertyFormatting SimpleSetBlockFormatting { // tested
            get; set; }

        [Category("Braces"),
         Description("Event Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle EventBraceStyle { // tested
            get; set; }

        [Category("Braces"),
         Description("Event Add Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle EventAddBraceStyle { // tested
            get; set; }

        [Category("Braces"),
         Description("Event Remove Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle EventRemoveBraceStyle { // tested
            get; set; }

        [Category("Braces"),
         Description("Allow Event Add Block In line"),
         DefaultValue(true)]
        public bool AllowEventAddBlockInline { // tested
            get; set; }

        [Category("Braces"),
         Description("Allow Event Remove Block In line"),
         DefaultValue(true)]
        public bool AllowEventRemoveBlockInline { // tested
            get; set; }

        [Category("Braces"),
         Description("Statement Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle StatementBraceStyle { // tested
            get; set; }

        [Category("Braces"),
         Description("Allow If Block In line"),
         DefaultValue(true)]
        public bool AllowIfBlockInline { get; set; }

        [Category("Braces"),
         Description("Allow One Lined Array Initialziers"),
         DefaultValue(true)]
        public bool AllowOneLinedArrayInitialziers { get; set; } = true;

        #endregion

        #region NewLines

        [Category("NewLines"),
         Description("Else New Line Placement"),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement ElseNewLinePlacement { // tested
            get; set; }

        [Category("NewLines"),
         Description(""),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement ElseIfNewLinePlacement { // tested
            get; set; }

        [Category("NewLines"),
         Description("Catch New Line Placement"),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement CatchNewLinePlacement { // tested
            get; set; }

        [Category("NewLines"),
         Description("Finally New Line Placement"),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement FinallyNewLinePlacement { // tested
            get; set; }

        [Category("NewLines"),
         Description("While New Line Placement"),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement WhileNewLinePlacement { // tested
            get; set; }

        [Category("NewLines"),
         Description("Embedded Statement Placement"),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement EmbeddedStatementPlacement { get; set; } = NewLinePlacement.NewLine;

        #endregion

        #region Spaces

        // Methods
        [Category("Spaces - Method"),
         Description("Space Before Method Declaration Parentheses"),
         DefaultValue(true)]
        public bool SpaceBeforeMethodDeclarationParentheses { // tested
            get; set; }

        [Category("Spaces - Method"),
         Description("Space Between Empty Method Declaration Parentheses"),
         DefaultValue(true)]
        public bool SpaceBetweenEmptyMethodDeclarationParentheses { get; set; }

        [Category("Spaces - Method"),
         Description("Space Before Method Declaration Parameter Comma"),
         DefaultValue(true)]
        public bool SpaceBeforeMethodDeclarationParameterComma { // tested
            get; set; }

        [Category("Spaces - Method"),
         Description("Space After Method Declaration Parameter Comma"),
         DefaultValue(true)]
        public bool SpaceAfterMethodDeclarationParameterComma { // tested
            get; set; }

        [Category("Spaces - Method"),
         Description("Space Within Method Declaration Parentheses"),
         DefaultValue(true)]
        public bool SpaceWithinMethodDeclarationParentheses { // tested
            get; set; }

        // Method calls
        [Category("Spaces - Method calls"),
         Description("Space Before Method Call Parentheses"),
         DefaultValue(true)]
        public bool SpaceBeforeMethodCallParentheses { // tested
            get; set; }

        [Category("Spaces - Method calls"),
         Description("Space Between Empty Method Call Parentheses"),
         DefaultValue(true)]
        public bool SpaceBetweenEmptyMethodCallParentheses { // tested
            get; set; }

        [Category("Spaces - Method calls"),
         Description("Space Before Method Call Parameter Comma"),
         DefaultValue(true)]
        public bool SpaceBeforeMethodCallParameterComma { // tested
            get; set; }

        [Category("Spaces - Method calls"),
         Description("Space After Method Call Parameter Comma"),
         DefaultValue(true)]
        public bool SpaceAfterMethodCallParameterComma { // tested
            get; set; }

        [Category("Spaces - Method calls"),
         Description("Space Withi nMethod Call Parentheses"),
         DefaultValue(true)]
        public bool SpaceWithinMethodCallParentheses { // tested
            get; set; }

        // fields
        [Category("Spaces - Fields"),
         Description("Space Before Field Declaration Comma"),
         DefaultValue(true)]
        public bool SpaceBeforeFieldDeclarationComma { // tested
            get; set; }

        [Category("Spaces - Fields"),
         Description("Space After Field Declaration Comma"),
         DefaultValue(true)]
        public bool SpaceAfterFieldDeclarationComma { // tested
            get; set; }

        // local variables
        [Category("Spaces - Local variables"),
         Description("Space Before Local Variable Declaration Comma"),
         DefaultValue(true)]
        public bool SpaceBeforeLocalVariableDeclarationComma { // tested
            get; set; }

        [Category("Spaces - Local variables"),
         Description("Space After Local Variable Declaration Comma"),
         DefaultValue(true)]
        public bool SpaceAfterLocalVariableDeclarationComma { // tested
            get; set; }

        // constructors
        [Category("Spaces - Constructors"),
         Description("Space Before Constructor Declaration Parentheses"),
         DefaultValue(true)]
        public bool SpaceBeforeConstructorDeclarationParentheses { // tested
            get; set; }

        [Category("Spaces - Constructors"),
         Description("Space Between Empty Constructor Declaration Parentheses"),
         DefaultValue(true)]
        public bool SpaceBetweenEmptyConstructorDeclarationParentheses { // tested
            get; set; }

        [Category("Spaces - Constructors"),
         Description("Space Before Constructor Declaration Parameter Comma"),
         DefaultValue(true)]
        public bool SpaceBeforeConstructorDeclarationParameterComma { // tested
            get; set; }

        [Category("Spaces - Constructors"),
         Description("Space After Constructor Declaration Parameter Comma"),
         DefaultValue(true)]
        public bool SpaceAfterConstructorDeclarationParameterComma { // tested
            get; set; }

        [Category("Spaces - Constructors"),
         Description("Space Within Constructor Declaration Parentheses"),
         DefaultValue(true)]
        public bool SpaceWithinConstructorDeclarationParentheses { // tested
            get; set; }

        [Category("Spaces - Constructors"),
         Description("New Line Before Constructor Initializer Colon"),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement NewLineBeforeConstructorInitializerColon { get; set; }

        [Category("Spaces - Constructors"),
         Description("New Line After Constructor Initializer Colon"),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement NewLineAfterConstructorInitializerColon { get; set; }

        // indexer
        [Category("Spaces -Indexer"),
         Description("Space Before Indexer Declaration Bracket"),
         DefaultValue(true)]
        public bool SpaceBeforeIndexerDeclarationBracket { // tested
            get; set; }

        [Category("Spaces - Indexer"),
         Description("Space Within Indexer Declaration Bracket"),
         DefaultValue(true)]
        public bool SpaceWithinIndexerDeclarationBracket { // tested
            get; set; }

        [Category("Spaces - Indexer"),
         Description("Space Before Indexer Declaration Parameter Comma"),
         DefaultValue(true)]
        public bool SpaceBeforeIndexerDeclarationParameterComma { get; set; }

        [Category("Spaces - Indexer"),
         Description("Space After Indexer Declaration Parameter Comma"),
         DefaultValue(true)]
        public bool SpaceAfterIndexerDeclarationParameterComma { get; set; }

        // delegates
        [Category("Spaces - Delegates"),
         Description("Space Before Delegate Declaration Parentheses"),
         DefaultValue(true)]
        public bool SpaceBeforeDelegateDeclarationParentheses { get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Between Empty Delegate Declaration Parentheses"),
         DefaultValue(true)]
        public bool SpaceBetweenEmptyDelegateDeclarationParentheses { get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Before Delegate Declaration Parameter Comma"),
         DefaultValue(true)]
        public bool SpaceBeforeDelegateDeclarationParameterComma { get; set; }

        [Category("Spaces - Delegates"),
         Description("Space After Delegate Declaration Parameter Comma"),
         DefaultValue(true)]
        public bool SpaceAfterDelegateDeclarationParameterComma { get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Within Delegate Declaration Parentheses"),
         DefaultValue(true)]
        public bool SpaceWithinDelegateDeclarationParentheses { get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Before New Parentheses"),
         DefaultValue(true)]
        public bool SpaceBeforeNewParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Before If Parentheses"),
         DefaultValue(true)]
        public bool SpaceBeforeIfParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Before While Parentheses"),
         DefaultValue(true)]
        public bool SpaceBeforeWhileParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Before For Parentheses"),
         DefaultValue(true)]
        public bool SpaceBeforeForParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Before Foreach Parentheses"),
         DefaultValue(true)]
        public bool SpaceBeforeForeachParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Before Catch Parentheses"),
         DefaultValue(true)]
        public bool SpaceBeforeCatchParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Before Switch Parentheses"),
         DefaultValue(true)]
        public bool SpaceBeforeSwitchParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Before Lock Parentheses"),
         DefaultValue(true)]
        public bool SpaceBeforeLockParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Before Using Parentheses"),
         DefaultValue(true)]
        public bool SpaceBeforeUsingParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Around Assignment"),
         DefaultValue(true)]
        public bool SpaceAroundAssignment { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Around Logical Operator"),
         DefaultValue(true)]
        public bool SpaceAroundLogicalOperator { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Around Equality Operator"),
         DefaultValue(true)]
        public bool SpaceAroundEqualityOperator { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Around Relational Operator"),
         DefaultValue(true)]
        public bool SpaceAroundRelationalOperator { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Around Bitwise Operator"),
         DefaultValue(true)]
        public bool SpaceAroundBitwiseOperator { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Around Additive Operator"),
         DefaultValue(true)]
        public bool SpaceAroundAdditiveOperator { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Around Multiplicative Operator"),
         DefaultValue(true)]
        public bool SpaceAroundMultiplicativeOperator { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Around Shift Operator"),
         DefaultValue(true)]
        public bool SpaceAroundShiftOperator { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Around Null Coalescing Operator"),
         DefaultValue(true)]
        public bool SpaceAroundNullCoalescingOperator { // Tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space After Unsafe Address Of Operator"),
         DefaultValue(true)]
        public bool SpaceAfterUnsafeAddressOfOperator { // Tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space After Unsafe Asterisk Of Operator"),
         DefaultValue(true)]
        public bool SpaceAfterUnsafeAsteriskOfOperator { // Tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Around Unsafe Arrow Operator"),
         DefaultValue(true)]
        public bool SpaceAroundUnsafeArrowOperator { // Tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Spaces Within Parentheses"),
         DefaultValue(true)]
        public bool SpacesWithinParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description(""),
         DefaultValue(true)]
        public bool SpacesWithinIfParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Spaces Within While Parentheses"),
         DefaultValue(true)]
        public bool SpacesWithinWhileParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Spaces Within For Parentheses"),
         DefaultValue(true)]
        public bool SpacesWithinForParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Spaces Within Foreach Parentheses"),
         DefaultValue(true)]
        public bool SpacesWithinForeachParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Spaces Within Catch Parentheses"),
         DefaultValue(true)]
        public bool SpacesWithinCatchParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Spaces Within Switch Parentheses"),
         DefaultValue(true)]
        public bool SpacesWithinSwitchParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Spaces Within Lock Parentheses"),
         DefaultValue(true)]
        public bool SpacesWithinLockParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Spaces Within Using Parentheses"),
         DefaultValue(true)]
        public bool SpacesWithinUsingParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Spaces Within Cast Parentheses"),
         DefaultValue(true)]
        public bool SpacesWithinCastParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Spaces Within Size Of Parentheses"),
         DefaultValue(true)]
        public bool SpacesWithinSizeOfParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Before Size Of Parentheses"),
         DefaultValue(true)]
        public bool SpaceBeforeSizeOfParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Spaces Within Type Of Parentheses"),
         DefaultValue(true)]
        public bool SpacesWithinTypeOfParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Spaces Within New Parentheses"),
         DefaultValue(true)]
        public bool SpacesWithinNewParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Spaces Between Empty New Parentheses"),
         DefaultValue(true)]
        public bool SpacesBetweenEmptyNewParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Before New Parameter Comma"),
         DefaultValue(true)]
        public bool SpaceBeforeNewParameterComma { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space After New Parameter Comma"),
         DefaultValue(true)]
        public bool SpaceAfterNewParameterComma { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Before Type Of Parentheses"),
         DefaultValue(true)]
        public bool SpaceBeforeTypeOfParentheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Spaces Within Checked Expression Parantheses"),
         DefaultValue(true)]
        public bool SpacesWithinCheckedExpressionParantheses { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Before Conditional Operator Condition"),
         DefaultValue(true)]
        public bool SpaceBeforeConditionalOperatorCondition { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space After Conditional Operator Condition"),
         DefaultValue(true)]
        public bool SpaceAfterConditionalOperatorCondition { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space Before Conditional Operator Separator"),
         DefaultValue(true)]
        public bool SpaceBeforeConditionalOperatorSeparator { // tested
            get; set; }

        [Category("Spaces - Delegates"),
         Description("Space After Conditional Operator Separator"),
         DefaultValue(true)]
        public bool SpaceAfterConditionalOperatorSeparator { // tested
            get; set; }

        // brackets
        [Category("Spaces - Brackets"),
         Description("Spaces Within Brackets"),
         DefaultValue(true)]
        public bool SpacesWithinBrackets { // tested
            get; set; }

        [Category("Spaces - Brackets"),
         Description("Spaces Before Brackets"),
         DefaultValue(true)]
        public bool SpacesBeforeBrackets { // tested
            get; set; }

        [Category("Spaces - Brackets"),
         Description("Space Before Bracket Comma"),
         DefaultValue(true)]
        public bool SpaceBeforeBracketComma { // tested
            get; set; }

        [Category("Spaces - Brackets"),
         Description("Space After Bracket Comma"),
         DefaultValue(true)]
        public bool SpaceAfterBracketComma { // tested
            get; set; }

        [Category("Spaces - Brackets"),
         Description("Space Before For Semicolon"),
         DefaultValue(true)]
        public bool SpaceBeforeForSemicolon { // tested
            get; set; }

        [Category("Spaces - Brackets"),
         Description("Space After For Semicolon"),
         DefaultValue(true)]
        public bool SpaceAfterForSemicolon { // tested
            get; set; }

        [Category("Spaces - Brackets"),
         Description("Space After Typecast"),
         DefaultValue(true)]
        public bool SpaceAfterTypecast { // tested
            get; set; }

        [Category("Spaces - Brackets"),
         Description("Space Before Array Declaration Brackets"),
         DefaultValue(true)]
        public bool SpaceBeforeArrayDeclarationBrackets { // tested
            get; set; }

        [Category("Spaces - Brackets"),
         Description("Space In Named Argument After Double Colon"),
         DefaultValue(true)]
        public bool SpaceInNamedArgumentAfterDoubleColon { get; set; }

        [Category("Spaces - Brackets"),
         Description("Remove End Of Line WhiteSpace"),
         DefaultValue(true)]
        public bool RemoveEndOfLineWhiteSpace { get; set; }

        [Category("Spaces - Brackets"),
         Description("Space Before Semicolon"),
         DefaultValue(true)]
        public bool SpaceBeforeSemicolon { get; set; }

        #endregion

        #region Blank Lines

        [Category("Blank Lines"),
         Description("Minimum Blank Lines Before Usings"),
         DefaultValue(0)]
        public int MinimumBlankLinesBeforeUsings { get; set; }

        [Category("Blank Lines"),
         Description("Minimum Blank Lines After Usings"),
         DefaultValue(0)]
        public int MinimumBlankLinesAfterUsings { get; set; }

        [Category("Blank Lines"),
         Description("Minimum Blank Lines Before First Declaration"),
         DefaultValue(0)]
        public int MinimumBlankLinesBeforeFirstDeclaration { get; set; }

        [Category("Blank Lines"),
         Description("Minimum Blank Lines Between Types"),
         DefaultValue(0)]
        public int MinimumBlankLinesBetweenTypes { get; set; }

        [Category("Blank Lines"),
         Description("Minimum Blank Lines Between Fields"),
         DefaultValue(0)]
        public int MinimumBlankLinesBetweenFields { get; set; }

        [Category("Blank Lines"),
         Description("Minimum Blank Lines Between Event Fields"),
         DefaultValue(0)]
        public int MinimumBlankLinesBetweenEventFields { get; set; }

        [Category("Blank Lines"),
         Description("Minimum Blank Lines Between Members"),
         DefaultValue(0)]
        public int MinimumBlankLinesBetweenMembers { get; set; }

        [Category("Blank Lines"),
         Description("Minimum Blank Lines Around Region"),
         DefaultValue(0)]
        public int MinimumBlankLinesAroundRegion { get; set; }

        [Category("Blank Lines"),
         Description("Minimum Blank Lines Inside Region"),
         DefaultValue(0)]
        public int MinimumBlankLinesInsideRegion { get; set; }

        #endregion

        #region Wrapping

        [Category("Wrapping"),
         Description("Array Initializer Wrapping"),
         DefaultValue(Wrapping.DoNotWrap)]
        public Wrapping ArrayInitializerWrapping { get; set; }

        [Category("Wrapping"),
         Description("Array Initializer Brace Style"),
         DefaultValue(BraceStyle.DoNotChange)]
        public BraceStyle ArrayInitializerBraceStyle { get; set; }

        [Category("Wrapping"),
         Description("Chained Method Call Wrapping"),
         DefaultValue(Wrapping.DoNotWrap)]
        public Wrapping ChainedMethodCallWrapping { get; set; }

        [Category("Wrapping"),
         Description("Method Call Argument Wrapping"),
         DefaultValue(Wrapping.DoNotWrap)]
        public Wrapping MethodCallArgumentWrapping { get; set; }

        [Category("Wrapping"),
         Description("New Line Afer Method Call Open Parentheses"),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement NewLineAferMethodCallOpenParentheses { get; set; }

        [Category("Wrapping"),
         Description("Method Call Closing Parentheses On New Line"),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement MethodCallClosingParenthesesOnNewLine { get; set; }

        [Category("Wrapping"),
         Description("Indexer Argument Wrapping"),
         DefaultValue(Wrapping.DoNotWrap)]
        public Wrapping IndexerArgumentWrapping { get; set; }

        [Category("Wrapping"),
         Description("New Line Afer Indexer Open Bracket"),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement NewLineAferIndexerOpenBracket { get; set; }

        [Category("Wrapping"),
         Description("Indexer Closing Bracket On New Line"),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement IndexerClosingBracketOnNewLine { get; set; }

        [Category("Wrapping"),
         Description("Method Declaration Parameter Wrapping"),
         DefaultValue(Wrapping.DoNotWrap)]
        public Wrapping MethodDeclarationParameterWrapping { get; set; }

        [Category("Wrapping"),
         Description("New Line Afer Method Declaration Open Parentheses"),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement NewLineAferMethodDeclarationOpenParentheses { get; set; }

        [Category("Wrapping"),
         Description("Method Declaration Closing Parentheses On New Line"),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement MethodDeclarationClosingParenthesesOnNewLine { get; set; }

        [Category("Wrapping"),
         Description("Indexer Declaration Parameter Wrapping"),
         DefaultValue(Wrapping.DoNotWrap)]
        public Wrapping IndexerDeclarationParameterWrapping { get; set; }

        [Category("Wrapping"),
         Description("NewL ine Afer Indexer Declaration Open Bracket"),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement NewLineAferIndexerDeclarationOpenBracket { get; set; }

        [Category("Wrapping"),
         Description("Indexer Declaration Closing Bracket On New Line"),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement IndexerDeclarationClosingBracketOnNewLine { get; set; }

        [Category("Wrapping"),
         Description("Align To First Indexer Argument"),
         DefaultValue(true)]
        public bool AlignToFirstIndexerArgument { get; set; }

        [Category("Wrapping"),
         Description("Align To First Indexer Declaration Parameter"),
         DefaultValue(true)]
        public bool AlignToFirstIndexerDeclarationParameter { get; set; }

        [Category("Wrapping"),
         Description("Align To First Method Call Argument"),
         DefaultValue(true)]
        public bool AlignToFirstMethodCallArgument { get; set; }

        [Category("Wrapping"),
         Description("Align To First Method Declaration Parameter"),
         DefaultValue(true)]
        public bool AlignToFirstMethodDeclarationParameter { get; set; }

        [Category("Wrapping"),
         Description("New Line Before New Query Clause"),
         DefaultValue(NewLinePlacement.NewLine)]
        public NewLinePlacement NewLineBeforeNewQueryClause { get; set; }

        #endregion
    }
}