namespace POI_DNA_Analyzer
{
	internal class ChunkMatrixBuilder
	{
		private ChunkAnalyzer _chunkAnalyzer;

		public ChunkMatrixBuilder(ChunkAnalyzer chunkAnalyzer)
		{
			_chunkAnalyzer = chunkAnalyzer;
		}

		public Dictionary<string, float> DinucleotidesProbabilities { get; private set; } = new Dictionary<string, float>();

		public void Build()
		{
			ClearDinucleotidesProbabilities();

			foreach (string key in _chunkAnalyzer.DinucleotidesCount.Keys.ToList())
			{
				SetProbability(key);
			}
		}

		private void ClearDinucleotidesProbabilities()
		{
			DinucleotidesProbabilities.Clear();
		}

		private void SetProbability(string dinucleotide)
		{
			DinucleotidesProbabilities.Add(dinucleotide, CalculateProbability(dinucleotide));
		}

		private float CalculateProbability(string dinucleotide)
		{
			char nucleotide = dinucleotide[0];

			if (_chunkAnalyzer.NucleotidesCount[nucleotide] == 0)
				return 0;

			float result = (float)_chunkAnalyzer.DinucleotidesCount[dinucleotide] / _chunkAnalyzer.NucleotidesCount[nucleotide];

			return result;
		}
	}
}
