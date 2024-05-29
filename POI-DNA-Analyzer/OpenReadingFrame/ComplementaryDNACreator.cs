using System.IO;
using System.Text.RegularExpressions;

namespace POI_DNA_Analyzer
{
	internal class ComplementaryDNACreator
	{
		public string Create(StreamReader fileStream)
		{
			if (fileStream == null)
				throw new ArgumentNullException("FileStream is null");

			string text = fileStream.ReadToEnd();
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
			text = Regex.Replace(text, @"\t|\n|\r", "");

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
