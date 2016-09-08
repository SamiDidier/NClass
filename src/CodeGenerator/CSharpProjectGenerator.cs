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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NClass.Core;

namespace NClass.CodeGenerator
{
    internal sealed class CSharpProjectGenerator : ProjectGenerator
    {
        private readonly SolutionType solutionType;

        /// <exception cref="ArgumentNullException">
        ///     <paramref name="model" /> is null.
        /// </exception>
        public CSharpProjectGenerator(Model model, SolutionType solutionType)
            : base(model)
        {
            this.solutionType = solutionType;
        }

        public override string RelativeProjectFileName
        {
            get
            {
                var fileName = ProjectName + ".csproj";
                var directoryName = ProjectName;

                return Path.Combine(directoryName, fileName);
            }
        }

        protected override SourceFileGenerator CreateSourceFileGenerator(TypeBase type,
                                                                         bool sort_using,
                                                                         bool generate_document_comment,
                                                                         string compagny_name,
                                                                         string copyright_header,
                                                                         string author)
        {
            return new CSharpSourceFileGenerator(type,
                                                 RootNamespace,
                                                 sort_using,
                                                 generate_document_comment,
                                                 compagny_name,
                                                 copyright_header,
                                                 author);
        }

        protected override bool GenerateProjectFiles(string location)
        {
            try
            {
                var templateDir = Path.Combine(Application.StartupPath, "Templates");
                var templateFile = Path.Combine(templateDir, "csproj.template");
                var projectFile = Path.Combine(location, RelativeProjectFileName);

                using (var reader = new StreamReader(templateFile))
                using (var writer = new StreamWriter(
                    projectFile,
                    false,
                    reader.CurrentEncoding))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        line = line.Replace("${RootNamespace}", RootNamespace);
                        line = line.Replace("${AssemblyName}", ProjectName);

                        if (line.Contains("${VS2005:"))
                        {
                            if (solutionType == SolutionType.VisualStudio2005)
                                line = Regex.Replace(line, @"\${VS2005:(?<content>.+?)}", "${content}");
                            else
                                line = Regex.Replace(line, @"\${VS2005:(?<content>.+?)}", "");

                            if (line.Length == 0)
                                continue;
                        }
                        if (line.Contains("${VS2008:"))
                        {
                            if (solutionType == SolutionType.VisualStudio2008)
                                line = Regex.Replace(line, @"\${VS2008:(?<content>.+?)}", "${content}");
                            else
                                line = Regex.Replace(line, @"\${VS2008:(?<content>.+?)}", "");

                            if (line.Length == 0)
                                continue;
                        }

                        if (line.Contains("${SourceFile}"))
                        {
                            foreach (var fileName in FileNames)
                            {
                                var newLine = line.Replace("${SourceFile}", fileName);
                                writer.WriteLine(newLine);
                            }
                        }
                        else
                        {
                            writer.WriteLine(line);
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}