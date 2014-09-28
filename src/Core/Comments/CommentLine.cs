using System;


namespace NClass.Core
{
	#region Class CommentLine
	public class CommentLine
	{
		#region Variables
		#region Public
		public string Comment = string.Empty;
		public bool IsMultiline = false;
		#endregion Public
		#endregion Variables

		#region Constructors
		public CommentLine (string _comment, bool _IsMultiline)
		{
			Comment = _comment;
			IsMultiline = _IsMultiline;
		}

		public CommentLine (string _comment)
		{
			Comment = _comment;
			IsMultiline = false;
		}

		public CommentLine ()
		{
			Comment = string.Empty;
			IsMultiline = false;
		}
		#endregion Constructors
	}
	#endregion Class CommentLine
}

