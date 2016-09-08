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
using System.Drawing;
using System.Windows.Forms;
using NClass.Core;
using NClass.DiagramEditor.Properties;

namespace NClass.DiagramEditor.ClassDiagram
{
    public static class Icons
    {
        private const int DefaultDestructorIndex = 84;
        private const int PrivateDestructorIndex = 85;
        public const int InterfaceImageIndex = 86;
        public const int EnumItemImageIndex = 87;
        public const int ParameterImageIndex = 88;
        public const int ClassImageIndex = 89;

        private static Bitmap[] images;

        static Icons()
        {
            LoadImages();
        }

        public static ImageList IconList { get; private set; }

        private static void LoadImages()
        {
            images = new[]
            {
                Resources.DefaultConst,
                Resources.PublicConst,
                Resources.ProtintConst,
                Resources.InternalConst,
                Resources.ProtectedConst,
                Resources.PrivateConst,
                Resources.DefaultField,
                Resources.PublicField,
                Resources.ProtintField,
                Resources.InternalField,
                Resources.ProtectedField,
                Resources.PrivateField,
                Resources.DefaultConstructor,
                Resources.PublicConstructor,
                Resources.ProtintConstructor,
                Resources.InternalConstructor,
                Resources.ProtectedConstructor,
                Resources.PrivateConstructor,
                Resources.DefaultOperator,
                Resources.PublicOperator,
                Resources.ProtintOperator,
                Resources.InternalOperator,
                Resources.ProtectedOperator,
                Resources.PrivateOperator,
                Resources.DefaultMethod,
                Resources.PublicMethod,
                Resources.ProtintMethod,
                Resources.InternalMethod,
                Resources.ProtectedMethod,
                Resources.PrivateMethod,
                Resources.DefaultReadonly,
                Resources.PublicReadonly,
                Resources.ProtintReadonly,
                Resources.InternalReadonly,
                Resources.ProtectedReadonly,
                Resources.PrivateReadonly,
                Resources.DefaultWriteonly,
                Resources.PublicWriteonly,
                Resources.ProtintWriteonly,
                Resources.InternalWriteoly,
                Resources.ProtectedWriteonly,
                Resources.PrivateWriteonly,
                Resources.DefaultProperty,
                Resources.PublicProperty,
                Resources.ProtintProperty,
                Resources.InternalProperty,
                Resources.ProtectedProperty,
                Resources.PrivateProperty,
                Resources.DefaultEvent,
                Resources.PublicEvent,
                Resources.ProtintEvent,
                Resources.InternalEvent,
                Resources.ProtectedEvent,
                Resources.PrivateEvent,
                Resources.DefaultClass,
                Resources.PublicClass,
                Resources.ProtintClass,
                Resources.InternalClass,
                Resources.ProtectedClass,
                Resources.PrivateClass,
                Resources.DefaultStructure,
                Resources.PublicStructure,
                Resources.ProtintStructure,
                Resources.InternalStructure,
                Resources.ProtectedStructure,
                Resources.PrivateStructure,
                Resources.DefaultInterface,
                Resources.PublicInterface,
                Resources.ProtintInterface,
                Resources.InternalInterface,
                Resources.ProtectedInterface,
                Resources.PrivateInterface,
                Resources.DefaultEnum,
                Resources.PublicEnum,
                Resources.ProtintEnum,
                Resources.InternalEnum,
                Resources.ProtectedEnum,
                Resources.PrivateEnum,
                Resources.DefaultDelegate,
                Resources.PublicDelegate,
                Resources.ProtintDelegate,
                Resources.InternalDelegate,
                Resources.ProtectedDelegate,
                Resources.PrivateDelegate,
                Resources.DefaultDestructor, // 84.
                Resources.PrivateDestructor, // 85.
                Resources.Interface24, // 86.
                Resources.EnumItem, // 87.
                Resources.Parameter, // 88.
                Resources.Class // 89.
            };

            IconList = new ImageList();
            IconList.ColorDepth = ColorDepth.Depth32Bit;
            IconList.Images.AddRange(images);
        }

        /// <exception cref="ArgumentNullException">
        ///     A <paramref name="member" /> nem lehet null.
        /// </exception>
        public static int GetImageIndex(Member member)
        {
            if (member == null)
                throw new ArgumentNullException("member");

            var group = 0;

            if (member is Field)
            {
                if (((Field) member).IsConstant)
                {
                    group = 0;
                }
                else
                {
                    group = 1;
                }
            }
            else if (member is Method)
            {
                if (member is Destructor)
                {
                    return PrivateDestructorIndex;
                }
                if (member is Constructor)
                {
                    @group = 2;
                }
                else if (((Method) member).IsOperator)
                {
                    @group = 3;
                }
                else
                {
                    @group = 4;
                }
            }
            else if (member is Property)
            {
                var property = (Property) member;

                if (property.IsReadonly)
                {
                    group = 5;
                }
                else if (property.IsWriteonly)
                {
                    group = 6;
                }
                else
                {
                    group = 7;
                }
            }
            else if (member is Event)
            {
                group = 8;
            }

            return group*6 + (int) member.Access;
        }

        /// <exception cref="ArgumentNullException">
        ///     <paramref name="member" /> is null.
        /// </exception>
        public static Image GetImage(Member member)
        {
            var imageIndex = GetImageIndex(member);
            return images[imageIndex];
        }

        public static Image GetImage(MemberType type, AccessModifier access)
        {
            if (type == MemberType.Destructor)
            {
                if (access == AccessModifier.Default)
                    return Resources.DefaultDestructor;
                return Resources.PrivateDestructor;
            }

            var group = 0;
            switch (type)
            {
                case MemberType.Field:
                    group = 1;
                    break;

                case MemberType.Method:
                    group = 4;
                    break;

                case MemberType.Constructor:
                    group = 2;
                    break;

                case MemberType.Property:
                    group = 7;
                    break;

                case MemberType.Event:
                    group = 8;
                    break;
            }

            return images[group*6 + (int) access];
        }

        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type" /> is null.
        /// </exception>
        public static Image GetImage(TypeBase type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return GetImage(type.EntityType, type.AccessModifier);
        }

        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type" /> is null.
        /// </exception>
        public static Image GetImage(EntityType type, AccessModifier access)
        {
            var group = 0;
            switch (type)
            {
                case EntityType.Class:
                    group = 9;
                    break;

                case EntityType.Structure:
                    group = 10;
                    break;

                case EntityType.Interface:
                    group = 11;
                    break;

                case EntityType.Enum:
                    group = 12;
                    break;

                case EntityType.Delegate:
                    group = 13;
                    break;
            }

            return images[group*6 + (int) access];
        }
    }
}