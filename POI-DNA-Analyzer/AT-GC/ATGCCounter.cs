namespace POI_DNA_Analyzer
{
	internal class ATGCCounter : IATGCCounterResult
	{
		private ChunkAnalyzer _chunkAnalyzer;
		private Dictionary<string, List<double>> _percents;
		private List<int> _indexes;

		public ATGCCounter()
		{
			_chunkAnalyzer = new ChunkAnalyzer();
			_percents = new Dictionary<string, List<double>>();
			_indexes = new List<int>();

			InitializeDictionary();
		}

		public double ATPercent { get; private set; }

		public double GCPercent { get; private set; }

		public IReadOnlyDictionary<string, List<double>> Percents => _percents;

		public IEnumerable<int> Indexes => _indexes;

		public void CountByChunk(string text, int chunkSize)
		{
			SetDefaultState();

			int actualIndex = 1;
			List<string> parts = DivideIntoParts(text, chunkSize);

			foreach (string part in parts)
			{
				_chunkAnalyzer.AnalyzeChunk(part);
				AddPercentValuesToDictionary(CountATPercent(), CountGCPercent());
				_indexes.Add(actualIndex);
				actualIndex++;
			}
		}

		public void CountOverall(string text)
		{
			_chunkAnalyzer.AnalyzeChunk(text);
			ATPercent = CountATPercent();
			GCPercent = CountGCPercent();
		}

		private void AddPercentValuesToDictionary(double atPercent, double gcPercent)
		{
			_percents["AT"].Add(atPercent);
			_percents["GC"].Add(gcPercent);
		}

		private double CountATPercent()
		{
			double atCount = GetNucleotideCount('A') + GetNucleotideCount('T');
			double gcCount = GetNucleotideCount('G') + GetNucleotideCount('C');
			double wholeCount = atCount + gcCount;

			if (wholeCount == 0)
				return 0;

			double atPercent = new PercentCalculator().GetPercent(atCount, wholeCount) * 100;
			return atPercent;
		}

		private double CountGCPercent()
		{
			double atCount = GetNucleotideCount('A') + GetNucleotideCount('T');
			double gcCount = GetNucleotideCount('G') + GetNucleotideCount('C');
			double wholeCount = atCount + gcCount;

			if (wholeCount == 0)
				return 0;

			double gcPercent = new PercentCalculator().GetPercent(gcCount, wholeCount) * 100;
			return gcPercent;
		}

		private int GetNucleotideCount(char nucleotide)
		{
			if (_chunkAnalyzer.NucleotidesCount.ContainsKey(nucleotide) == false)
				return 0;

			return _chunkAnalyzer.NucleotidesCount[nucleotide];
		}

		private List<string> DivideIntoParts(string text, int chunkSize)
		{
			List<string> result = new List<string>();
			int leftBorder = 0;

			while (leftBorder + chunkSize < text.Length)
			{
				result.Add(text.Substring(leftBorder, chunkSize));
				leftBorder += chunkSize;
			}

			if (leftBorder < text.Length)
			{
				int partSize = text.Length - leftBorder;
				result.Add(text.Substring(leftBorder, partSize));
			}

			return result;
		}

		private void SetDefaultState()
		{
			ClearDictionary();
			_indexes.Clear();
		}

		private void InitializeDictionary()
		{
			_percents.Add("AT", new List<double>());
			_percents.Add("GC", new List<double>());
		}

		private void ClearDictionary()
		{
			foreach (string key in _percents.Keys.ToList())
				_percents[key] = new List<double>();
		}
	}
}
