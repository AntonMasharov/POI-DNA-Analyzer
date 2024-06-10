namespace POI_DNA_Analyzer
{
	internal class SequenceFinderOutput
	{
		public IEnumerable<int> OccurencesIndexes { get; private set; } = new List<int>();

		public string GetOccurencesCountText(Languages language)
		{
			if (language == Languages.Russian)
				return $"Количество повторений: {OccurencesIndexes.Count()}";
			else
				return $"Occurrences count: {OccurencesIndexes.Count()}";
		}

		public void Update(IEnumerable<int> occurencesIndexes)
		{
			OccurencesIndexes = occurencesIndexes;
		}
	}
}
