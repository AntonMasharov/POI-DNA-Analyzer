namespace POI_DNA_Analyzer
{
	internal class FeedbackEntities
	{
		private Dictionary<string, string> _feedbackRU;
		private Dictionary<string, string> _feedbackEN;

		public FeedbackEntities()
		{
			_feedbackRU = new Dictionary<string, string>();
			_feedbackEN = new Dictionary<string, string>();
		}

		public string GetCorrespondingFeedbackEntity(string key, Languages language)
		{
			if (language == Languages.Russian)
			{
				return _feedbackRU[key];
			}
			else
			{
				return _feedbackEN[key];
			}
		}

		public void InitializeDictionaries(string[] contentLines, Dictionary<string, int> header)
		{
			_feedbackRU.Clear();
			_feedbackEN.Clear();

			foreach (string line in contentLines)
			{
				string[] parts = line.Split(',');

				if (parts.Length >= 3)
				{
					string key = parts[header["NAME"]];
					string valueRU = parts[header["RU"]];
					string valueEN = parts[header["EN"]];

					_feedbackRU.Add(key, valueRU);
					_feedbackEN.Add(key, valueEN);
				}
			}
		}
	}
}
