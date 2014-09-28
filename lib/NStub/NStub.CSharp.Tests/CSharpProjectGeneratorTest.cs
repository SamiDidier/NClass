using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace NStub.CSharp.Tests
{
	/// <summary>
	/// This class exercises all major functionality of the CSharpProjectGenerator
	/// module.
	/// </summary>
	[TestFixture]
	public class CSharpProjectGeneratorTest
	{
		#region Fields (Private)

		private CSharpProjectGenerator _cSharpProjectGenerator;
		private string _projectName = "This is a sample project name";
		private string _outputDirectory;
		private IList<string> _classFiles;
		private IList<AssemblyName> _referencedAssemblies; 

		#endregion Fields (Private)

		#region TestFixtureSetUp (Public)

		/// <summary>
		/// Performs the initial setup for the entire test fixture.  Sets our
		/// output directory to a valid, temporary path.
		/// </summary>
		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			_outputDirectory = Path.GetTempPath();
		} 

		#endregion TestFixtureSetUp (Public)

		#region TestFixtureTearDown (Public)

		/// <summary>
		/// Performs the final cleanup following execution of all of the tests.
		/// If the temporary output directory still exists, it will be removed.
		/// </summary>
		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			if (Directory.Exists(_outputDirectory))
			{
				Directory.Delete(_outputDirectory);
			}
		} 

		#endregion TestFixtureTearDown (Public)

		#region SetUp (Public)

		/// <summary>
		/// Performs the initial setup prior to execution of each test.
		/// </summary>
		[SetUp]
		public void SetUp()
		{
			_cSharpProjectGenerator =
				new CSharpProjectGenerator(_projectName, _outputDirectory);

			_classFiles = new List<string>();
			_classFiles.Add("FooClass.cs");
			_classFiles.Add("BarClass.cs");
			_cSharpProjectGenerator.ClassFiles = _classFiles;

			_referencedAssemblies = new List<AssemblyName>();
			_referencedAssemblies.Add(new AssemblyName("FooAssembly.dll"));
			_referencedAssemblies.Add(new AssemblyName("BarAssembly.dll"));
			_cSharpProjectGenerator.ReferencedAssemblies = _referencedAssemblies;
		} 

		#endregion SetUp (Public)

		#region Tests (Public)

		/// <summary>
		/// Ensures that the ClassFiles property is set correctly on our
		/// instance of the project generator.
		/// </summary>
		[Test]
		public void ClassFilesTest()
		{
			Assert.AreEqual(_classFiles, _cSharpProjectGenerator.ClassFiles);
		}

		/// <summary>
		/// Ensures that the OutputDirectory is set correctly on our
		/// instance of the project generator.
		/// </summary>
		[Test]
		public void OutputDirectoryTest()
		{
			Assert.AreEqual(_outputDirectory,
				_cSharpProjectGenerator.OutputDirectory);
		}

		/// <summary>
		/// Ensures that the ProjectName is set correctly on our instance of
		/// the project generator.
		/// </summary>
		[Test]
		public void ProjectNameTest()
		{
			Assert.AreEqual(_projectName, _cSharpProjectGenerator.ProjectName);
		}

		/// <summary>
		/// Ensures that the ReferencedAssemblies property is set correctly on
		/// our instance of the project generator.
		/// </summary>
		[Test]
		public void ReferencedAssemblies()
		{
			Assert.AreEqual(_referencedAssemblies,
				_cSharpProjectGenerator.ReferencedAssemblies);
		}

		/// <summary>
		/// Ensures that our instance of the project generator can perform
		/// generation of the project file without fail.
		/// </summary>
		[Test]
		public void GenerateProjectFileTest()
		{
			try
			{
				_cSharpProjectGenerator.GenerateProjectFile();
			}
			catch (Exception exception)
			{
				Assert.Fail("Any exception will be considered a failure: {0}",
							exception.Message);
			}
		} 

		#endregion Tests (Public)
	}
}
