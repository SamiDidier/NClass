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

namespace NClass.Core
{
    public abstract class Operation : Member
    {
        private ArgumentList argumentList;

        /// <exception cref="BadSyntaxException">
        ///     The <paramref name="name" /> does not fit to the syntax.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     The language of <paramref name="parent" /> does not equal.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="parent" /> is null.
        /// </exception>
        protected Operation(string name, CompositeType parent)
            : base(name, parent)
        {
            argumentList = Language.CreateParameterCollection();
        }

        public bool HasParameter { get { return ArgumentList != null && ArgumentList.Count > 0; } }

        public ArgumentList ArgumentList
        {
            protected get { return argumentList; }
            set
            {
                if (argumentList.Equals(value))
                    return;

                argumentList = value;
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
                if (value == AccessModifier)
                    return;

                var previousAccess = base.AccessModifier;

                try
                {
                    RaiseChangedEvent = false;

                    base.AccessModifier = value;
                    Language.ValidateOperation(this);
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

        public OperationModifier Modifier { get; private set; } = OperationModifier.None;

        public sealed override bool IsModifierless { get { return Modifier == OperationModifier.None; } }

        /// <exception cref="BadSyntaxException">
        ///     Cannot set static modifier.
        /// </exception>
        public override bool IsStatic
        {
            get { return (Modifier & OperationModifier.Static) != 0; }
            set
            {
                if (value == IsStatic)
                    return;

                var previousModifier = Modifier;

                try
                {
                    if (value)
                        Modifier |= OperationModifier.Static;
                    else
                        Modifier &= ~OperationModifier.Static;
                    Language.ValidateOperation(this);
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
            get { return (Modifier & OperationModifier.Hider) != 0; }
            set
            {
                if (value == IsHider)
                    return;

                var previousModifier = Modifier;

                try
                {
                    if (value)
                        Modifier |= OperationModifier.Hider;
                    else
                        Modifier &= ~OperationModifier.Hider;
                    Language.ValidateOperation(this);
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
        ///     Cannot set virtual modifier.
        /// </exception>
        public virtual bool IsVirtual
        {
            get { return (Modifier & OperationModifier.Virtual) != 0; }
            set
            {
                if (value == IsVirtual)
                    return;

                var previousModifier = Modifier;

                try
                {
                    if (value)
                        Modifier |= OperationModifier.Virtual;
                    else
                        Modifier &= ~OperationModifier.Virtual;
                    Language.ValidateOperation(this);
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
        ///     Cannot set abstract modifier.
        /// </exception>
        public virtual bool IsAbstract
        {
            get { return (Modifier & OperationModifier.Abstract) != 0; }
            set
            {
                if (value == IsAbstract)
                    return;

                var previousModifier = Modifier;

                try
                {
                    if (value)
                        Modifier |= OperationModifier.Abstract;
                    else
                        Modifier &= ~OperationModifier.Abstract;
                    Language.ValidateOperation(this);
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
        ///     Cannot set override modifier.
        /// </exception>
        public virtual bool IsOverride
        {
            get { return (Modifier & OperationModifier.Override) != 0; }
            set
            {
                if (value == IsOverride)
                    return;

                var previousModifier = Modifier;

                try
                {
                    if (value)
                        Modifier |= OperationModifier.Override;
                    else
                        Modifier &= ~OperationModifier.Override;
                    Language.ValidateOperation(this);
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
        ///     Cannot set sealed modifier.
        /// </exception>
        public virtual bool IsSealed
        {
            get { return (Modifier & OperationModifier.Sealed) != 0; }
            set
            {
                if (value == IsSealed)
                    return;

                var previousModifier = Modifier;

                try
                {
                    if (value)
                        Modifier |= OperationModifier.Sealed;
                    else
                        Modifier &= ~OperationModifier.Sealed;
                    Language.ValidateOperation(this);
                    Changed();
                }
                catch
                {
                    Modifier = previousModifier;
                    throw;
                }
            }
        }

        public virtual bool HasBody { get { return !IsAbstract && !(Parent is InterfaceType); } }

        public virtual bool Overridable
        {
            get
            {
                if (Language.ExplicitVirtualMethods)
                {
                    return IsVirtual || IsAbstract || (IsOverride && !IsSealed);
                }
                return Access != AccessModifier.Private &&
                       (IsModifierless || IsAbstract || IsHider);
            }
        }

        public virtual void ClearModifiers()
        {
            if (Modifier != OperationModifier.None)
            {
                Modifier = OperationModifier.None;
                Changed();
            }
        }

        protected override void CopyFrom(Member member)
        {
            base.CopyFrom(member);

            var operation = (Operation) member;
            Modifier = operation.Modifier;
            argumentList = operation.argumentList.Clone();
        }

        public abstract Operation Clone(CompositeType newParent);

        public virtual bool HasSameSignatureAs(Operation operation)
        {
            if (operation == null || Name != operation.Name)
                return false;

            // Names and types are the same and the parameter counts do not equal
            if (ArgumentList.Count != operation.ArgumentList.Count)
                return false;

            for (var i = 0; i < ArgumentList.Count; i++)
            {
                if (ArgumentList[i].Type != operation.ArgumentList[i].Type ||
                    ArgumentList[i].Modifier != operation.ArgumentList[i].Modifier)
                {
                    return false;
                }
            }

            return true;
        }
    }
}