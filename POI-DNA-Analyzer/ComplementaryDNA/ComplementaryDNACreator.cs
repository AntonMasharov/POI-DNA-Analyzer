using System.IO;

namespace POI_DNA_Analyzer
{
	internal class ComplementaryDNACreator
	{
		public string Create(string text)
		{
			if (text == null || text == "")
				return "";

			text = ReplaceLetters(text);
			text = InvertText(text);

			return text;
		}

		private string ReplaceLetters(string text)
		{
			text = text.Replace('A', 'X');
			text = text.Replace('T', 'A');
			text = text.Replace('X', 'T');
			text = text.Replace('C', 'Y');
			text = text.Replace('G', 'C');
			text = text.Replace('Y', 'G');

			return text;
		}

		private string InvertText(string text)
		{
			char[] charArray = text.ToCharArray();
			Array.Reverse(charArray);
			return new string(charArray);
		}
	}
}
