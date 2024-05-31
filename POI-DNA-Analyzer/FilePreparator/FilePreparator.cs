using System.IO;
using System.Text.RegularExpressions;

namespace POI_DNA_Analyzer
{
	class FilePreparator
    {
		public string GetString(StreamReader streamReader)
		{
			if (streamReader == null)
				return "";

			string text = streamReader.ReadToEnd();

			text = RemoveHeader(text);
			text = RemoveSpecialSymbols(text);

			return text;
		}

		private string RemoveHeader(string text)
		{
			string pattern = $@"^{Regex.Escape(">")}.*(\r?\n|\r)";
			return Regex.Replace(text, pattern, string.Empty, RegexOptions.Multiline);
		}

		private string RemoveSpecialSymbols(string text)
		{
			return Regex.Replace(text, @"\t|\n|\r", "");
		}
    }
}
