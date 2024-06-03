namespace POI_DNA_Analyzer
{
	internal class DNACodonTableFileReader
	{
		private DNACodonTableFile _codonsFile;
		private Codon _codon;
		private Dictionary<string, int> _header;

		public DNACodonTableFileReader(DNACodonTableFile file)
		{
			_codonsFile = file;
			_codon = new Codon();
			_header = new Dictionary<string, int>();
		}

		public void ReadTable()
		{
			SetDefaultState();

			string content = _codonsFile.GetString();
			string[] lines = content.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

			ReadHeader(lines[0]);
			ReadContent(lines[1..]);
		}

		private void ReadHeader(string headerLine)
		{
			string[] parts = headerLine.Split(',');

			for (int i = 0; i < parts.Length; i++)
			{
				_header.Add(parts[i], i);
			}
		}

		private void ReadContent(string[] contentLines)
		{
			_codon.InitializeDictionaries(contentLines, _header);
		}

		private void SetDefaultState()
		{
			_header = new Dictionary<string, int>();
		}
	}
}
