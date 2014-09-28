using System;
using System.CodeDom;
using System.IO;
using NUnit.Framework;
using Rhino.Mocks;

namespace NStub.Core.Tests
{
	/// <summary>
	/// This class exercises all major methods of NStubCore.  Mock objects are
	/// used where possible to enhance granularity.
	/// </summary>
	[TestFixture]
	public class NStubCoreTest
	{
		#region Fields (Private)

		private string _outputDirectory;
		private string _sampleNamespace = "ThisIsMySampleNamespace";
		private ICodeGenerator _mockCodeGenerator;
		private NStubCore _nStubCore;

		#endregion Fields (Private)

		#region TestFixtureSetUp (Public)

		/// <summary>
		/// Performs the initial setup for the entire test suite.  We create
		/// an temporary output directory for code generation.  This directory
		/// will be deleted during cleanup.
		/// </summary>
		[TestFixtureSetUp]
		public void TestFixtureSetUp()
		{
			_outputDirectory = Path.GetTempPath();
		} 

		#endregion TestFixtureSetUp (Public)

		/// <summary>
		/// Performs clean up after the entire test suite has completed.
		/// The scratch ouput directory created for testing will be 
		/// completely removed.
		/// </summary>
		[TestFixtureTearDown]
		public void TestFixtureTearDown()
		{
			if (Directory.Exists(_outputDirectory))
			{
				Directory.Delete(_outputDirectory, true);
			}
		}

		#region SetUp (Public)

		/// <summary>
		/// Performs the setup for each individual test.  This method
		/// refreshes the instances of both MockCodeGenerator and
		/// NStubCore.
		/// </summary>
		[SetUp]
		public void SetUp()
		{
			MockRepository mockRepository = new MockRepository();
			_mockCodeGenerator = mockRepository.CreateMock<ICodeGenerator>();

			CodeNamespace codeNamespace = new CodeNamespace(_sampleNamespace);

			_nStubCore =
				new NStubCore(codeNamespace, _outputDirectory, _mockCodeGenerator);
		} 

		#endregion SetUp (Public)

		#region Tests (Public)

		/// <summary>
		/// This test ensures that a properly initialized instance of NStubCore can successfully
		/// generate code without throwing an exception.
		/// </summary>
		[Test]
		public void GenerateCodeTest()
		{
			try
			{
				_nStubCore.GenerateCode();
			}
			catch (Exception exception)
			{
				Assert.Fail("This operation should be able to complete with no errors: {0}",
					exception.Message);
			}
		}

		/// <summary>
		/// This test ensures that the code namespace set on NStubCore's constructor
		/// is set correctly.
		/// </summary>
		[Test]
		public void CodeNamespaceTest()
		{
			Assert.AreEqual(_sampleNamespace, _nStubCore.CodeNamespace.Name);
		}

		/// <summary>
		/// This test ensures that the code generator set on NStubCore's constructor
		/// is set correctly.
		/// </summary>
		[Test]
		public void CodeGeneratorTest()
		{
			Assert.AreEqual(_mockCodeGenerator, _nStubCore.CodeGenerator);
		}

		/// <summary>
		/// This test ensures that the output directory set on NStubCore's constructor
		/// is set correctly.
		/// </summary>
		[Test]
		public void OutputDirectoryTest()
		{
			Assert.AreEqual(_outputDirectory, _nStubCore.OutputDirectory);
		} 

		#endregion Tests (Public)
	}
}
