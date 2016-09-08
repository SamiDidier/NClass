// NClass - Free class diagram editor
// Copyright (C) 2006-2009 Balazs Tihanyi
// 
// This program is free software; you can redistribute it and/or modify it under 
// the terms of the GNU General Public License as published by the Free Software 
// Foundation; either version 3 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT 
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS 
// FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with 
// this program; if not, write to the Free Software Foundation, Inc., 
// 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using NClass.Translations;

namespace NClass.DiagramEditor
{
    public enum GradientStyle
    {
        None,
        Horizontal,
        Vertical,
        Diagonal
    }

    [Serializable]
    [DefaultProperty("AttributeColor")]
    public sealed class Style : IDisposable
    {
        private static Style currentStyle;
        private static readonly SortedList<string, Style> styles = new SortedList<string, Style>();

        private static readonly string settingsDir = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "NClass");

        private static readonly string userStylePath = Path.Combine(settingsDir, "style.dst");

        static Style()
        {
            Directory.CreateDirectory(StylesDirectory);
            LoadStyles();
            if (!LoadCurrentStyle())
            {
                CurrentStyle = new Style();
                SaveCurrentStyle();
            }
        }

        public Style()
        {
            abstractNameFont = new Font(nameFont, nameFont.Style | FontStyle.Italic);
            staticMemberFont = new Font(memberFont, memberFont.Style | FontStyle.Underline);
            abstractMemberFont = new Font(memberFont, memberFont.Style | FontStyle.Italic);
        }

        public static IEnumerable<Style> AvaiableStyles { get { return styles.Values; } }

        public static Style CurrentStyle
        {
            get { return currentStyle; }
            set
            {
                if (value != null && currentStyle != value)
                {
                    if (currentStyle != null)
                        currentStyle.Dispose();
                    currentStyle = value.Clone();
                    SaveCurrentStyle();
                    if (CurrentStyleChanged != null)
                        CurrentStyleChanged(null, EventArgs.Empty);
                }
            }
        }

        public static string StylesDirectory { get; } = Path.Combine(settingsDir, "UserStyles");

        #region Background properties

        [DisplayName("Background Color"), Category("(Background)")]
        [Description("The background color for the diagram.")]
        [DefaultValue(typeof (Color), "White")]
        public Color BackgroundColor { get { return backgroundColor; } set { backgroundColor = value; } }

        #endregion

        public void Dispose()
        {
            nameFont.Dispose();
            abstractNameFont.Dispose();
            identifierFont.Dispose();
            memberFont.Dispose();
            staticMemberFont.Dispose();
            abstractMemberFont.Dispose();
            commentFont.Dispose();
            relationshipTextFont.Dispose();
        }

        public static event EventHandler CurrentStyleChanged;

        public Style Clone()
        {
            var newStyle = (Style) MemberwiseClone();

            newStyle.nameFont = (Font) NameFont.Clone();
            newStyle.abstractNameFont = (Font) AbstractNameFont.Clone();
            newStyle.identifierFont = (Font) IdentifierFont.Clone();
            newStyle.memberFont = (Font) MemberFont.Clone();
            newStyle.staticMemberFont = (Font) StaticMemberFont.Clone();
            newStyle.abstractMemberFont = (Font) AbstractMemberFont.Clone();
            newStyle.commentFont = (Font) CommentFont.Clone();
            newStyle.relationshipTextFont = (Font) RelationshipTextFont.Clone();

            return newStyle;
        }

        private static bool LoadStyles()
        {
            try
            {
                if (Directory.Exists(StylesDirectory))
                {
                    var files = Directory.GetFiles(StylesDirectory, "*.dst");
                    foreach (var file in files)
                        Load(file);

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private static bool LoadCurrentStyle()
        {
            return (currentStyle = Load(userStylePath, false)) != null;
        }

        public static bool SaveCurrentStyle()
        {
            return CurrentStyle.Save(userStylePath, false);
        }

        public static Style Load(string path)
        {
            return Load(path, true);
        }

        private static Style Load(string path, bool addToList)
        {
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Open))
                {
                    var formatter = new BinaryFormatter();
                    var result = (Style) formatter.Deserialize(stream);

                    if (addToList && result != null)
                        AddToList(result, path);

                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        [OnDeserialized]
        private void SetFonts(StreamingContext context)
        {
            abstractNameFont = new Font(nameFont, nameFont.Style | FontStyle.Italic);
            staticMemberFont = new Font(memberFont, memberFont.Style | FontStyle.Underline);
            abstractMemberFont = new Font(memberFont, memberFont.Style | FontStyle.Italic);
        }

        public bool Save(string filePath)
        {
            return Save(filePath, true);
        }

        private bool Save(string path, bool addToList)
        {
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                }

                if (addToList)
                    AddToList(Clone(), path);

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static void AddToList(Style style, string stylePath)
        {
            if (!styles.ContainsKey(stylePath))
            {
                styles.Add(stylePath, style);
            }
            else // Replace the old style
            {
                styles.Remove(stylePath);
                styles.Add(stylePath, style);
            }
        }

        public override string ToString()
        {
            return Name;
        }

        #region Fields

        // General fields
        private string name;
        private string author;
        private Color backgroundColor = Color.White;
        private Size shadowOffset = new Size(4, 4);
        private Color shadowColor = Color.FromArgb(70, Color.Black);

        // Type fields
        private bool showSignature;
        private bool showStereotypes = true;
        private bool useIcons;
        private Color identifierColor = Color.Black;
        private Color nameColor = Color.Black;
        private Color attributeColor = Color.Black;
        private Color operationColor = Color.Black;
        private ContentAlignment headerAlignment = ContentAlignment.MiddleCenter;
        private Font nameFont = new Font("Arial", 9.75F, FontStyle.Bold);
        private Font identifierFont = new Font("Tahoma", 6.75F);
        private Font memberFont = new Font("Tahoma", 8.25F);

        [NonSerialized]
        private Font abstractNameFont;

        [NonSerialized]
        private Font staticMemberFont;

        [NonSerialized]
        private Font abstractMemberFont;

        // Class fields
        private int classBorderWidth = 1;
        private int classRoundingSize;
        private bool isClassBorderDashed;
        private Color classBackgroundColor = Color.White;
        private Color classBorderColor = Color.Black;
        private Color classHeaderColor = Color.White;
        private GradientStyle classGradientHeaderStyle = GradientStyle.None;

        // Modified class fields
        private int abstractClassBorderWidth = 1;
        private int sealedClassBorderWidth = 1;
        private int staticClassBorderWidth = 1;
        private bool isAbstractClassBorderDashed;
        private bool isSealedClassBorderDashed;
        private bool isStaticClassBorderDashed;

        // Structure fields
        private int structureBorderWidth = 1;
        private int structureRoundingSize;
        private bool isStructureBorderDashed;
        private Color structureBackgroundColor = Color.White;
        private Color structureBorderColor = Color.Black;
        private Color structureHeaderColor = Color.White;
        private GradientStyle structureGradientHeaderStyle = GradientStyle.None;

        // Interface fields
        private int interfaceBorderWidth = 1;
        private int interfaceRoundingSize;
        private bool isInterfaceBorderDashed;
        private Color interfaceBackgroundColor = Color.White;
        private Color interfaceBorderColor = Color.Black;
        private Color interfaceHeaderColor = Color.White;
        private GradientStyle interfaceGradientHeaderStyle = GradientStyle.None;

        // Enum fields
        private int enumBorderWidth = 1;
        private int enumRoundingSize;
        private bool isEnumBorderDashed;
        private Color enumBackgroundColor = Color.White;
        private Color enumBorderColor = Color.Black;
        private Color enumHeaderColor = Color.White;
        private Color enumItemColor = Color.Black;
        private GradientStyle enumGradientHeaderStyle = GradientStyle.None;

        // Delegate fields
        private int delegateBorderWidth = 1;
        private int delegateRoundingSize;
        private bool isDelegateBorderDashed;
        private Color delegateBackgroundColor = Color.White;
        private Color delegateBorderColor = Color.Black;
        private Color delegateHeaderColor = Color.White;
        private Color delegateParameterColor = Color.Black;
        private GradientStyle delegateGradientHeaderStyle = GradientStyle.None;

        // Comment fields
        private int commentBorderWidth = 1;
        private bool isCommentBorderDashed;
        private Color commentBackColor = Color.White;
        private Color commentBorderColor = Color.Black;
        private Color textColor = Color.Black;
        private Font commentFont = new Font("Tahoma", 8.25F);

        // Relationship fields
        private int relationshipDashSize = 5;
        private int relationshipWidth = 1;
        private Color relationshipColor = Color.Black;
        private Color relationshipTextColor = Color.Black;
        private Font relationshipTextFont = new Font("Tahoma", 8.25F);

        #endregion

        #region Style Information

        [Browsable(false)]
        public bool IsUntitled { get { return string.IsNullOrEmpty(name); } }

        [DisplayName("Style Name"), Category("(Style Information)")]
        [Description("The name of the current style.")]
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(name))
                    return Strings.Untitled;
                return name;
            }
            set
            {
                if (value == Strings.Untitled)
                    name = null;
                else
                    name = value;
            }
        }

        [DisplayName("Author"), Category("(Style Information)")]
        [Description("The author of the current style.")]
        public string Author
        {
            get
            {
                if (string.IsNullOrEmpty(author))
                    return Strings.Unknown;
                return author;
            }
            set
            {
                if (value == Strings.Unknown)
                    author = null;
                else
                    author = value;
            }
        }

        #endregion

        #region All Types properties

        [DisplayName("Attribute Color"), Category("(All Types)")]
        [Description("The text color of a type's attributes.")]
        [DefaultValue(typeof (Color), "Black")]
        public Color AttributeColor { get { return attributeColor; } set { attributeColor = value; } }

        [DisplayName("Background Color"), Category("(All Types)")]
        [Description("The background color for all types.")]
        [DefaultValue(typeof (Color), "White")]
        public Color TypeBackgroundColor
        {
            get
            {
                var color = classBackgroundColor;

                if (structureBackgroundColor == color &&
                    interfaceBackgroundColor == color &&
                    enumBackgroundColor == color &&
                    delegateBackgroundColor == color)
                {
                    return color;
                }
                return Color.Empty;
            }
            set
            {
                if (value != Color.Empty)
                {
                    classBackgroundColor = value;
                    structureBackgroundColor = value;
                    interfaceBackgroundColor = value;
                    enumBackgroundColor = value;
                    delegateBackgroundColor = value;
                }
            }
        }

        [DisplayName("Border Color"), Category("(All Types)")]
        [Description("The border color for all types.")]
        [DefaultValue(typeof (Color), "Black")]
        public Color BorderColor
        {
            get
            {
                var color = classBorderColor;

                if (structureBorderColor == color &&
                    interfaceBorderColor == color &&
                    enumBorderColor == color &&
                    delegateBorderColor == color)
                {
                    return color;
                }
                return Color.Empty;
            }
            set
            {
                if (value != Color.Empty)
                {
                    classBorderColor = value;
                    structureBorderColor = value;
                    interfaceBorderColor = value;
                    enumBorderColor = value;
                    delegateBorderColor = value;
                }
            }
        }

        [DisplayName("Border Width"), Category("(All Types)")]
        [Description("The border width for all types.")]
        [DefaultValue(1)]
        public int? BorderWidth
        {
            get
            {
                var width = classBorderWidth;

                if (structureBorderWidth == width &&
                    interfaceBorderWidth == width &&
                    enumBorderWidth == width &&
                    delegateBorderWidth == width &&
                    abstractClassBorderWidth == width &&
                    sealedClassBorderWidth == width &&
                    staticClassBorderWidth == width)
                {
                    return width;
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    var width = value.Value < 1 ? 1 : value.Value;

                    classBorderWidth = width;
                    structureBorderWidth = width;
                    interfaceBorderWidth = width;
                    enumBorderWidth = width;
                    delegateBorderWidth = width;
                    abstractClassBorderWidth = width;
                    sealedClassBorderWidth = width;
                    staticClassBorderWidth = width;
                }
            }
        }

        [DisplayName("Dashed Border"), Category("(All Types)")]
        [Description("Whether the border for all types will be dashed.")]
        [DefaultValue(false)]
        public bool? IsBorderDashed
        {
            get
            {
                var dashed = isClassBorderDashed;

                if (isStructureBorderDashed == dashed &&
                    isInterfaceBorderDashed == dashed &&
                    isEnumBorderDashed == dashed &&
                    isDelegateBorderDashed == dashed &&
                    isAbstractClassBorderDashed == dashed &&
                    isSealedClassBorderDashed == dashed &&
                    isStaticClassBorderDashed == dashed)
                {
                    return dashed;
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    isClassBorderDashed = value.Value;
                    isStructureBorderDashed = value.Value;
                    isInterfaceBorderDashed = value.Value;
                    isEnumBorderDashed = value.Value;
                    isDelegateBorderDashed = value.Value;
                    isAbstractClassBorderDashed = value.Value;
                    isSealedClassBorderDashed = value.Value;
                    isStaticClassBorderDashed = value.Value;
                }
            }
        }

        [DisplayName("Header Alignment"), Category("(All Types)")]
        [Description("Specifies text alignment within the header compartment.")]
        [DefaultValue(ContentAlignment.MiddleCenter)]
        public ContentAlignment HeaderAlignment { get { return headerAlignment; } set { headerAlignment = value; } }

        [DisplayName("Header Color"), Category("(All Types)")]
        [Description("The background color of the header compartment for all types.")]
        [DefaultValue(typeof (Color), "White")]
        public Color HeaderColor
        {
            get
            {
                var color = classHeaderColor;

                if (structureHeaderColor == color &&
                    interfaceHeaderColor == color &&
                    enumHeaderColor == color &&
                    delegateHeaderColor == color)
                {
                    return color;
                }
                return Color.Empty;
            }
            set
            {
                if (value != Color.Empty)
                {
                    classHeaderColor = value;
                    structureHeaderColor = value;
                    interfaceHeaderColor = value;
                    enumHeaderColor = value;
                    delegateHeaderColor = value;
                }
            }
        }

        [DisplayName("Identifier Color"), Category("(All Types)")]
        [Description("The text color of the secondary text beside the type's name.")]
        [DefaultValue(typeof (Color), "Black")]
        public Color IdentifierColor { get { return identifierColor; } set { identifierColor = value; } }

        [DisplayName("Identifier Font"), Category("(All Types)")]
        [Description("The font of the secondary text beside the type's name.")]
        public Font IdentifierFont
        {
            get { return identifierFont; }
            set
            {
                if (value != null && identifierFont != value)
                {
                    identifierFont.Dispose();
                    identifierFont = value;
                }
            }
        }

        [DisplayName("Member Font"), Category("(All Types)")]
        [Description("The font of the type's members.")]
        public Font MemberFont
        {
            get { return memberFont; }
            set
            {
                if (value != null && memberFont != value)
                {
                    memberFont.Dispose();
                    staticMemberFont.Dispose();
                    abstractMemberFont.Dispose();

                    memberFont = value;
                    staticMemberFont = new Font(memberFont, memberFont.Style | FontStyle.Underline);
                    abstractMemberFont = new Font(memberFont, memberFont.Style | FontStyle.Italic);
                }
            }
        }

        [Browsable(false)]
        public Font StaticMemberFont { get { return staticMemberFont; } }

        [Browsable(false)]
        public Font AbstractMemberFont { get { return abstractMemberFont; } }

        [DisplayName("Name Color"), Category("(All Types)")]
        [Description("The text color of the type's name.")]
        [DefaultValue(typeof (Color), "Black")]
        public Color NameColor { get { return nameColor; } set { nameColor = value; } }

        [DisplayName("Name Font"), Category("(All Types)")]
        [Description("The font of the type's name.")]
        public Font NameFont
        {
            get { return nameFont; }
            set
            {
                if (value != null && nameFont != value)
                {
                    nameFont.Dispose();
                    abstractNameFont.Dispose();

                    nameFont = value;
                    abstractNameFont = new Font(nameFont, nameFont.Style | FontStyle.Italic);
                }
            }
        }

        [Browsable(false)]
        public Font AbstractNameFont { get { return abstractNameFont; } }

        [DisplayName("Operation Color"), Category("(All Types)")]
        [Description("The text color of a type's operations.")]
        [DefaultValue(typeof (Color), "Black")]
        public Color OperationColor { get { return operationColor; } set { operationColor = value; } }

        [DisplayName("Rounding Size"), Category("(All Types)")]
        [Description("The rounding size of the corners for all types.")]
        [DefaultValue(0)]
        public int? RoundingSize
        {
            get
            {
                var size = classRoundingSize;

                if (structureRoundingSize == size &&
                    interfaceRoundingSize == size &&
                    enumRoundingSize == size &&
                    delegateRoundingSize == size)
                {
                    return size;
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    var size = value.Value < 0 ? 0 : value.Value;

                    classRoundingSize = size;
                    structureRoundingSize = size;
                    interfaceRoundingSize = size;
                    enumRoundingSize = size;
                    delegateRoundingSize = size;
                }
            }
        }

        [DisplayName("Shadow"), Category("(All Types)")]
        [Description("The offset of the shadow for all entities.")]
        [DefaultValue(typeof (Size), "4, 4")]
        public Size ShadowOffset
        {
            get { return shadowOffset; }
            set
            {
                if (value.Width < 0)
                    value.Width = 0;
                if (value.Height < 0)
                    value.Height = 0;

                shadowOffset = value;
            }
        }

        [DisplayName("Shadow Color"), Category("(All Types)")]
        [Description("The color of the shadow.")]
        [DefaultValue(typeof (Color), "70,0,0,0")]
        public Color ShadowColor { get { return shadowColor; } set { shadowColor = value; } }

        [DisplayName("Show Signature"), Category("(All Types)")]
        [Description("Whether to show detailed type description within the header compartment.")]
        [DefaultValue(false)]
        public bool ShowSignature
        {
            get { return showSignature; }
            set
            {
                if (value && ShowStereotype)
                    ShowStereotype = false;
                showSignature = value;
            }
        }

        [DisplayName("Show Stereotype"), Category("(All Types)")]
        [Description("Whether to show stereotype within the header compartment.")]
        [DefaultValue(true)]
        public bool ShowStereotype
        {
            get { return showStereotypes; }
            set
            {
                if (value && ShowSignature)
                    ShowSignature = false;
                showStereotypes = value;
            }
        }

        [DisplayName("Header Gradient Style"), Category("(All Types)")]
        [Description("The direction of the gradient header color in all types.")]
        [DefaultValue(GradientStyle.None)]
        public GradientStyle? HeaderGradientStyle
        {
            get
            {
                var value = classGradientHeaderStyle;

                if (structureGradientHeaderStyle == value &&
                    interfaceGradientHeaderStyle == value &&
                    enumGradientHeaderStyle == value &&
                    delegateGradientHeaderStyle == value)
                {
                    return value;
                }
                return null;
            }
            set
            {
                if (value.HasValue)
                {
                    classGradientHeaderStyle = value.Value;
                    structureGradientHeaderStyle = value.Value;
                    interfaceGradientHeaderStyle = value.Value;
                    enumGradientHeaderStyle = value.Value;
                    delegateGradientHeaderStyle = value.Value;
                }
            }
        }

        [DisplayName("Use Icons"), Category("(All Types)")]
        [Description("Whether to use member type icons in the " +
                     "attributes and operations compartments.")]
        [DefaultValue(false)]
        public bool UseIcons { get { return useIcons; } set { useIcons = value; } }

        #endregion

        #region Class properties

        [DisplayName("Background Color"), Category("Class")]
        [Description("The background color for the class type.")]
        [DefaultValue(typeof (Color), "White")]
        public Color ClassBackgroundColor { get { return classBackgroundColor; } set { classBackgroundColor = value; } }

        [DisplayName("Border Color"), Category("Class")]
        [Description("The border color for the class type.")]
        [DefaultValue(typeof (Color), "Black")]
        public Color ClassBorderColor { get { return classBorderColor; } set { classBorderColor = value; } }

        [DisplayName("Border Width"), Category("Class")]
        [Description("The border width for the class type.")]
        [DefaultValue(1)]
        public int ClassBorderWidth
        {
            get { return classBorderWidth; }
            set
            {
                if (value < 1)
                    classBorderWidth = 1;
                else
                    classBorderWidth = value;
            }
        }

        [DisplayName("Dashed Border"), Category("Class")]
        [Description("Whether the border for the class type will be dashed.")]
        [DefaultValue(false)]
        public bool IsClassBorderDashed { get { return isClassBorderDashed; } set { isClassBorderDashed = value; } }

        [DisplayName("Header Color"), Category("Class")]
        [Description("The background color of the header compartment for the class type.")]
        [DefaultValue(typeof (Color), "White")]
        public Color ClassHeaderColor { get { return classHeaderColor; } set { classHeaderColor = value; } }

        [DisplayName("Rounding Size"), Category("Class")]
        [Description("The rounding size of the corners for the class type.")]
        [DefaultValue(0)]
        public int ClassRoundingSize
        {
            get { return classRoundingSize; }
            set
            {
                if (value < 0)
                    classRoundingSize = 0;
                else
                    classRoundingSize = value;
            }
        }

        [DisplayName("Header Gradient Style"), Category("Class")]
        [Description("The direction of the gradient header color in class types.")]
        [DefaultValue(GradientStyle.None)]
        public GradientStyle ClassGradientHeaderStyle
        {
            get { return classGradientHeaderStyle; }
            set { classGradientHeaderStyle = value; }
        }

        #endregion

        #region Modified class properties

        [DisplayName("Border Width"), Category("Abstract Class")]
        [Description("The border width for abstract classes.")]
        [DefaultValue(1)]
        public int AbstractClassBorderWidth
        {
            get { return abstractClassBorderWidth; }
            set
            {
                if (value < 1)
                    abstractClassBorderWidth = 1;
                else
                    abstractClassBorderWidth = value;
            }
        }

        [DisplayName("Dashed Border"), Category("Abstract Class")]
        [Description("Whether the border for abstract classes will be dashed.")]
        [DefaultValue(false)]
        public bool IsAbstractClassBorderDashed
        {
            get { return isAbstractClassBorderDashed; }
            set { isAbstractClassBorderDashed = value; }
        }

        [DisplayName("Border Width"), Category("Sealed Class")]
        [Description("The border width for sealed classes.")]
        [DefaultValue(1)]
        public int SealedClassBorderWidth
        {
            get { return sealedClassBorderWidth; }
            set
            {
                if (value < 1)
                    sealedClassBorderWidth = 1;
                else
                    sealedClassBorderWidth = value;
            }
        }

        [DisplayName("Dashed Border"), Category("Sealed Class")]
        [Description("Whether the border for sealed classes will be dashed.")]
        [DefaultValue(false)]
        public bool IsSealedClassBorderDashed
        {
            get { return isSealedClassBorderDashed; }
            set { isSealedClassBorderDashed = value; }
        }

        [DisplayName("Border Width"), Category("Static Class")]
        [Description("The border width for static classes.")]
        [DefaultValue(1)]
        public int StaticClassBorderWidth
        {
            get { return staticClassBorderWidth; }
            set
            {
                if (value < 1)
                    staticClassBorderWidth = 1;
                else
                    staticClassBorderWidth = value;
            }
        }

        [DisplayName("Dashed Border"), Category("Static Class")]
        [Description("Whether the border for static classes will be dashed.")]
        [DefaultValue(false)]
        public bool IsStaticClassBorderDashed
        {
            get { return isStaticClassBorderDashed; }
            set { isStaticClassBorderDashed = value; }
        }

        #endregion

        #region Structure properties

        [DisplayName("Background Color"), Category("Structure")]
        [Description("The background color for the structure type.")]
        [DefaultValue(typeof (Color), "White")]
        public Color StructureBackgroundColor
        {
            get { return structureBackgroundColor; }
            set { structureBackgroundColor = value; }
        }

        [DisplayName("Border Color"), Category("Structure")]
        [Description("The border color for the structure type.")]
        [DefaultValue(typeof (Color), "Black")]
        public Color StructureBorderColor { get { return structureBorderColor; } set { structureBorderColor = value; } }

        [DisplayName("Border Width"), Category("Structure")]
        [Description("The border width for the structure type.")]
        [DefaultValue(1)]
        public int StructureBorderWidth
        {
            get { return structureBorderWidth; }
            set
            {
                if (value < 1)
                    structureBorderWidth = 1;
                else
                    structureBorderWidth = value;
            }
        }

        [DisplayName("Dashed Border"), Category("Structure")]
        [Description("Whether the border for the structure type will be dashed.")]
        [DefaultValue(false)]
        public bool IsStructureBorderDashed
        {
            get { return isStructureBorderDashed; }
            set { isStructureBorderDashed = value; }
        }

        [DisplayName("Header Color"), Category("Structure")]
        [Description("The background color of the header compartment for the structure type.")]
        [DefaultValue(typeof (Color), "White")]
        public Color StructureHeaderColor { get { return structureHeaderColor; } set { structureHeaderColor = value; } }

        [DisplayName("Rounding Size"), Category("Structure")]
        [Description("The rounding size of the corners for the structure type.")]
        [DefaultValue(0)]
        public int StructureRoundingSize
        {
            get { return structureRoundingSize; }
            set
            {
                if (value < 0)
                    structureRoundingSize = 0;
                else
                    structureRoundingSize = value;
            }
        }

        [DisplayName("Header Gradient Style"), Category("Structure")]
        [Description("The direction of the gradient header color in structure types.")]
        [DefaultValue(GradientStyle.None)]
        public GradientStyle StructureGradientHeaderStyle
        {
            get { return structureGradientHeaderStyle; }
            set { structureGradientHeaderStyle = value; }
        }

        #endregion

        #region Interface properties

        [DisplayName("Background Color"), Category("Interface")]
        [Description("The background color for the interface type.")]
        [DefaultValue(typeof (Color), "White")]
        public Color InterfaceBackgroundColor
        {
            get { return interfaceBackgroundColor; }
            set { interfaceBackgroundColor = value; }
        }

        [DisplayName("Border Color"), Category("Interface")]
        [Description("The border color for the interface type.")]
        [DefaultValue(typeof (Color), "Black")]
        public Color InterfaceBorderColor { get { return interfaceBorderColor; } set { interfaceBorderColor = value; } }

        [DisplayName("Border Width"), Category("Interface")]
        [Description("The border width for the interface type.")]
        [DefaultValue(1)]
        public int InterfaceBorderWidth
        {
            get { return interfaceBorderWidth; }
            set
            {
                if (value < 1)
                    interfaceBorderWidth = 1;
                else
                    interfaceBorderWidth = value;
            }
        }

        [DisplayName("Dashed Border"), Category("Interface")]
        [Description("Whether the border for the interface type will be dashed.")]
        [DefaultValue(false)]
        public bool IsInterfaceBorderDashed
        {
            get { return isInterfaceBorderDashed; }
            set { isInterfaceBorderDashed = value; }
        }

        [DisplayName("Header Color"), Category("Interface")]
        [Description("The background color of the header compartment for the interface type.")]
        [DefaultValue(typeof (Color), "White")]
        public Color InterfaceHeaderColor { get { return interfaceHeaderColor; } set { interfaceHeaderColor = value; } }

        [DisplayName("Rounding Size"), Category("Interface")]
        [Description("The rounding size of the corners for the interface type.")]
        [DefaultValue(0)]
        public int InterfaceRoundingSize
        {
            get { return interfaceRoundingSize; }
            set
            {
                if (value < 0)
                    interfaceRoundingSize = 0;
                else
                    interfaceRoundingSize = value;
            }
        }

        [DisplayName("Header Gradient Style"), Category("Interface")]
        [Description("The direction of the gradient header color in interface types.")]
        [DefaultValue(GradientStyle.None)]
        public GradientStyle InterfaceGradientHeaderStyle
        {
            get { return interfaceGradientHeaderStyle; }
            set { interfaceGradientHeaderStyle = value; }
        }

        #endregion

        #region Enum properties

        [DisplayName("Background Color"), Category("Enum")]
        [Description("The background color for the enum type.")]
        [DefaultValue(typeof (Color), "White")]
        public Color EnumBackgroundColor { get { return enumBackgroundColor; } set { enumBackgroundColor = value; } }

        [DisplayName("Border Color"), Category("Enum")]
        [Description("The border color for the enum type.")]
        [DefaultValue(typeof (Color), "Black")]
        public Color EnumBorderColor { get { return enumBorderColor; } set { enumBorderColor = value; } }

        [DisplayName("Border Width"), Category("Enum")]
        [Description("The border width for the enum type")]
        [DefaultValue(1)]
        public int EnumBorderWidth
        {
            get { return enumBorderWidth; }
            set
            {
                if (value < 1)
                    enumBorderWidth = 1;
                else
                    enumBorderWidth = value;
            }
        }

        [DisplayName("Dashed Border"), Category("Enum")]
        [Description("Whether the border for the enum type will be dashed.")]
        [DefaultValue(false)]
        public bool IsEnumBorderDashed { get { return isEnumBorderDashed; } set { isEnumBorderDashed = value; } }

        [DisplayName("Header Color"), Category("Enum")]
        [Description("The background color of the header compartment for the enum type.")]
        [DefaultValue(typeof (Color), "White")]
        public Color EnumHeaderColor { get { return enumHeaderColor; } set { enumHeaderColor = value; } }

        [DisplayName("Item Color"), Category("Enum")]
        [Description("The text color of an enumerator item.")]
        [DefaultValue(typeof (Color), "Black")]
        public Color EnumItemColor { get { return enumItemColor; } set { enumItemColor = value; } }

        [DisplayName("Rounding Size"), Category("Enum")]
        [Description("The rounding size of the corners for the enum type.")]
        [DefaultValue(0)]
        public int EnumRoundingSize
        {
            get { return enumRoundingSize; }
            set
            {
                if (value < 0)
                    enumRoundingSize = 0;
                else
                    enumRoundingSize = value;
            }
        }

        [DisplayName("Header Gradient Style"), Category("Enum")]
        [Description("The direction of the gradient header color in enum types.")]
        [DefaultValue(GradientStyle.None)]
        public GradientStyle EnumGradientHeaderStyle
        {
            get { return enumGradientHeaderStyle; }
            set { enumGradientHeaderStyle = value; }
        }

        #endregion

        #region Delegate properties

        [DisplayName("Background Color"), Category("Delegate")]
        [Description("The background color for the delegate type.")]
        [DefaultValue(typeof (Color), "White")]
        public Color DelegateBackgroundColor
        {
            get { return delegateBackgroundColor; }
            set { delegateBackgroundColor = value; }
        }

        [DisplayName("Border Color"), Category("Delegate")]
        [Description("The border color for the delegate type.")]
        [DefaultValue(typeof (Color), "Black")]
        public Color DelegateBorderColor { get { return delegateBorderColor; } set { delegateBorderColor = value; } }

        [DisplayName("Border Width"), Category("Delegate")]
        [Description("The border width for the delegate type.")]
        [DefaultValue(1)]
        public int DelegateBorderWidth
        {
            get { return delegateBorderWidth; }
            set
            {
                if (value < 1)
                    delegateBorderWidth = 1;
                else
                    delegateBorderWidth = value;
            }
        }

        [DisplayName("Dashed Border"), Category("Delegate")]
        [Description("Whether the border for the delegate type will be dashed.")]
        [DefaultValue(false)]
        public bool IsDelegateBorderDashed
        {
            get { return isDelegateBorderDashed; }
            set { isDelegateBorderDashed = value; }
        }

        [DisplayName("Header Color"), Category("Delegate")]
        [Description("The background color of the header compartment for the delegate type.")]
        [DefaultValue(typeof (Color), "White")]
        public Color DelegateHeaderColor { get { return delegateHeaderColor; } set { delegateHeaderColor = value; } }

        [DisplayName("Parameter Color"), Category("Delegate")]
        [Description("The text color of a delegate's parameters.")]
        [DefaultValue(typeof (Color), "Black")]
        public Color DelegateParameterColor
        {
            get { return delegateParameterColor; }
            set { delegateParameterColor = value; }
        }

        [DisplayName("Rounding Size"), Category("Delegate")]
        [Description("The rounding size of the corners for the delegate type.")]
        [DefaultValue(0)]
        public int DelegateRoundingSize
        {
            get { return delegateRoundingSize; }
            set
            {
                if (value < 0)
                    delegateRoundingSize = 0;
                else
                    delegateRoundingSize = value;
            }
        }

        [DisplayName("Header Gradient Style"), Category("Delegate")]
        [Description("The direction of the gradient header color in delegate types.")]
        [DefaultValue(GradientStyle.None)]
        public GradientStyle DelegateGradientHeaderStyle
        {
            get { return delegateGradientHeaderStyle; }
            set { delegateGradientHeaderStyle = value; }
        }

        #endregion

        #region Comment properties

        [DisplayName("Background Color"), Category("Comment")]
        [Description("The background color for the comment.")]
        [DefaultValue(typeof (Color), "White")]
        public Color CommentBackColor { get { return commentBackColor; } set { commentBackColor = value; } }

        [DisplayName("Border Color"), Category("Comment")]
        [Description("The border color for the comment.")]
        [DefaultValue(typeof (Color), "Black")]
        public Color CommentBorderColor { get { return commentBorderColor; } set { commentBorderColor = value; } }

        [DisplayName("Border Width"), Category("Comment")]
        [Description("The border width for the comment.")]
        [DefaultValue(1)]
        public int CommentBorderWidth
        {
            get { return commentBorderWidth; }
            set
            {
                if (value < 1)
                    commentBorderWidth = 1;
                else
                    commentBorderWidth = value;
            }
        }

        [DisplayName("Dashed Border"), Category("Comment")]
        [Description("Whether the border for the comment will be dashed.")]
        [DefaultValue(false)]
        public bool IsCommentBorderDashed
        {
            get { return isCommentBorderDashed; }
            set { isCommentBorderDashed = value; }
        }

        [DisplayName("Font"), Category("Comment")]
        [Description("The font of the displayed text for the comment.")]
        public Font CommentFont
        {
            get { return commentFont; }
            set
            {
                if (value != null && commentFont != value)
                {
                    commentFont.Dispose();
                    commentFont = value;
                }
            }
        }

        [DisplayName("Text Color"), Category("Comment")]
        [Description("The text color for the comment.")]
        [DefaultValue(typeof (Color), "Black")]
        public Color CommentTextColor { get { return textColor; } set { textColor = value; } }

        #endregion

        #region Relationship properties

        [DisplayName("Dash Size"), Category("(Relationship)")]
        [Description("The lengths of alternating dashes and spaces in dashed lines.")]
        [DefaultValue(5)]
        public int RelationshipDashSize
        {
            get { return relationshipDashSize; }
            set
            {
                if (value < 1)
                    relationshipDashSize = 1;
                else
                    relationshipDashSize = value;
            }
        }

        [DisplayName("Color"), Category("(Relationship)")]
        [Description("The line color for the relationship.")]
        [DefaultValue(typeof (Color), "Black")]
        public Color RelationshipColor { get { return relationshipColor; } set { relationshipColor = value; } }

        [DisplayName("Font Color"), Category("(Relationship)")]
        [Description("The color of relationship texts.")]
        [DefaultValue(typeof (Color), "Black")]
        public Color RelationshipTextColor
        {
            get { return relationshipTextColor; }
            set { relationshipTextColor = value; }
        }

        [DisplayName("Font"), Category("(Relationship)")]
        [Description("The font of the type's name.")]
        public Font RelationshipTextFont
        {
            get { return relationshipTextFont; }
            set
            {
                if (value != null && relationshipTextFont != value)
                {
                    relationshipTextFont.Dispose();
                    relationshipTextFont = value;
                }
            }
        }

        [DisplayName("Width"), Category("(Relationship)")]
        [Description("The width of the relationship line.")]
        [DefaultValue(1)]
        public int RelationshipWidth
        {
            get { return relationshipWidth; }
            set
            {
                if (value < 1)
                    relationshipWidth = 1;
                else
                    relationshipWidth = value;
            }
        }

        #endregion
    }
}