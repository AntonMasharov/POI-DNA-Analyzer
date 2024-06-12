namespace POI_DNA_Analyzer
{
	internal interface IATGCCounterResult
	{
		IReadOnlyDictionary<string, List<double>> Percents { get; }

		IEnumerable<int> Indexes {  get; }
	}
}
