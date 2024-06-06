using System.IO;
using System.Text.RegularExpressions;

namespace POI_DNA_Analyzer
{
	class FilePreparator
    {
		private string _headerPattern = $@"^{Regex.Escape(">")}.*(\r?\n|\r)";

		public string Header { get; private set; } = "";

		public string GetString(StreamReader streamReader)
		{
			if (streamReader == null)
				return "";

			string text = streamReader.ReadToEnd();

			Header = GetHeader(text);
			text = RemoveHeader(text);
			text = RemoveSpecialSymbols(text);

			return text;
		}

		private string GetHeader(string text)
		{
			Match match = Regex.Match(text, _headerPattern, RegexOptions.Multiline);

			if (match.Success)
			{
				string pattern = @"[<>:""/\\|?*\t\n\r]";
				return Regex.Replace(match.Value, pattern, "");
			}

			return "";
		}

		private string RemoveHeader(string text)
		{
			return Regex.Replace(text, _headerPattern, string.Empty, RegexOptions.Multiline);
		}

		private string RemoveSpecialSymbols(string text)
		{
			return Regex.Replace(text, @"\t|\n|\r", "");
		}
    }
}
