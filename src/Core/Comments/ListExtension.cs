using System;
using System.Collections.Generic;


namespace NClass.Core
{
	public static class ListExtension
	{
		// Pour découper une chaine de caractères ligne par ligne
		public static void TextToList(ref List<string> list, string text)
		{
			list.Clear();
			
			string[] split = text.Split('\n');
			
			list.AddRange(split);
		}
	}
}

