using System.Text.RegularExpressions;

namespace POI_DNA_Analyzer
{
	internal class ChunkAnalyzer
	{
		public ChunkAnalyzer()
		{
			InitializeDictionaries();
		}

		public Dictionary<string, int> DinucleotidesCount { get; private set; } = new Dictionary<string, int>();

		public Dictionary<char, int> NucleotidesCount { get; private set; } = new Dictionary<char, int>();

		public void AnalyzeChunk(string chunk)
		{
			chunk = ClearChunk(chunk);
			ClearDictionaries();

			for (int i = 0; i < chunk.Length - 1; i++)
			{
				string pair = chunk[i].ToString() + chunk[i + 1].ToString();

				CountDinucleotide(pair);
				CountNucleotide(pair[0]);
			}
		}

		private void CountDinucleotide(string dinucleotide)
		{
			if (DinucleotidesCount.ContainsKey(dinucleotide))
				DinucleotidesCount[dinucleotide]++;
		}

		private void CountNucleotide(char letter)
		{
			if (NucleotidesCount.ContainsKey(letter))
				NucleotidesCount[letter]++;
		}

		private void ClearDictionaries()
		{
			foreach (string key in DinucleotidesCount.Keys.ToList())
				DinucleotidesCount[key] = 0;

			foreach (char key in new NucleotidesList().Get)
				NucleotidesCount[key] = 0;
		}

		private void InitializeDictionaries()
		{
			foreach (string key in new DinucleotidesList().Get)
				DinucleotidesCount.Add(key, 0);

			foreach (char key in new NucleotidesList().Get)
				NucleotidesCount.Add(key, 0);
		}

		private string ClearChunk(string chunk)
		{
			return Regex.Replace(chunk, @"\t|\n|\r", "");
		}
	}
}
