using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using NStub.Core;

namespace NStub.CSharp
{
	/// <summary>
	/// The CSharpProjectGenerator class is responsible for writing the XML which
	/// will create the project file.  This class ensures that all necessary
	/// resources as well as all necessary references are properly included.
	/// </summary>
	public class CSharpProjectGenerator : IProjectGenerator
	{
		#region Fields (Private)

		private string _projectName;
		private IList<string> _classFiles;
		private IList<AssemblyName> _referencedAssemblies;
		private string _outputDirectory;
		private XmlWriter _xmlWriter;

		#endregion Fields (Private)

		#region Constructor (Public)

		/// <summary>
		/// Initializes a new instance of the <see cref="CSharpProjectGenerator"/>
		/// within the given projectName which will output to the given 
		/// outputDirectory.
		/// </summary>
		/// <param name="projectName">The name of the project.</param>
		/// <param name="outputDirectory">The directory where the project
		/// will be output.</param>	
		/// <exception cref="System.ArgumentNullException">Either projectName or
		/// outputDirectory is null.</exception>
		/// <exception cref="System.ArgumentException">Either projectName or
		/// outputDirectory is empty.</exception>
		/// <exception cref="DirectoryNotFoundException">outputDirectory
		/// cannot be found.</exception>
		public CSharpProjectGenerator(string projectName, string outputDirectory)
		{
			#region Validation

			// Null arguments will not be accepted
			if (projectName == null)
			{
				throw new ArgumentNullException("projectName",
					Exceptions.ParameterCannotBeNull);
			}
			if (outputDirectory == null)
			{
				throw new ArgumentNullException("outputDirectory",
					Exceptions.ParameterCannotBeNull);
			}
			// Empty arguments will not be accepted
			if (projectName.Length == 0)
			{
				throw new ArgumentException(Exceptions.StringCannotBeEmpty,
					"projectName");
			}
			if (outputDirectory.Length == 0)
			{
				throw new ArgumentException(Exceptions.StringCannotBeEmpty,
					"outputDirectory");
			}
			// Ensure that the output directory is valid
			if (!(Directory.Exists(outputDirectory)))
			{
				throw new DirectoryNotFoundException(Exceptions.DirectoryCannotBeFound);
			}

			#endregion Validation

			// Set our member variables
			_outputDirectory = outputDirectory;
			_projectName = projectName;

			// Set our collection member variables
			_classFiles = new List<string>();
			_referencedAssemblies = new List<AssemblyName>();

			// We know that we'll need a reference to the NUnit framework, so
			// let's go ahead and add it
			_referencedAssemblies.Add(new AssemblyName("NUnit.Framework"));
		}

		#endregion Constructor (Public)

		#region Properties (Public)

		/// <summary>
		/// Gets or sets the name of the project.
		/// </summary>
		/// <value>The name of the project.</value>
		public string ProjectName
		{
			get
			{
				return _projectName;
			}
			set
			{
				_projectName = value;
			}
		}

		/// <summary>
		/// Gets or sets the directory where the project will be output to.
		/// </summary>
		/// <value>The directory where the project will be output to.</value>
		public string OutputDirectory
		{
			get
			{
				return _outputDirectory;
			}
			set
			{
				_outputDirectory = value;
			}
		}

		/// <summary>
		/// Gets or sets the class files which will be included in the project.
		/// </summary>
		/// <value>The class files which will be included in the project.</value>
		public IList<string> ClassFiles
		{
			get
			{
				return _classFiles;
			}
			set
			{
				_classFiles = value;
			}
		}

		/// <summary>
		/// Gets or sets the assemblies which will be referenced in the project.
		/// Any duplicate references found in this list will be removed at generation time.
		/// </summary>
		/// <value>The assemblies which will be referenced by the project.</value>
		public IList<AssemblyName> ReferencedAssemblies
		{
			get
			{
				return _referencedAssemblies;
			}
			set
			{
				_referencedAssemblies = value;
			}
		}

		#endregion Properties (Public)

		#region Methods (Public)

		/// <summary>
		/// Generates the project file.  This method is responsible for actually
		/// generating the XML which will represent the project as well including
		/// all necessary resources and references.
		/// </summary>
		public void GenerateProjectFile()
		{
			// Create our XmlWriter according to our specified settings
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.OmitXmlDeclaration = true;

			_xmlWriter = XmlWriter.Create(_outputDirectory +
				Path.DirectorySeparatorChar + _projectName + ".csproj", xmlWriterSettings);

			// Specify the scheams we will be using 
			_xmlWriter.WriteStartElement("Project", @"http://schemas.microsoft.com/developer/msbuild/2003");
			_xmlWriter.WriteAttributeString("DefaultTargets", "Build");
			_xmlWriter.WriteAttributeString("xmlns", @"http://schemas.microsoft.com/developer/msbuild/2003");
			
			// Write our configuration elements
			WritePropertyGroupElement();
			WritePropertyGroupElement("Debug", "AnyCPU");
			WritePropertyGroupElement("Release", "AnyCPU");

			// Write the items to our project
			AddReferencedAssemblies();
			AddClassFiles();
			AddDefaultTarget();

			_xmlWriter.Close();
		}
		
		#endregion Methods (Public)
		
		#region Helper Methods (Private)

		/// <summary>
		/// Writes the default target to the project file.
		/// </summary>
		private void AddDefaultTarget()
		{
			// Add a default target
			_xmlWriter.WriteStartElement("Import");
			_xmlWriter.WriteAttributeString("Project", @"$(MSBuildBinPath)\Microsoft.CSharp.targets");
			_xmlWriter.WriteEndElement();

			_xmlWriter.WriteEndElement();	// Project
		}

		/// <summary>
		/// Adds an ItemGroup to the project file which includes the class files which will be
		/// part of the project.
		/// </summary>
		private void AddClassFiles()
		{
			// Add our class files
			_xmlWriter.WriteStartElement("ItemGroup");
			foreach (string classFile in ClassFiles)
			{
				string scrubbedClassFile = 
					Utility.ScrubPathOfIllegalCharacters(classFile);
				_xmlWriter.WriteStartElement("Compile");
				_xmlWriter.WriteAttributeString("Include", scrubbedClassFile);
				_xmlWriter.WriteEndElement(); // Compile
			}
			_xmlWriter.WriteEndElement();	// ItemGroup
		}

		/// <summary>
		/// Adds an ItemGroup to the project file which includes the list of assemblies
		/// which will be referenced as part of the project.
		/// </summary>
		private void AddReferencedAssemblies()
		{
			// This list will keep track of assemblies we've already referenced so as not
			// to add a duplicate
			List<AssemblyName> assembliesAlreadyReferenced =
				new List<AssemblyName>(_referencedAssemblies.Count);

			// Add our referenced assemblies
			_xmlWriter.WriteStartElement("ItemGroup");
			foreach (AssemblyName referencedAssembly in _referencedAssemblies)
			{
				// Only add this assembly to the References group if we haven't added
				// it already
				if (!(assembliesAlreadyReferenced.Contains(referencedAssembly)))
				{
					_xmlWriter.WriteStartElement("Reference");
					_xmlWriter.WriteAttributeString("Include",
						referencedAssembly.FullName);
					_xmlWriter.WriteEndElement();	// Reference

					assembliesAlreadyReferenced.Add(referencedAssembly);
				}
			}
			_xmlWriter.WriteEndElement();	// ItemGroup
		}

		/// <summary>
		/// Writes an empty property group element to the project file.
		/// </summary>
		private void WritePropertyGroupElement()
		{
			WritePropertyGroupElement("", "");
		}

		/// <summary>
		/// Writes a property group element with the given configuration and
		/// platform to the project file.
		/// </summary>
		/// <param name="configuration">The configuration.</param>
		/// <param name="platform">The platform.</param>
		private void WritePropertyGroupElement(string configuration, string platform)
		{
			_xmlWriter.WriteWhitespace(Environment.NewLine);

			if ((configuration.Length == 0) && (platform.Length == 0))
			{
				_xmlWriter.WriteWhitespace(Environment.NewLine);
				WriteDefaultConfiguration();
			}
			else if (string.Equals(configuration, "Debug", StringComparison.InvariantCultureIgnoreCase) &&
				string.Equals(platform, "AnyCPU", StringComparison.InvariantCultureIgnoreCase))
			{
				WriteDebugConfiguration();
			}
			else if (string.Equals(configuration, "Release", StringComparison.InvariantCultureIgnoreCase) &&
				string.Equals(platform, "AnyCPU", StringComparison.InvariantCultureIgnoreCase))
			{
				WriteReleaseConfiguration();
			}

			_xmlWriter.WriteWhitespace(Environment.NewLine);
		}

		/// <summary>
		/// Writes a Configuration element to the project file specifying the default
		/// configuration.
		/// </summary>
		private void WriteDefaultConfiguration()
		{
			// Default configuration (debug)					
			_xmlWriter.WriteStartElement("PropertyGroup");

			_xmlWriter.WriteStartElement("Configuration");
			_xmlWriter.WriteAttributeString("Condition",
				" '$(Configuration)' == '' ");
			_xmlWriter.WriteValue("Debug");
			_xmlWriter.WriteEndElement();	// Configuration	

			_xmlWriter.WriteStartElement("ProductVersion");
			_xmlWriter.WriteEndElement();	// ProductVersion					

			_xmlWriter.WriteStartElement("SchemaVersion");
			_xmlWriter.WriteEndElement();	// SchemaVersion

			_xmlWriter.WriteStartElement("ProjectGuid");
			_xmlWriter.WriteEndElement();	// ProjectGuid

			_xmlWriter.WriteStartElement("OutputType");
			_xmlWriter.WriteValue("Library");
			_xmlWriter.WriteEndElement();	// OutputType	

			_xmlWriter.WriteStartElement("AppDesignerFolder");
			_xmlWriter.WriteEndElement();	// AppDesignerFolder

			_xmlWriter.WriteStartElement("RootNamespace");
			_xmlWriter.WriteEndElement();	// RootNamespace

			_xmlWriter.WriteStartElement("AssemblyName");
			_xmlWriter.WriteValue(_projectName);
			_xmlWriter.WriteEndElement();	// AssemblyName

			_xmlWriter.WriteEndElement();	// PropertyGroup
		}

		/// <summary>
		/// Writes a Configuration element to the project file specifying the Debug
		/// configuration.
		/// </summary>
		private void WriteDebugConfiguration()
		{
			// Debug configuration
			_xmlWriter.WriteStartElement("PropertyGroup");
			_xmlWriter.WriteAttributeString("Condition", " '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ");
			_xmlWriter.WriteElementString("DebugSymbols", "true");
			_xmlWriter.WriteElementString("DebugType", "full");
			_xmlWriter.WriteElementString("Optimize", "false");
			_xmlWriter.WriteElementString("OutputPath", @"bin\Debug\");
			_xmlWriter.WriteElementString("DefineConstants", "DEBUG;TRACE");
			_xmlWriter.WriteElementString("ErrorReport", "prompt");
			_xmlWriter.WriteElementString("WarningLevel", "4");
			_xmlWriter.WriteEndElement();	// PropertyGroup
		}

		/// <summary>
		/// Writes a Configuration element to the project file specifying the Release
		/// configuration.
		/// </summary>
		private void WriteReleaseConfiguration()
		{
			// Release configuration
			_xmlWriter.WriteStartElement("PropertyGroup");
			_xmlWriter.WriteAttributeString("Condition", " '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ");
			_xmlWriter.WriteElementString("DebugType", "pdbOnly");
			_xmlWriter.WriteElementString("Optimize", "true");
			_xmlWriter.WriteElementString("OutputPath", @"bin\Release\");
			_xmlWriter.WriteElementString("DefineConstants", "TRACE");
			_xmlWriter.WriteElementString("ErrorReport", "prompt");
			_xmlWriter.WriteElementString("WarningLevel", "4");
			_xmlWriter.WriteEndElement();	// PropertyGroup
		}

		#endregion Helper Methods (Private)
	}
}
