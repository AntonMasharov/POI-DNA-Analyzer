namespace POI_DNA_Analyzer
{
	internal class FeedbackFileReader
	{
		private FeedbackFile _file;
		private FeedbackEntities _entities;
		private Dictionary<string, int> _header;

		public FeedbackFileReader(FeedbackEntities entities, FeedbackFile file)
		{
			_file = file;
			_entities = entities;
			_header = new Dictionary<string, int>();
		}

		public void Read()
		{
			SetDefaultState();

			string content = _file.GetString();
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
			_entities.InitializeDictionaries(contentLines, _header);
		}

		private void SetDefaultState()
		{
			_header = new Dictionary<string, int>();
		}
	}
}
