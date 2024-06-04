namespace POI_DNA_Analyzer
{
	internal class StartAminoAcidTableFileReader
	{
		private StartAminoAcidFile _startAminoAcidFile;
		private AminoAcid _aminoAcid;
		private Dictionary<string, int> _header;

		public StartAminoAcidTableFileReader(AminoAcid aminoAcid, StartAminoAcidFile file)
		{
			_startAminoAcidFile = file;
			_aminoAcid = aminoAcid;
			_header = new Dictionary<string, int>();
		}

		public void Read()
		{
			SetDefaultState();

			string content = _startAminoAcidFile.GetString();
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
			_aminoAcid.Initialize(contentLines, _header);
		}

		private void SetDefaultState()
		{
			_header = new Dictionary<string, int>();
		}
	}
}
