﻿namespace POI_DNA_Analyzer
{
	internal class DinucleotidesAnalyzer
    {
		private ChunkAnalyzer _chunkAnalyzer;
		private ChunkMatrixBuilder _chunkMatrixBuilder;
		private IMatrixComparator _matrixComparator;

		private Dictionary<string, float> _lastMatrix = new Dictionary<string, float>();
		private int _lastIndex;

		public DinucleotidesAnalyzer(IMatrixComparator matrixComparator) 
		{
			_chunkAnalyzer = new ChunkAnalyzer();
			_chunkMatrixBuilder = new ChunkMatrixBuilder(_chunkAnalyzer);
			_matrixComparator = matrixComparator;

			InitializeDictionary();
		}

		public Dictionary<string, List<double>> DinucleotidesProbabilities { get; private set; } = new Dictionary<string, List<double>>();

		public List<int> Indexes { get; private set; } = new List<int>();

		public void Analyze(string text, int chunkSize, double similarityCoefficient, bool canSkipCheckboxState)
		{
			SetDefaultState();
			int actualIndex = 1;

			List<string> parts = DivideIntoParts(text, chunkSize);

			foreach (string part in parts)
			{
				_chunkAnalyzer.AnalyzeChunk(part);
				_chunkMatrixBuilder.Build();

				if (CanSkip(similarityCoefficient, canSkipCheckboxState) == false)
				{
					GetDataFromMatrix(_chunkMatrixBuilder.DinucleotidesProbabilities);
					Indexes.Add(_lastIndex);
				}

				_lastIndex++;
				actualIndex++;
			}
		}

		private void SetDefaultState()
		{
			ClearDictionary();
			Indexes.Clear();
			_lastMatrix.Clear();
			_lastIndex = 1;
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

		private void GetDataFromMatrix(Dictionary<string, float> matrix)
		{
			foreach (string key in DinucleotidesProbabilities.Keys.ToList())
			{
				if (matrix.ContainsKey(key) == false)
				{
					DinucleotidesProbabilities[key].Add(0);
					return;
				}

				DinucleotidesProbabilities[key].Add(matrix[key] * 100);
			}
		}
		
		private void InitializeDictionary()
		{
			foreach (string key in new DinucleotidesList().Get)
				DinucleotidesProbabilities.Add(key, new List<double>());
		}

		private void ClearDictionary()
		{
			foreach (string key in DinucleotidesProbabilities.Keys.ToList())
				DinucleotidesProbabilities[key] = new List<double>();
		}

		private bool CanSkip(double similarityCoefficient, bool canSkipCheckboxState)
		{
			if (canSkipCheckboxState == false)
				return false;

			if (_lastMatrix.Count == 0 || _lastMatrix == null)
			{
				_lastMatrix = new Dictionary<string, float>(_chunkMatrixBuilder.DinucleotidesProbabilities);

				return false;
			}

			bool result = _matrixComparator.IsSimilar(_chunkMatrixBuilder.DinucleotidesProbabilities, _lastMatrix, similarityCoefficient);

			_lastMatrix = new Dictionary<string, float>(_chunkMatrixBuilder.DinucleotidesProbabilities);

			return result;
		}
    }
}
