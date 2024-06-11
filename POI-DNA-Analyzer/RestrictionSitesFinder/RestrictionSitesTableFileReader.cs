namespace POI_DNA_Analyzer
{
	internal class RestrictionSitesTableFileReader
	{
		private RestrictionSites _restrictionSite;
		private RestrictionSitesTableFile _restrictionSitesTableFile;
		private Dictionary<string, int> _header;

		public RestrictionSitesTableFileReader(RestrictionSites restrictionSite, RestrictionSitesTableFile restrictionSitesTableFile)
		{
			_restrictionSite = restrictionSite;
			_restrictionSitesTableFile = restrictionSitesTableFile;
			_header = new Dictionary<string, int>();
		}

		public void Read()
		{
			SetDefaultState();

			string content = _restrictionSitesTableFile.GetString();
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
			_restrictionSite.Initialize(contentLines, _header);
		}

		private void SetDefaultState()
		{
			_header = new Dictionary<string, int>();
		}
	}
}
