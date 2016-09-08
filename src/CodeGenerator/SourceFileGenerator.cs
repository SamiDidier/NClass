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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NClass.Core;

namespace NClass.CodeGenerator
{
    public abstract class SourceFileGenerator
    {
        private const int DefaultBuilderCapacity = 10240; // 10 KB
        // This builder object is static to increase performance
        private static StringBuilder codeBuilder;
        protected string author;
        protected string compagny_name;
        protected string copyright_header;
        protected bool generate_document_comment;
        private int indentLevel;
        protected bool sort_using;

        /// <exception cref="ArgumentNullException">
        ///     <paramref name="type" /> is null.
        /// </exception>
        protected SourceFileGenerator(TypeBase type,
                                      string rootNamespace,
                                      bool sort_using,
                                      bool generate_document_comment,
                                      string compagny_name,
                                      string copyright_header,
                                      string author)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            Type = type;
            RootNamespace = rootNamespace;
            this.sort_using = sort_using;
            this.generate_document_comment = generate_document_comment;
            this.compagny_name = compagny_name;
            this.copyright_header = copyright_header;
            this.author = author;
        }

        protected TypeBase Type { get; }

        protected string RootNamespace { get; }

        protected int IndentLevel
        {
            get { return indentLevel; }
            set
            {
                if (value >= 0)
                    indentLevel = value;
            }
        }

        protected abstract string Extension { get; }

        /// <exception cref="FileGenerationException">
        ///     An error has occured while generating the source file.
        /// </exception>
        public string Generate(string directory)
        {
            try
            {
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var fileName = Type.Name + Extension;
                fileName = Regex.Replace(fileName, @"\<(?<type>.+)\>", @"[${type}]");
                var path = Path.Combine(directory, fileName);

                using (var writer = new StreamWriter(path, false))
                {
                    WriteFileContent(fileName, writer);
                }
                return fileName;
            }
            catch (Exception ex)
            {
                throw new FileGenerationException(directory, ex);
            }
        }

        /// <exception cref="IOException">
        ///     An I/O error occurs.
        /// </exception>
        /// <exception cref="ObjectDisposedException">
        ///     The <see cref="TextWriter" /> is closed.
        /// </exception>
        private void WriteFileContent(string fileName, TextWriter writer)
        {
            if (codeBuilder == null)
                codeBuilder = new StringBuilder(DefaultBuilderCapacity);
            else
                codeBuilder.Length = 0;

            WriteFileContent(fileName);
            writer.Write(codeBuilder.ToString());
        }

        protected abstract void WriteFileContent(string fileName);

        internal static void FinishWork()
        {
            codeBuilder = null;
        }

        protected void AddBlankLine()
        {
            AddBlankLine(false);
        }

        protected void AddBlankLine(bool indentation)
        {
            if (indentation)
                AddIndent();
            codeBuilder.AppendLine();
        }

        protected void WriteLine(string text)
        {
            WriteLine(text, true);
        }

        protected void WriteLine(string text, bool indentation)
        {
            if (indentation)
                AddIndent();
            codeBuilder.AppendLine(text);
        }

        private void AddIndent()
        {
            string indentString;
            if (Settings.Default.UseTabsForIndents)
                indentString = new string('\t', IndentLevel);
            else
                indentString = new string(' ', IndentLevel*Settings.Default.IndentSize);

            codeBuilder.Append(indentString);
        }
    }
}