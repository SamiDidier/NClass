using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using ICSharpCode.NRefactory.CSharp;
using System.ComponentModel;


namespace NClass.CodeGenerator
{
    [DefaultPropertyAttribute("C# formatting Options")]
    public class CSharpFormattingOptionsUI
    {
		public string Name {
			get;
			set;
		}

		public bool IsBuiltIn {
			get;
			set;
		}

		public CSharpFormattingOptionsUI Clone ()
		{
			return (CSharpFormattingOptionsUI)MemberwiseClone ();
		}

		#region Indentation
        [CategoryAttribute("Indent"),
         DescriptionAttribute("Indent Namespace Body"),
         DefaultValueAttribute(true)]
		public bool IndentNamespaceBody { // tested
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Indent Class Body"),
         DefaultValueAttribute(true)]
		public bool IndentClassBody { // tested
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Indent Interface Body"),
         DefaultValueAttribute(true)]
		public bool IndentInterfaceBody { // tested
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Indent Struct Body"),
         DefaultValueAttribute(true)]
		public bool IndentStructBody { // tested
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Indent Enum Body"),
         DefaultValueAttribute(true)]
		public bool IndentEnumBody { // tested
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Indent Method Body"),
         DefaultValueAttribute(true)]
		public bool IndentMethodBody { // tested
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Indent Property Body"),
         DefaultValueAttribute(true)]
		public bool IndentPropertyBody { // tested
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Indent Event Body"),
         DefaultValueAttribute(true)]
		public bool IndentEventBody { // tested
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Indent Blocks"),
         DefaultValueAttribute(true)]
		public bool IndentBlocks { // tested
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Indent Switch Body"),
         DefaultValueAttribute(true)]
		public bool IndentSwitchBody { // tested
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Indent Case Body"),
         DefaultValueAttribute(true)]
		public bool IndentCaseBody { // tested
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Indent Break Statements"),
         DefaultValueAttribute(true)]
		public bool IndentBreakStatements { // tested
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Align Embedded Statements"),
         DefaultValueAttribute(true)]
		public bool AlignEmbeddedStatements { // tested
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Align Else In If Statements"),
         DefaultValueAttribute(true)]
		public bool AlignElseInIfStatements {
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Auto Property Formatting"),
         DefaultValueAttribute(true)]
		public PropertyFormatting AutoPropertyFormatting { // tested
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Simple Property Formatting"),
         DefaultValueAttribute(true)]
		public PropertyFormatting SimplePropertyFormatting { // tested
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Empty Line Formatting"),
         DefaultValueAttribute(true)]
		public EmptyLineFormatting EmptyLineFormatting {
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Indent Preprocessor Directives"),
         DefaultValueAttribute(true)]
		public bool IndentPreprocessorDirectives { // tested
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Align To Member Reference Dot"),
         DefaultValueAttribute(true)]
		public bool AlignToMemberReferenceDot { // TODO!
			get;
			set;
		}

        [CategoryAttribute("Indent"),
         DescriptionAttribute("Indent Blocks Inside Expressions"),
         DefaultValueAttribute(true)]
		public bool IndentBlocksInsideExpressions {
			get;
			set;
		}
		#endregion
		
		#region Braces
        [CategoryAttribute("Braces"),
         DescriptionAttribute("Namespace Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle NamespaceBraceStyle { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Class Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle ClassBraceStyle { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Interface Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle InterfaceBraceStyle { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Struct Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle StructBraceStyle { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Enum Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle EnumBraceStyle { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Method Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle MethodBraceStyle { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Anonymous Method Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle AnonymousMethodBraceStyle {
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Constructor Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle ConstructorBraceStyle {  // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Destructor Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle DestructorBraceStyle { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Property Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle PropertyBraceStyle { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Property Get Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle PropertyGetBraceStyle { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Property Set Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle PropertySetBraceStyle { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Simple Get Block Formatting"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public PropertyFormatting SimpleGetBlockFormatting { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Simple Set Block Formatting"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public PropertyFormatting SimpleSetBlockFormatting { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Event Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle EventBraceStyle { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Event Add Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle EventAddBraceStyle { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Event Remove Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle EventRemoveBraceStyle { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Allow Event Add Block In line"),
         DefaultValueAttribute(true)]
		public bool AllowEventAddBlockInline { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Allow Event Remove Block In line"),
         DefaultValueAttribute(true)]
		public bool AllowEventRemoveBlockInline { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Statement Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle StatementBraceStyle { // tested
			get;
			set;
		}

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Allow If Block In line"),
         DefaultValueAttribute(true)]
		public bool AllowIfBlockInline {
			get;
			set;
		}

		bool allowOneLinedArrayInitialziers = true;

        [CategoryAttribute("Braces"),
         DescriptionAttribute("Allow One Lined Array Initialziers"),
         DefaultValueAttribute(true)]
		public bool AllowOneLinedArrayInitialziers {
			get {
				return allowOneLinedArrayInitialziers;
			}
			set {
				allowOneLinedArrayInitialziers = value;
			}
		}
		#endregion

		#region NewLines
        [CategoryAttribute("NewLines"),
         DescriptionAttribute("Else New Line Placement"),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement ElseNewLinePlacement { // tested
			get;
			set;
		}

        [CategoryAttribute("NewLines"),
         DescriptionAttribute(""),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement ElseIfNewLinePlacement { // tested
			get;
			set;
		}

        [CategoryAttribute("NewLines"),
         DescriptionAttribute("Catch New Line Placement"),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement CatchNewLinePlacement { // tested
			get;
			set;
		}

        [CategoryAttribute("NewLines"),
         DescriptionAttribute("Finally New Line Placement"),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement FinallyNewLinePlacement { // tested
			get;
			set;
		}

        [CategoryAttribute("NewLines"),
         DescriptionAttribute("While New Line Placement"),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement WhileNewLinePlacement { // tested
			get;
			set;
		}

		NewLinePlacement embeddedStatementPlacement = NewLinePlacement.NewLine;

        [CategoryAttribute("NewLines"),
         DescriptionAttribute("Embedded Statement Placement"),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement EmbeddedStatementPlacement {
			get {
				return embeddedStatementPlacement;
			}
			set {
				embeddedStatementPlacement = value;
			}
		}
		#endregion
		
		#region Spaces
		// Methods
        [CategoryAttribute("Spaces - Method"),
         DescriptionAttribute("Space Before Method Declaration Parentheses"),
         DefaultValueAttribute(true)]
		public bool SpaceBeforeMethodDeclarationParentheses { // tested
			get;
			set;
		}

        [CategoryAttribute("Spaces - Method"),
         DescriptionAttribute("Space Between Empty Method Declaration Parentheses"),
         DefaultValueAttribute(true)]
		public bool SpaceBetweenEmptyMethodDeclarationParentheses {
			get;
			set;
		}

        [CategoryAttribute("Spaces - Method"),
         DescriptionAttribute("Space Before Method Declaration Parameter Comma"),
         DefaultValueAttribute(true)]
		public bool SpaceBeforeMethodDeclarationParameterComma { // tested
			get;
			set;
		}

        [CategoryAttribute("Spaces - Method"),
         DescriptionAttribute("Space After Method Declaration Parameter Comma"),
         DefaultValueAttribute(true)]
		public bool SpaceAfterMethodDeclarationParameterComma { // tested
			get;
			set;
		}

        [CategoryAttribute("Spaces - Method"),
         DescriptionAttribute("Space Within Method Declaration Parentheses"),
         DefaultValueAttribute(true)]
		public bool SpaceWithinMethodDeclarationParentheses { // tested
			get;
			set;
		}
		
		// Method calls
        [CategoryAttribute("Spaces - Method calls"),
         DescriptionAttribute("Space Before Method Call Parentheses"),
         DefaultValueAttribute(true)]
		public bool SpaceBeforeMethodCallParentheses { // tested
			get;
			set;
		}

        [CategoryAttribute("Spaces - Method calls"),
         DescriptionAttribute("Space Between Empty Method Call Parentheses"),
         DefaultValueAttribute(true)]
		public bool SpaceBetweenEmptyMethodCallParentheses { // tested
			get;
			set;
		}

        [CategoryAttribute("Spaces - Method calls"),
         DescriptionAttribute("Space Before Method Call Parameter Comma"),
         DefaultValueAttribute(true)]
		public bool SpaceBeforeMethodCallParameterComma { // tested
			get;
			set;
		}

        [CategoryAttribute("Spaces - Method calls"),
         DescriptionAttribute("Space After Method Call Parameter Comma"),
         DefaultValueAttribute(true)]
		public bool SpaceAfterMethodCallParameterComma { // tested
			get;
			set;
		}

        [CategoryAttribute("Spaces - Method calls"),
         DescriptionAttribute("Space Withi nMethod Call Parentheses"),
         DefaultValueAttribute(true)]
		public bool SpaceWithinMethodCallParentheses { // tested
			get;
			set;
		}
		
		// fields
	    [CategoryAttribute("Spaces - Fields"),
         DescriptionAttribute("Space Before Field Declaration Comma"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeFieldDeclarationComma { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Fields"),
         DescriptionAttribute("Space After Field Declaration Comma"),
         DefaultValueAttribute(true)]	
		public bool SpaceAfterFieldDeclarationComma { // tested
			get;
			set;
		}
		
		// local variables
	    [CategoryAttribute("Spaces - Local variables"),
         DescriptionAttribute("Space Before Local Variable Declaration Comma"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeLocalVariableDeclarationComma { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Local variables"),
         DescriptionAttribute("Space After Local Variable Declaration Comma"),
         DefaultValueAttribute(true)]	
		public bool SpaceAfterLocalVariableDeclarationComma { // tested
			get;
			set;
		}
		
		// constructors
	    [CategoryAttribute("Spaces - Constructors"),
         DescriptionAttribute("Space Before Constructor Declaration Parentheses"),
         DefaultValueAttribute(true)]			
		public bool SpaceBeforeConstructorDeclarationParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Constructors"),
         DescriptionAttribute("Space Between Empty Constructor Declaration Parentheses"),
         DefaultValueAttribute(true)]
		public bool SpaceBetweenEmptyConstructorDeclarationParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Constructors"),
         DescriptionAttribute("Space Before Constructor Declaration Parameter Comma"),
         DefaultValueAttribute(true)]
		public bool SpaceBeforeConstructorDeclarationParameterComma { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Constructors"),
         DescriptionAttribute("Space After Constructor Declaration Parameter Comma"),
         DefaultValueAttribute(true)]
		public bool SpaceAfterConstructorDeclarationParameterComma { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Constructors"),
         DescriptionAttribute("Space Within Constructor Declaration Parentheses"),
         DefaultValueAttribute(true)]
		public bool SpaceWithinConstructorDeclarationParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Constructors"),
         DescriptionAttribute("New Line Before Constructor Initializer Colon"),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement NewLineBeforeConstructorInitializerColon {
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Constructors"),
         DescriptionAttribute("New Line After Constructor Initializer Colon"),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement NewLineAfterConstructorInitializerColon {
			get;
			set;
		}
		
		// indexer
	    [CategoryAttribute("Spaces -Indexer"),
         DescriptionAttribute("Space Before Indexer Declaration Bracket"),
         DefaultValueAttribute(true)]
		public bool SpaceBeforeIndexerDeclarationBracket { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Indexer"),
         DescriptionAttribute("Space Within Indexer Declaration Bracket"),
         DefaultValueAttribute(true)]
		public bool SpaceWithinIndexerDeclarationBracket { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Indexer"),
         DescriptionAttribute("Space Before Indexer Declaration Parameter Comma"),
         DefaultValueAttribute(true)]
		public bool SpaceBeforeIndexerDeclarationParameterComma {
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Indexer"),
         DescriptionAttribute("Space After Indexer Declaration Parameter Comma"),
         DefaultValueAttribute(true)]
		public bool SpaceAfterIndexerDeclarationParameterComma {
			get;
			set;
		}
		
		// delegates
	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Before Delegate Declaration Parentheses"),
         DefaultValueAttribute(true)]		
		public bool SpaceBeforeDelegateDeclarationParentheses {
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Between Empty Delegate Declaration Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpaceBetweenEmptyDelegateDeclarationParentheses {
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Before Delegate Declaration Parameter Comma"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeDelegateDeclarationParameterComma {
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space After Delegate Declaration Parameter Comma"),
         DefaultValueAttribute(true)]	
		public bool SpaceAfterDelegateDeclarationParameterComma {
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Within Delegate Declaration Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpaceWithinDelegateDeclarationParentheses {
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Before New Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeNewParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Before If Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeIfParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Before While Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeWhileParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Before For Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeForParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Before Foreach Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeForeachParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Before Catch Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeCatchParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Before Switch Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeSwitchParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Before Lock Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeLockParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Before Using Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeUsingParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Around Assignment"),
         DefaultValueAttribute(true)]	
		public bool SpaceAroundAssignment { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Around Logical Operator"),
         DefaultValueAttribute(true)]	
		public bool SpaceAroundLogicalOperator { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Around Equality Operator"),
         DefaultValueAttribute(true)]	
		public bool SpaceAroundEqualityOperator { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Around Relational Operator"),
         DefaultValueAttribute(true)]	
		public bool SpaceAroundRelationalOperator { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Around Bitwise Operator"),
         DefaultValueAttribute(true)]	
		public bool SpaceAroundBitwiseOperator { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Around Additive Operator"),
         DefaultValueAttribute(true)]	
		public bool SpaceAroundAdditiveOperator { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Around Multiplicative Operator"),
         DefaultValueAttribute(true)]	
		public bool SpaceAroundMultiplicativeOperator { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Around Shift Operator"),
         DefaultValueAttribute(true)]	
		public bool SpaceAroundShiftOperator { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Around Null Coalescing Operator"),
         DefaultValueAttribute(true)]	
		public bool SpaceAroundNullCoalescingOperator { // Tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space After Unsafe Address Of Operator"),
         DefaultValueAttribute(true)]	
		public bool SpaceAfterUnsafeAddressOfOperator { // Tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space After Unsafe Asterisk Of Operator"),
         DefaultValueAttribute(true)]	
		public bool SpaceAfterUnsafeAsteriskOfOperator { // Tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Around Unsafe Arrow Operator"),
         DefaultValueAttribute(true)]	
		public bool SpaceAroundUnsafeArrowOperator { // Tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Spaces Within Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpacesWithinParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute(""),
         DefaultValueAttribute(true)]	
		public bool SpacesWithinIfParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Spaces Within While Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpacesWithinWhileParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Spaces Within For Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpacesWithinForParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Spaces Within Foreach Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpacesWithinForeachParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Spaces Within Catch Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpacesWithinCatchParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Spaces Within Switch Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpacesWithinSwitchParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Spaces Within Lock Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpacesWithinLockParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Spaces Within Using Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpacesWithinUsingParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Spaces Within Cast Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpacesWithinCastParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Spaces Within Size Of Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpacesWithinSizeOfParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Before Size Of Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeSizeOfParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Spaces Within Type Of Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpacesWithinTypeOfParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Spaces Within New Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpacesWithinNewParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Spaces Between Empty New Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpacesBetweenEmptyNewParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Before New Parameter Comma"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeNewParameterComma { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space After New Parameter Comma"),
         DefaultValueAttribute(true)]	
		public bool SpaceAfterNewParameterComma { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Before Type Of Parentheses"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeTypeOfParentheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Spaces Within Checked Expression Parantheses"),
         DefaultValueAttribute(true)]	
		public bool SpacesWithinCheckedExpressionParantheses { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Before Conditional Operator Condition"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeConditionalOperatorCondition { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space After Conditional Operator Condition"),
         DefaultValueAttribute(true)]	
		public bool SpaceAfterConditionalOperatorCondition { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space Before Conditional Operator Separator"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeConditionalOperatorSeparator { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Delegates"),
         DescriptionAttribute("Space After Conditional Operator Separator"),
         DefaultValueAttribute(true)]	
		public bool SpaceAfterConditionalOperatorSeparator { // tested
			get;
			set;
		}
		
		// brackets
	    [CategoryAttribute("Spaces - Brackets"),
         DescriptionAttribute("Spaces Within Brackets"),
         DefaultValueAttribute(true)]	
		public bool SpacesWithinBrackets { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Brackets"),
         DescriptionAttribute("Spaces Before Brackets"),
         DefaultValueAttribute(true)]	
		public bool SpacesBeforeBrackets { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Brackets"),
         DescriptionAttribute("Space Before Bracket Comma"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeBracketComma { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Brackets"),
         DescriptionAttribute("Space After Bracket Comma"),
         DefaultValueAttribute(true)]	
		public bool SpaceAfterBracketComma { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Brackets"),
         DescriptionAttribute("Space Before For Semicolon"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeForSemicolon { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Brackets"),
         DescriptionAttribute("Space After For Semicolon"),
         DefaultValueAttribute(true)]	
		public bool SpaceAfterForSemicolon { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Brackets"),
         DescriptionAttribute("Space After Typecast"),
         DefaultValueAttribute(true)]	
		public bool SpaceAfterTypecast { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Brackets"),
         DescriptionAttribute("Space Before Array Declaration Brackets"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeArrayDeclarationBrackets { // tested
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Brackets"),
         DescriptionAttribute("Space In Named Argument After Double Colon"),
         DefaultValueAttribute(true)]	
		public bool SpaceInNamedArgumentAfterDoubleColon {
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Brackets"),
         DescriptionAttribute("Remove End Of Line WhiteSpace"),
         DefaultValueAttribute(true)]	
		public bool RemoveEndOfLineWhiteSpace {
			get;
			set;
		}

	    [CategoryAttribute("Spaces - Brackets"),
         DescriptionAttribute("Space Before Semicolon"),
         DefaultValueAttribute(true)]	
		public bool SpaceBeforeSemicolon {
			get;
			set;
		}
		#endregion
		
		#region Blank Lines
	    [CategoryAttribute("Blank Lines"),
         DescriptionAttribute("Minimum Blank Lines Before Usings"),
         DefaultValueAttribute(0)]	
		public int MinimumBlankLinesBeforeUsings {
			get;
			set;
		}

	    [CategoryAttribute("Blank Lines"),
         DescriptionAttribute("Minimum Blank Lines After Usings"),
         DefaultValueAttribute(0)]
		public int MinimumBlankLinesAfterUsings {
			get;
			set;
		}

	    [CategoryAttribute("Blank Lines"),
         DescriptionAttribute("Minimum Blank Lines Before First Declaration"),
         DefaultValueAttribute(0)]
		public int MinimumBlankLinesBeforeFirstDeclaration {
			get;
			set;
		}

	    [CategoryAttribute("Blank Lines"),
         DescriptionAttribute("Minimum Blank Lines Between Types"),
         DefaultValueAttribute(0)]
		public int MinimumBlankLinesBetweenTypes {
			get;
			set;
		}

	    [CategoryAttribute("Blank Lines"),
         DescriptionAttribute("Minimum Blank Lines Between Fields"),
         DefaultValueAttribute(0)]
		public int MinimumBlankLinesBetweenFields {
			get;
			set;
		}

	    [CategoryAttribute("Blank Lines"),
         DescriptionAttribute("Minimum Blank Lines Between Event Fields"),
         DefaultValueAttribute(0)]
		public int MinimumBlankLinesBetweenEventFields {
			get;
			set;
		}

	    [CategoryAttribute("Blank Lines"),
         DescriptionAttribute("Minimum Blank Lines Between Members"),
         DefaultValueAttribute(0)]
		public int MinimumBlankLinesBetweenMembers {
			get;
			set;
		}

	    [CategoryAttribute("Blank Lines"),
         DescriptionAttribute("Minimum Blank Lines Around Region"),
         DefaultValueAttribute(0)]
		public int MinimumBlankLinesAroundRegion {
			get;
			set;
		}

	    [CategoryAttribute("Blank Lines"),
         DescriptionAttribute("Minimum Blank Lines Inside Region"),
         DefaultValueAttribute(0)]
		public int MinimumBlankLinesInsideRegion {
			get;
			set;
		}
		#endregion


		#region Keep formatting
	    [CategoryAttribute("Keep formatting"),
         DescriptionAttribute("Keep Comments At First Column"),
         DefaultValueAttribute(true)]
		public bool KeepCommentsAtFirstColumn {
			get;
			set;
		}
		#endregion

		#region Wrapping
	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("Array Initializer Wrapping"),
         DefaultValueAttribute(Wrapping.DoNotWrap)]
		public Wrapping ArrayInitializerWrapping {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("Array Initializer Brace Style"),
         DefaultValueAttribute(BraceStyle.DoNotChange)]
		public BraceStyle ArrayInitializerBraceStyle {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("Chained Method Call Wrapping"),
         DefaultValueAttribute(Wrapping.DoNotWrap)]
		public Wrapping ChainedMethodCallWrapping {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("Method Call Argument Wrapping"),
         DefaultValueAttribute(Wrapping.DoNotWrap)]
		public Wrapping MethodCallArgumentWrapping {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("New Line Afer Method Call Open Parentheses"),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement NewLineAferMethodCallOpenParentheses {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("Method Call Closing Parentheses On New Line"),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement MethodCallClosingParenthesesOnNewLine {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("Indexer Argument Wrapping"),
         DefaultValueAttribute(Wrapping.DoNotWrap)]
		public Wrapping IndexerArgumentWrapping {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("New Line Afer Indexer Open Bracket"),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement NewLineAferIndexerOpenBracket {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("Indexer Closing Bracket On New Line"),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement IndexerClosingBracketOnNewLine {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("Method Declaration Parameter Wrapping"),
         DefaultValueAttribute(Wrapping.DoNotWrap)]
		public Wrapping MethodDeclarationParameterWrapping {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("New Line Afer Method Declaration Open Parentheses"),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement NewLineAferMethodDeclarationOpenParentheses {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("Method Declaration Closing Parentheses On New Line"),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement MethodDeclarationClosingParenthesesOnNewLine {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("Indexer Declaration Parameter Wrapping"),
         DefaultValueAttribute(Wrapping.DoNotWrap)]
		public Wrapping IndexerDeclarationParameterWrapping {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("NewL ine Afer Indexer Declaration Open Bracket"),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement NewLineAferIndexerDeclarationOpenBracket {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("Indexer Declaration Closing Bracket On New Line"),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement IndexerDeclarationClosingBracketOnNewLine {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("Align To First Indexer Argument"),
         DefaultValueAttribute(true)]
		public bool AlignToFirstIndexerArgument {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("Align To First Indexer Declaration Parameter"),
         DefaultValueAttribute(true)]
		public bool AlignToFirstIndexerDeclarationParameter {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("Align To First Method Call Argument"),
         DefaultValueAttribute(true)]
		public bool AlignToFirstMethodCallArgument {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("Align To First Method Declaration Parameter"),
         DefaultValueAttribute(true)]
		public bool AlignToFirstMethodDeclarationParameter {
			get;
			set;
		}

	    [CategoryAttribute("Wrapping"),
         DescriptionAttribute("New Line Before New Query Clause"),
         DefaultValueAttribute(NewLinePlacement.NewLine)]
		public NewLinePlacement NewLineBeforeNewQueryClause {
			get;
			set;
		}

		#endregion

		#region Using Declarations
	    [CategoryAttribute("Using Declarations"),
         DescriptionAttribute("Using Placement"),
         DefaultValueAttribute(UsingPlacement.InsideNamespace)]
		public UsingPlacement UsingPlacement {
			get;
			set;
		}
		#endregion

        public CSharpFormattingOptionsUI()
		{
		}

		public static CSharpFormattingOptionsUI Load (string selectedFile)
		{
			using (var stream = System.IO.File.OpenRead (selectedFile)) {
				return Load (stream);
			}
		}

		public static CSharpFormattingOptionsUI Load (System.IO.Stream input)
		{
            CSharpFormattingOptionsUI result = FormattingOptionsFactoryUI.CreateMono();
			result.Name = "noname";
			using (XmlTextReader reader = new XmlTextReader (input)) {
				while (reader.Read ()) {
					if (reader.NodeType == XmlNodeType.Element) {
						if (reader.LocalName == "Property") {
							var info = typeof(CSharpFormattingOptionsUI).GetProperty (reader.GetAttribute ("name"));
							string valString = reader.GetAttribute ("value");
							object value;
							if (info.PropertyType == typeof(bool)) {
								value = Boolean.Parse (valString);
							} else if (info.PropertyType == typeof(int)) {
								value = Int32.Parse (valString);
							} else {
								value = Enum.Parse (info.PropertyType, valString);
							}
							info.SetValue (result, value, null);
						} else if (reader.LocalName == "FormattingProfile") {
							result.Name = reader.GetAttribute ("name");
						}
					} else if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == "FormattingProfile") {
						//Console.WriteLine ("result:" + result.Name);
						return result;
					}
				}
			}
			return result;
		}

		public void Save (string fileName)
		{
			using (var writer = new XmlTextWriter (fileName, Encoding.Default)) {
				writer.Formatting = System.Xml.Formatting.Indented;
				writer.Indentation = 1;
				writer.IndentChar = '\t';
				writer.WriteStartElement ("FormattingProfile");
				writer.WriteAttributeString ("name", Name);
				foreach (PropertyInfo info in typeof (CSharpFormattingOptionsUI).GetProperties ()) {
					//if (info.GetCustomAttributes (false).Any (o => o.GetType () == typeof(ItemPropertyAttribute))) {
						writer.WriteStartElement ("Property");
						writer.WriteAttributeString ("name", info.Name);
						writer.WriteAttributeString ("value", info.GetValue (this, null).ToString ());
						writer.WriteEndElement ();
					//}
				}
				writer.WriteEndElement ();
			}
		}

		public bool Equals (CSharpFormattingOptionsUI other)
		{
			foreach (PropertyInfo info in typeof (CSharpFormattingOptionsUI).GetProperties ()) {
				//if (info.GetCustomAttributes (false).Any (o => o.GetType () == typeof(ItemPropertyAttribute))) {
					object val = info.GetValue (this, null);
					object otherVal = info.GetValue (other, null);
					if (val == null) {
						if (otherVal == null)
							continue;
						return false;
					}
					if (!val.Equals (otherVal)) {
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
            CSharpFormattingOptions b = FormattingOptionsFactory.CreateEmpty();

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
            b. NewLineAfterConstructorInitializerColon = a.NewLineAfterConstructorInitializerColon;
		
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
            CSharpFormattingOptionsUI b = FormattingOptionsFactoryUI.CreateEmpty();

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
    }
}
