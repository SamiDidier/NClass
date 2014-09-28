using System.CodeDom;

namespace NStub.Core
{
	/// <summary>
	/// ICodeGenerator represents a class that is capable of generating the set
	/// of types represented by <see cref="System.CodeDom.CodeNamespace"/>
	/// into a set of individual class files which will be output to the OutputDirectory.
	/// </summary>
	public interface ICodeGenerator
	{
		/// <summary>
		/// Gets or sets the output directory where the class files will be
		/// output to.
		/// </summary>
		/// <value>The output directory where the class files will be output
		/// to.</value>
		string OutputDirectory
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the <see cref="System.CodeDom.CodeNamespace"/>
		/// which represents the set of types to be generated.
		/// </summary>
		/// <value>The <see cref="CodeNamespace"/> which represents the set 
		/// of types of be generated.</value>
		CodeNamespace CodeNamespace
		{
			get;
			set;
		}

		/// <summary>
		/// The implementation of this method will actually perform the generation
		/// of the class files which represent the types supplied by the 
		/// <see cref="System.CodeDom.CodeNamespace"/>.
		/// </summary>
		void GenerateCode();
	}
}
