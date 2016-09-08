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
using System.IO;
using NClass.Core;

namespace NClass.CodeGenerator
{
    public abstract class ProjectGenerator
    {
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="model" /> is null.
        /// </exception>
        protected ProjectGenerator(Model model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            Model = model;
        }

        public string ProjectName { get { return Model.Name; } }

        public abstract string RelativeProjectFileName { get; }

        public Language ProjectLanguage { get { return Model.Language; } }

        protected Model Model { get; }

        protected string RootNamespace
        {
            get
            {
                var projectName = Model.Project.Name;
                var modelName = Model.Name;

                if (string.Equals(projectName, modelName, StringComparison.OrdinalIgnoreCase))
                    return modelName;
                return projectName + "." + modelName;
            }
        }

        protected List<string> FileNames { get; } = new List<string>();

        /// <exception cref="ArgumentException">
        ///     <paramref name="location" /> contains invalid path characters.
        /// </exception>
        internal bool Generate(string location,
                               bool sort_using,
                               bool generate_document_comment,
                               string compagny_name,
                               string copyright_header,
                               string author)
        {
            var success = true;

            success &= GenerateSourceFiles(location,
                                           sort_using,
                                           generate_document_comment,
                                           compagny_name,
                                           copyright_header,
                                           author);
            success &= GenerateProjectFiles(location);

            return success;
        }

        private bool GenerateSourceFiles(string location,
                                         bool sort_using,
                                         bool generate_document_comment,
                                         string compagny_name,
                                         string copyright_header,
                                         string author)
        {
            var success = true;
            location = Path.Combine(location, ProjectName);

            FileNames.Clear();
            foreach (var entity in Model.Entities)
            {
                var type = entity as TypeBase;

                if (type != null && !type.IsNested)
                {
                    var sourceFile = CreateSourceFileGenerator(type,
                                                               sort_using,
                                                               generate_document_comment,
                                                               compagny_name,
                                                               copyright_header,
                                                               author);

                    try
                    {
                        var fileName = sourceFile.Generate(location);
                        FileNames.Add(fileName);
                    }
                    catch (FileGenerationException)
                    {
                        success = false;
                    }
                }
            }

            return success;
        }

        protected abstract SourceFileGenerator CreateSourceFileGenerator(TypeBase type,
                                                                         bool sort_using,
                                                                         bool generate_document_comment,
                                                                         string compagny_name,
                                                                         string copyright_header,
                                                                         string author);

        protected abstract bool GenerateProjectFiles(string location);
    }
}