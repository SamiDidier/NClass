using NUnit.Framework;

namespace NStub.Core.Tests
{
	/// <summary>
	/// This class exercises the methods NStub's utility class.
	/// </summary>
	[TestFixture]
	public class UtilityTest
	{
		#region Fields (Private)

		private string _namespace = "System.IO";
		private string _qualifiedTypeName = "System.IO.FileStream";
		private string _unqualifiedTypeName = "FileStream";

		#endregion Fields (Private)

		#region Tests (Public)

		/// <summary>
		/// Tests the Utility.GetUnqualifiedTypeNameMethod(string) by passing
		/// it a fully qualified type name and comparing it to the expected
		/// class name in returned.
		/// </summary>
		[Test]
		public void GetUnqualifiedTypeNameTest()
		{
			Assert.AreEqual(_unqualifiedTypeName,
				Utility.GetUnqualifiedTypeName(_qualifiedTypeName));
		}

		/// <summary>
		/// Tests the Utility.GetNamespaceFromFullyQualifiedTypeName(string)
		/// method by pass it a fully qualified type name comparing it to the
		/// expected namespace returned.
		/// </summary>
		[Test]
		public void GetNamespaceFromFullyQualifiedTypeNameTest()
		{
			Assert.AreEqual(_namespace,
				Utility.GetNamespaceFromFullyQualifiedTypeName(_qualifiedTypeName));
		}

		/// <summary>
		/// Tests the Utility.ScrubPathOfIllegalCharacters(string) method by
		/// passing it a series of string surrounded by a known illegal
		/// character and ensuring that the returned string has these illegal
		/// characters removed.
		/// </summary>
		/// <remarks>The illegal characters will be replaced with '_'.</remarks>
		[Test]
		public void ScrubPathOfIllegalCharactersTest()
		{
			Assert.AreEqual("_Asterisk_",
				Utility.ScrubPathOfIllegalCharacters(@"*Asterisk*"));
			Assert.AreEqual("_QuestionMark_",
				Utility.ScrubPathOfIllegalCharacters(@"?QuestionMark?"));
			Assert.AreEqual("_QuotationMark_",
				Utility.ScrubPathOfIllegalCharacters("\"QuotationMark\""));
			Assert.AreEqual("_LessThan_",
				Utility.ScrubPathOfIllegalCharacters(@"<LessThan<"));
			Assert.AreEqual("_GreaterThan_",
				Utility.ScrubPathOfIllegalCharacters(@">GreaterThan>"));
			Assert.AreEqual("_Pipe_",
				Utility.ScrubPathOfIllegalCharacters(@"|Pipe|"));
			Assert.AreEqual("_Plus_",
				Utility.ScrubPathOfIllegalCharacters(@"+Plus+"));
			Assert.AreEqual("_Minus_",
				Utility.ScrubPathOfIllegalCharacters(@"-Minus-"));
		}

		#endregion Tests (Public)
	}
}
