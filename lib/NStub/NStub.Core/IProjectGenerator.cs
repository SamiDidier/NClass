using System.Collections.Generic;
using System.Reflection;

namespace NStub.Core
{
	/// <summary>
	/// The IProjectGenerator interface abstracts all information and functionality
	/// necessary to successfully generate a proper project file.
	/// </summary>
	public interface IProjectGenerator
	{
		/// <summary>
		/// Generates the project file.  This method is responsible for actually
		/// generating the XML which will represent the project as well including
		/// all necessary resources and references.
		/// </summary>
		void GenerateProjectFile();
		
		/// <summary>
		/// Gets or sets the name of the project.
		/// </summary>
		/// <value>The name of the project.</value>
		string ProjectName
		{
			get;
			set;
		}
		
		/// <summary>
		/// Gets or sets the output directory where the project will be
		/// output to.
		/// </summary>
		/// <value>The output directory where the project will be output to.</value>
		string OutputDirectory
		{
			get;
			set;
		}
		
		/// <summary>
		/// Gets or sets the class files which will be included in the project.
		/// </summary>
		/// <value>The class files which will be included in the project.</value>
		IList<string> ClassFiles
		{
			get;
			set;
		}
		
		/// <summary>
		/// Gets or sets the assemblies which will be referenced in the project.
		/// Any duplicate references found in this list will be removed at generation time.
		/// </summary>
		/// <value>The assemblies which will be referenced by the project.</value>
		IList<AssemblyName> ReferencedAssemblies
		{
			get;
			set;
		}
	}
}
