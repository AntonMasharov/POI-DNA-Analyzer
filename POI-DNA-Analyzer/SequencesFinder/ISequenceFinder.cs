namespace POI_DNA_Analyzer
{
	internal interface ISequencesFinder
	{
		IEnumerable<int> OccurencesIndexes { get; }

		int GetOccurrencesCount(string source, string sequenceToFind);
	}
}
