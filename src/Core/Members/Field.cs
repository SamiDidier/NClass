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

namespace NClass.Core
{
    public abstract class Field : Member
    {
        private string initialValue;

        /// <exception cref="BadSyntaxException">
        ///     The <paramref name="name" /> does not fit to the syntax.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     The language of <paramref name="parent" /> does not equal.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="parent" /> is null.
        /// </exception>
        protected Field(string name, CompositeType parent)
            : base(name, parent)
        {
        }

        public sealed override MemberType MemberType { get { return MemberType.Field; } }

        public override string Name
        {
            get { return base.Name; }
            set { ValidName = Language.GetValidName(value, false); }
        }

        /// <exception cref="BadSyntaxException">
        ///     Cannot set access visibility.
        /// </exception>
        public override AccessModifier AccessModifier
        {
            get { return base.AccessModifier; }
            set
            {
                if (value == AccessModifier)
                    return;

                var previousAccess = base.AccessModifier;

                try
                {
                    RaiseChangedEvent = false;

                    base.AccessModifier = value;
                    Language.ValidateField(this);
                }
                catch
                {
                    base.AccessModifier = previousAccess;
                    throw;
                }
                finally
                {
                    RaiseChangedEvent = true;
                }
            }
        }

        public FieldModifier Modifier { get; private set; } = FieldModifier.None;

        public sealed override bool IsModifierless { get { return Modifier == FieldModifier.None; } }

        /// <exception cref="BadSyntaxException">
        ///     Cannot set static modifier.
        /// </exception>
        public override bool IsStatic
        {
            get { return (Modifier & FieldModifier.Static) != 0; }
            set
            {
                if (value == IsStatic)
                    return;

                var previousModifier = Modifier;

                try
                {
                    if (value)
                        Modifier |= FieldModifier.Static;
                    else
                        Modifier &= ~FieldModifier.Static;
                    Language.ValidateField(this);
                    Changed();
                }
                catch
                {
                    Modifier = previousModifier;
                    throw;
                }
            }
        }

        /// <exception cref="BadSyntaxException">
        ///     Cannot set hider modifier.
        /// </exception>
        public override bool IsHider
        {
            get { return (Modifier & FieldModifier.Hider) != 0; }
            set
            {
                if (value == IsHider)
                    return;

                var previousModifier = Modifier;

                try
                {
                    if (value)
                        Modifier |= FieldModifier.Hider;
                    else
                        Modifier &= ~FieldModifier.Hider;
                    Language.ValidateField(this);
                    Changed();
                }
                catch
                {
                    Modifier = previousModifier;
                    throw;
                }
            }
        }

        /// <exception cref="BadSyntaxException">
        ///     Cannot set readonly modifier.
        /// </exception>
        public virtual bool IsReadonly
        {
            get { return (Modifier & FieldModifier.Readonly) != 0; }
            set
            {
                if (value == IsReadonly)
                    return;

                var previousModifier = Modifier;

                try
                {
                    if (value)
                        Modifier |= FieldModifier.Readonly;
                    else
                        Modifier &= ~FieldModifier.Readonly;
                    Language.ValidateField(this);
                    Changed();
                }
                catch
                {
                    Modifier = previousModifier;
                    throw;
                }
            }
        }

        /// <exception cref="BadSyntaxException">
        ///     Cannot set constant modifier.
        /// </exception>
        public virtual bool IsConstant
        {
            get { return (Modifier & FieldModifier.Constant) != 0; }
            set
            {
                if (value == IsConstant)
                    return;

                var previousModifier = Modifier;

                try
                {
                    if (value)
                        Modifier |= FieldModifier.Constant;
                    else
                        Modifier &= ~FieldModifier.Constant;
                    Language.ValidateField(this);
                    Changed();
                }
                catch
                {
                    Modifier = previousModifier;
                    throw;
                }
            }
        }

        /// <exception cref="BadSyntaxException">
        ///     Cannot set volatile modifier.
        /// </exception>
        public virtual bool IsVolatile
        {
            get { return (Modifier & FieldModifier.Volatile) != 0; }
            set
            {
                if (value == IsVolatile)
                    return;

                var previousModifier = Modifier;

                try
                {
                    if (value)
                        Modifier |= FieldModifier.Volatile;
                    else
                        Modifier &= ~FieldModifier.Volatile;
                    Language.ValidateField(this);
                    Changed();
                }
                catch
                {
                    Modifier = previousModifier;
                    throw;
                }
            }
        }

        public virtual string InitialValue
        {
            get { return initialValue; }
            set
            {
                if (initialValue != value &&
                    (!string.IsNullOrEmpty(value) || !string.IsNullOrEmpty(initialValue)))
                {
                    initialValue = value;
                    Changed();
                }
            }
        }

        public bool HasInitialValue { get { return !string.IsNullOrEmpty(InitialValue); } }

        public virtual void ClearModifiers()
        {
            if (Modifier != FieldModifier.None)
            {
                Modifier = FieldModifier.None;
                Changed();
            }
        }

        public sealed override string GetUmlDescription(bool getType,
                                                        bool getParameters,
                                                        bool getParameterNames,
                                                        bool getInitValue)
        {
            var builder = new StringBuilder(50);

            builder.Append(Name);
            if (getType)
                builder.AppendFormat(": {0}", Type);
            if (getInitValue && HasInitialValue)
                builder.AppendFormat(" = {0}", InitialValue);

            return builder.ToString();
        }

        protected override void CopyFrom(Member member)
        {
            base.CopyFrom(member);

            var field = (Field) member;
            Modifier = field.Modifier;
            initialValue = field.initialValue;
        }

        protected internal abstract Field Clone(CompositeType newParent);
    }
}