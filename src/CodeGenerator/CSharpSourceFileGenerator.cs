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
using NClass.Core;

namespace NClass.CodeGenerator
{
    internal sealed class CSharpSourceFileGenerator : SourceFileGenerator
    {
        /// <exception cref="NullReferenceException">
        ///     <paramref name="type" /> is null.
        /// </exception>
        public CSharpSourceFileGenerator(TypeBase type,
                                         string rootNamespace,
                                         bool sort_using,
                                         bool generate_document_comment,
                                         string compagny_name,
                                         string copyright_header,
                                         string author)
            : base(type, rootNamespace, sort_using, generate_document_comment, compagny_name, copyright_header, author)
        {
        }

        protected override string Extension { get { return ".cs"; } }

        protected override void WriteFileContent(string fileName)
        {
            WriteCopyrights(fileName);
            WriteUsings();
            OpenNamespace();
            WriteType(Type);
            CloseNamespace();
        }

        private void WriteCopyrights(string fileName)
        {
            WriteLine(string.Format("<copyright file=\"{0}\" company=\"{1}\">", fileName, compagny_name));
            WriteLine(copyright_header);
            WriteLine("</copyright>");

            WriteLine(string.Format("<author>{0}</author>", author));
            WriteLine(string.Format("<date></date>", DateTime.Now));
        }

        private void WriteUsings()
        {
            var importList = Settings.Default.CSharpImportList;

            var str = new List<string>();
            foreach (var usingElement in importList)
                str.Add(usingElement);

            // Sort using
            if (sort_using)
                str.Sort();

            foreach (var usingElement in str)
                WriteLine("using " + usingElement + ";");

            if (importList.Count > 0)
                AddBlankLine();
        }

        private void OpenNamespace()
        {
            WriteLine("namespace " + RootNamespace);
            WriteLine("{");
            IndentLevel++;
        }

        private void CloseNamespace()
        {
            IndentLevel--;
            WriteLine("}");
        }

        private void WriteType(TypeBase type)
        {
            if (type is CompositeType)
                WriteCompositeType((CompositeType) type);
            else if (type is EnumType)
                WriteEnum((EnumType) type);
            else if (type is DelegateType)
                WriteDelegate((DelegateType) type);
        }

        private void WriteCompositeType(CompositeType type)
        {
            // Writing type declaration
            WriteLine(type.GetDeclaration());
            WriteLine("{");
            IndentLevel++;

            if (type is ClassType)
            {
                foreach (var nestedType in ((ClassType) type).NestedChilds)
                {
                    WriteType(nestedType);
                    AddBlankLine();
                }
            }

            if (type.SupportsFields)
            {
                foreach (var field in type.Fields)
                    WriteField(field);
            }

            var needBlankLine = type.FieldCount > 0 && type.OperationCount > 0;

            foreach (var operation in type.Operations)
            {
                if (needBlankLine)
                    AddBlankLine();
                needBlankLine = true;

                WriteOperation(operation);
            }

            // Writing closing bracket of the type block
            IndentLevel--;
            WriteLine("}");
        }

        private void WriteEnum(EnumType _enum)
        {
            // Writing type declaration
            WriteLine(_enum.GetDeclaration());
            WriteLine("{");
            IndentLevel++;

            var valuesRemained = _enum.ValueCount;
            foreach (var value in _enum.Values)
            {
                if (--valuesRemained > 0)
                    WriteLine(value.GetDeclaration() + ",");
                else
                    WriteLine(value.GetDeclaration());
            }

            // Writing closing bracket of the type block
            IndentLevel--;
            WriteLine("}");
        }

        private void WriteDelegate(DelegateType _delegate)
        {
            WriteLine(_delegate.GetDeclaration());
        }

        private void WriteField(Field field)
        {
            WriteLine(field.GetDeclaration());
        }

        private void WriteOperation(Operation operation)
        {
            WriteLine(operation.GetDeclaration());

            if (operation is Property)
            {
                WriteProperty((Property) operation);
            }
            else if (operation.HasBody)
            {
                if (operation is Event)
                {
                    WriteLine("{");
                    IndentLevel++;
                    WriteLine("add {  }");
                    WriteLine("remove {  }");
                    IndentLevel--;
                    WriteLine("}");
                }
                else
                {
                    WriteLine("{");
                    IndentLevel++;
                    WriteNotImplementedString();
                    IndentLevel--;
                    WriteLine("}");
                }
            }
        }

        private void WriteProperty(Property property)
        {
            WriteLine("{");
            IndentLevel++;

            if (!property.IsWriteonly)
            {
                if (property.HasImplementation)
                {
                    WriteLine("get");
                    WriteLine("{");
                    IndentLevel++;
                    WriteNotImplementedString();
                    IndentLevel--;
                    WriteLine("}");
                }
                else
                {
                    WriteLine("get;");
                }
            }
            if (!property.IsReadonly)
            {
                if (property.HasImplementation)
                {
                    WriteLine("set");
                    WriteLine("{");
                    IndentLevel++;
                    WriteNotImplementedString();
                    IndentLevel--;
                    WriteLine("}");
                }
                else
                {
                    WriteLine("set;");
                }
            }

            IndentLevel--;
            WriteLine("}");
        }

        private void WriteNotImplementedString()
        {
            if (Settings.Default.UseNotImplementedExceptions)
            {
                if (Settings.Default.CSharpImportList.Contains("System"))
                    WriteLine("throw new NotImplementedException();");
                else
                    WriteLine("throw new System.NotImplementedException();");
            }
            else
            {
                AddBlankLine(true);
            }
        }
    }
}