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
using System.Windows.Forms;
using NClass.Core;
using NClass.Translations;

namespace NClass.CodeGenerator
{
    public abstract class SolutionGenerator
    {
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="project" /> is null.
        /// </exception>
        protected SolutionGenerator(Project project)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            Project = project;
        }

        public string SolutionName { get { return Project.Name; } }

        protected Project Project { get; }

        protected List<ProjectGenerator> ProjectGenerators { get; } = new List<ProjectGenerator>();

        /// <exception cref="ArgumentException">
        ///     <paramref name="location" /> contains invalid path characters.
        /// </exception>
        internal GenerationResult Generate(string location,
                                           bool sort_using,
                                           bool generate_document_comment,
                                           string compagny_name,
                                           string copyright_header,
                                           string author)
        {
            var result = CheckDestination(location);
            if (result != GenerationResult.Success)
                return result;

            if (
                !GenerateProjectFiles(location,
                                      sort_using,
                                      generate_document_comment,
                                      compagny_name,
                                      copyright_header,
                                      author))
                return GenerationResult.Error;
            if (!GenerateSolutionFile(location))
                return GenerationResult.Error;

            return GenerationResult.Success;
        }

        private GenerationResult CheckDestination(string location)
        {
            try
            {
                location = Path.Combine(location, SolutionName);
                if (Directory.Exists(location))
                {
                    var result = MessageBox.Show(
                        Strings.CodeGenerationOverwriteConfirmation,
                        Strings.Confirmation,
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                        return GenerationResult.Success;
                    return GenerationResult.Cancelled;
                }
                Directory.CreateDirectory(location);
                return GenerationResult.Success;
            }
            catch
            {
                return GenerationResult.Error;
            }
        }

        private bool GenerateProjectFiles(string location,
                                          bool sort_using,
                                          bool generate_document_comment,
                                          string compagny_name,
                                          string copyright_header,
                                          string author)
        {
            var success = true;
            location = Path.Combine(location, Project.Name);

            ProjectGenerators.Clear();
            foreach (var projectItem in Project.Items)
            {
                var model = projectItem as Model;

                if (model != null)
                {
                    var projectGenerator = CreateProjectGenerator(model);
                    ProjectGenerators.Add(projectGenerator);

                    try
                    {
                        projectGenerator.Generate(location,
                                                  sort_using,
                                                  generate_document_comment,
                                                  compagny_name,
                                                  copyright_header,
                                                  author);
                    }
                    catch (FileGenerationException)
                    {
                        success = false;
                    }
                }
            }

            return success;
        }

        /// <exception cref="ArgumentException">
        ///     The <paramref name="model" /> is invalid.
        /// </exception>
        protected abstract ProjectGenerator CreateProjectGenerator(Model model);

        protected abstract bool GenerateSolutionFile(string location);
    }
}