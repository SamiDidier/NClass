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
using System.Text;
using System.Text.RegularExpressions;
using NClass.Core;
using NClass.Translations;

namespace NClass.CSharp
{
    internal sealed class CSharpProperty : Property
    {
        private const string AccessorAccessPattern = @"(protected\s+internal\s+|" +
                                                     @"internal\s+protected\s+|internal\s+|protected\s+|private\s+)";

        // [<access>] [<modifiers>] <type> <name>["["<args>"]"]
        // ["{"] [[<getaccess>] get[;]] [[<setaccess>] set[;]] ["}"]
        private const string PropertyPattern =
            @"^\s*" + CSharpLanguage.AccessPattern + CSharpLanguage.OperationModifiersPattern +
            @"(?<type>" + CSharpLanguage.GenericTypePattern2 + @")\s+" +
            @"((?<name>" + CSharpLanguage.GenericOperationNamePattern +
            @")|(?<name>this)\s*\[(?<args>.+)\])" +
            @"\s*{?\s*(?<get>(?<getaccess>" + AccessorAccessPattern + @")?get(\s*;|\s|$))?" +
            @"\s*(?<set>(?<setaccess>" + AccessorAccessPattern + @")?set(\s*;)?)?\s*}?" +
            CSharpLanguage.DeclarationEnding;

        private const string PropertyNamePattern =
            @"^\s*(?<name>" + CSharpLanguage.GenericOperationNamePattern + @")\s*$";

        private static readonly Regex propertyRegex = new Regex(PropertyPattern);
        private static readonly Regex nameRegex = new Regex(PropertyNamePattern, RegexOptions.ExplicitCapture);

        private bool isExplicitImplementation;

        /// <exception cref="ArgumentNullException">
        ///     <paramref name="parent" /> is null.
        /// </exception>
        internal CSharpProperty(CompositeType parent)
            : this("NewProperty", parent)
        {
        }

        /// <exception cref="BadSyntaxException">
        ///     The <paramref name="name" /> does not fit to the syntax.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     The language of <paramref name="parent" /> does not equal.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="parent" /> is null.
        /// </exception>
        internal CSharpProperty(string name, CompositeType parent)
            : base(name, parent)
        {
        }

        /// <exception cref="BadSyntaxException">
        ///     The <paramref name="value" /> does not fit to the syntax.
        /// </exception>
        public override string Name
        {
            get { return base.Name; }
            set
            {
                var match = nameRegex.Match(value);

                if (match.Success)
                {
                    RaiseChangedEvent = false;
                    try
                    {
                        IsExplicitImplementation = match.Groups["namedot"].Success;
                        ValidName = match.Groups["name"].Value;

                        if (Name == "this")
                        {
                            // Indexer
                            if (!HasParameter)
                                ArgumentList.Add("int index");
                        }
                        else if (HasParameter)
                        {
                            // Not an indexer
                            ArgumentList.Clear();
                        }
                    }
                    finally
                    {
                        RaiseChangedEvent = true;
                    }
                }
                else
                {
                    throw new BadSyntaxException(Strings.ErrorInvalidName);
                }
            }
        }

        /// <exception cref="BadSyntaxException">
        ///     The <paramref name="value" /> does not fit to the syntax.
        /// </exception>
        public override string Type
        {
            get { return base.Type; }
            set
            {
                if (value == "void")
                    throw new BadSyntaxException(string.Format(Strings.ErrorType, "void"));

                base.Type = value;
            }
        }

        protected override string DefaultType { get { return "int"; } }

        public bool IsIndexer { get { return Name == "this"; } }

        public override bool IsStatic
        {
            get { return base.IsStatic; }
            set
            {
                if (value && IsIndexer)
                {
                    throw new BadSyntaxException(
                        Strings.ErrorStaticIndexer);
                }
                base.IsStatic = value;
            }
        }

        /// <exception cref="BadSyntaxException">
        ///     Cannot set access visibility.
        /// </exception>
        public override AccessModifier AccessModifier
        {
            get { return base.AccessModifier; }
            set
            {
                if (value != AccessModifier.Default && IsExplicitImplementation)
                {
                    throw new BadSyntaxException(
                        Strings.ErrorExplicitImplementationAccess);
                }
                if (value != AccessModifier.Default && Parent is InterfaceType)
                {
                    throw new BadSyntaxException(
                        Strings.ErrorInterfaceMemberAccess);
                }

                base.AccessModifier = value;
            }
        }

        public override bool IsAccessModifiable
        {
            get
            {
                return base.IsAccessModifiable &&
                       !(Parent is InterfaceType) && !IsExplicitImplementation;
            }
        }

        /// <exception cref="BadSyntaxException">
        ///     Interfaces cannot implement properties.
        /// </exception>
        public bool IsExplicitImplementation
        {
            get { return isExplicitImplementation; }
            private set
            {
                if (isExplicitImplementation != value)
                {
                    if (value && !(Parent is IInterfaceImplementer))
                        throw new BadSyntaxException(Strings.ErrorExplicitImplementation);

                    try
                    {
                        RaiseChangedEvent = false;
                        if (value)
                            AccessModifier = AccessModifier.Default;
                        isExplicitImplementation = value;
                        Changed();
                    }
                    finally
                    {
                        RaiseChangedEvent = true;
                    }
                }
            }
        }

        public override Language Language { get { return CSharpLanguage.Instance; } }

        /// <exception cref="BadSyntaxException">
        ///     The <paramref name="declaration" /> does not fit to the syntax.
        /// </exception>
        public override void InitFromString(string declaration)
        {
            var match = propertyRegex.Match(declaration);
            RaiseChangedEvent = false;

            try
            {
                if (match.Success)
                {
                    ClearModifiers();
                    ReadAccess = AccessModifier.Default;
                    WriteAccess = AccessModifier.Default;

                    var nameGroup = match.Groups["name"];
                    var typeGroup = match.Groups["type"];
                    var accessGroup = match.Groups["access"];
                    var modifierGroup = match.Groups["modifier"];
                    var nameDotGroup = match.Groups["namedot"];
                    var argsGroup = match.Groups["args"];
                    var getGroup = match.Groups["get"];
                    var setGroup = match.Groups["set"];
                    var getAccessGroup = match.Groups["getaccess"];
                    var setAccessGroup = match.Groups["setaccess"];

                    ArgumentList.InitFromString(argsGroup.Value);

                    // Validating identifier's name
                    if ((nameGroup.Value != "this" || !HasParameter) &&
                        CSharpLanguage.Instance.IsForbiddenName(nameGroup.Value))
                    {
                        throw new BadSyntaxException(Strings.ErrorInvalidName);
                    }
                    else
                    {
                        ValidName = nameGroup.Value;
                    }

                    // Validating type's name
                    if (CSharpLanguage.Instance.IsForbiddenTypeName(typeGroup.Value))
                        throw new BadSyntaxException(Strings.ErrorInvalidTypeName);
                    else
                        ValidType = typeGroup.Value;

                    IsExplicitImplementation = nameDotGroup.Success;
                    AccessModifier = Language.TryParseAccessModifier(accessGroup.Value);
                    IsReadonly = getGroup.Success && !setGroup.Success;
                    IsWriteonly = !getGroup.Success && setGroup.Success;
                    ReadAccess = Language.TryParseAccessModifier(getAccessGroup.Value);
                    WriteAccess = Language.TryParseAccessModifier(setAccessGroup.Value);

                    foreach (Capture modifierCapture in modifierGroup.Captures)
                    {
                        if (modifierCapture.Value == "static")
                            IsStatic = true;
                        if (modifierCapture.Value == "virtual")
                            IsVirtual = true;
                        if (modifierCapture.Value == "abstract")
                            IsAbstract = true;
                        if (modifierCapture.Value == "override")
                            IsOverride = true;
                        if (modifierCapture.Value == "sealed")
                            IsSealed = true;
                        if (modifierCapture.Value == "new")
                            IsHider = true;
                    }
                }
                else
                {
                    throw new BadSyntaxException(Strings.ErrorInvalidDeclaration);
                }
            }
            finally
            {
                RaiseChangedEvent = true;
            }
        }

        public override string GetUmlDescription(bool getType,
                                                 bool getParameters,
                                                 bool getParameterNames,
                                                 bool getInitValue)
        {
            var builder = new StringBuilder(50);

            builder.AppendFormat(Name);
            if (getParameters)
            {
                if (HasParameter)
                {
                    builder.Append("[");
                    for (var i = 0; i < ArgumentList.Count; i++)
                    {
                        builder.Append(ArgumentList[i]);
                        if (i < ArgumentList.Count - 1)
                            builder.Append(", ");
                    }
                    builder.Append("]");
                }

                if (IsReadonly)
                    builder.Append(" { get; }");
                else if (IsWriteonly)
                    builder.Append(" { set; }");
                else
                    builder.Append(" { get; set; }");
            }
            if (getType)
                builder.AppendFormat(" : {0}", Type);

            return builder.ToString();
        }

        public override string GetDeclaration()
        {
            return GetDeclarationLine(false);
        }

        public string GetDeclarationLine(bool showAccessors)
        {
            var builder = new StringBuilder(50);

            if (AccessModifier != AccessModifier.Default)
            {
                builder.Append(Language.GetAccessString(AccessModifier, true));
                builder.Append(" ");
            }

            if (IsHider)
                builder.Append("new ");
            if (IsStatic)
                builder.Append("static ");
            if (IsVirtual)
                builder.Append("virtual ");
            if (IsAbstract)
                builder.Append("abstract ");
            if (IsSealed)
                builder.Append("sealed ");
            if (IsOverride)
                builder.Append("override ");

            builder.AppendFormat("{0} {1}", Type, Name);

            if (HasParameter)
            {
                builder.Append("[");
                for (var i = 0; i < ArgumentList.Count; i++)
                {
                    builder.Append(ArgumentList[i]);
                    if (i < ArgumentList.Count - 1)
                        builder.Append(", ");
                }
                builder.Append("]");
            }

            if (showAccessors)
            {
                builder.Append(" { ");
                if (!IsWriteonly)
                {
                    if (ReadAccess != AccessModifier.Default)
                    {
                        builder.Append(Language.GetAccessString(ReadAccess, true));
                        builder.Append(" get; ");
                    }
                    else
                    {
                        builder.Append("get; ");
                    }
                }
                if (!IsReadonly)
                {
                    if (WriteAccess != AccessModifier.Default)
                    {
                        builder.Append(Language.GetAccessString(WriteAccess, true));
                        builder.Append(" set; ");
                    }
                    else
                    {
                        builder.Append("set; ");
                    }
                }
                builder.Append("}");
            }

            return builder.ToString();
        }

        public override string ToString()
        {
            return GetDeclarationLine(true);
        }

        public override Operation Clone(CompositeType newParent)
        {
            var property = new CSharpProperty(newParent);
            property.CopyFrom(this);
            return property;
        }
    }
}