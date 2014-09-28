using System;
using System.Text;

namespace NStub.Core
{
	/// <summary>
	/// This class provides basic utility methods to assist in the code generation
	/// perfomed by NStub.
	/// </summary>
	public static class Utility
	{
		#region Methods (Public)

		/// <summary>
		/// Gets the unqualified name of the given type.  For example, if 
		/// <c>System.IO.Stream</c> is provided, <c>Stream</c> is returned.
		/// </summary>
		/// <param name="name">The fully qualified name.</param>
		/// <returns>The unqualified name.</returns>
		/// <exception cref="System.ArgumentNullException">name is null.</exception>
		/// <exception cref="System.ArgumentException">name is an empty string.</exception>
		public static string GetUnqualifiedTypeName(string name)
		{
			#region Validation

			if (name == null)
			{
				throw new ArgumentNullException("name", Exceptions.ParameterCannotBeNull);
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Exceptions.StringCannotBeEmpty, "name");
			}

			#endregion Validation

			return name.Substring((name.LastIndexOf('.') + 1), 
				(name.Length - (name.LastIndexOf('.') + 1)));
		}

		/// <summary>
		/// Gets the name of the namespace from fully the given qualified type.
		/// For example, if <c>System.IO.Stream</c> is provided, <c>System.IO</c>
		/// is returned.
		/// </summary>
		/// <param name="name">The fully qualified name.</param>
		/// <returns>The namespace to which the fully qualified name belongs.</returns>
		/// <exception cref="System.ArgumentNullException">name is null.</exception>
		/// <exception cref="System.ArgumentException">name is an empty string.</exception>
		public static string GetNamespaceFromFullyQualifiedTypeName(string name)
		{
			#region Validation

			// Ensure our fully qualified name is indeed a valid name
			if (name == null)
			{
				throw new ArgumentNullException("name", Exceptions.ParameterCannotBeNull);
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(Exceptions.StringCannotBeEmpty, "name");
			}

			#endregion Validation

			// Each level represents an object in the qualified name
			string[] levels = 
				name.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			
			if (levels.Length == 1)
			{
				// We've encountered a class which has no namespace.
				return "";
			}

			// Now build the namespace back up again
			StringBuilder namespaceBuilder = new StringBuilder();
			for (int i = 0; i < (levels.Length - 1); i++)
			{
				namespaceBuilder.Append(levels[i]);
				namespaceBuilder.Append(".");
			}
			// Remove the final trailing '.'
			namespaceBuilder.Remove((namespaceBuilder.Length - 1), 1);

			return namespaceBuilder.ToString();			
		}

		/// <summary>
		/// Removes any illegal characters from the given path and replaces them
		/// with <c>_</c>.
		/// </summary>
		/// <param name="path">The path containing illegal characters.</param>
		/// <returns>A clean version of the path will all illegal characters replaced
		/// with underscores.</returns>
		/// <exception cref="System.ArgumentNullException">path is null.</exception>
		/// <exception cref="System.ArgumentException">path is an empty string.</exception>
		public static string ScrubPathOfIllegalCharacters(string path)
		{
			#region Validation

			if (path == null)
			{
				throw new ArgumentNullException("path", Exceptions.ParameterCannotBeNull);
			}
			if (path.Length == 0)
			{
				throw new ArgumentException(Exceptions.StringCannotBeEmpty, "path");
			}

			#endregion Validation
			
			if ( path.Contains(@"*") || path.Contains(@"?") || path.Contains(@"<") 
				|| path.Contains(@">") || path.Contains(@"|") || path.Contains("\"")
				|| path.Contains(@"+") || path.Contains("-"))			
			{
				StringBuilder stringBuilder = new StringBuilder(path);
				stringBuilder.Replace(@"*", "_");
				stringBuilder.Replace(@"?", "_");
				stringBuilder.Replace(@"<", "_");
				stringBuilder.Replace(@">", "_");
				stringBuilder.Replace(@"|", "_");
				stringBuilder.Replace("\"", "_");
				stringBuilder.Replace(@"+", "_");
				stringBuilder.Replace(@"-", "_");

				return stringBuilder.ToString();
			}
			else
			{
				return path;
			}
		}
		
		#endregion Methods (Public)
	}
}
