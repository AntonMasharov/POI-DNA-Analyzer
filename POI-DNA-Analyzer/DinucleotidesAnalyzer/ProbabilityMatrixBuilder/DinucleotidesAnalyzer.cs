using System.IO;
using System.Text;

namespace POI_DNA_Analyzer
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

		public void Analyze(StreamReader fileStream, int chunkSize, double similarityCoefficient)
		{
			ClearDictionary();
			Indexes.Clear();
			_lastMatrix.Clear();
			_lastIndex = 1;

			char[] buffer = new char[chunkSize];
			int charsRead;

			while ((charsRead = fileStream.ReadBlock(buffer, 0, buffer.Length)) > 0)
			{
				string chunk = new string(buffer, 0, charsRead);

				_chunkAnalyzer.AnalyzeChunk(chunk);
				_chunkMatrixBuilder.Build();

				if (CanSkip(similarityCoefficient) == false)
				{
					GetDataFromMatrix(_chunkMatrixBuilder.DinucleotidesProbabilities);
					Indexes.Add(_lastIndex);
				}

				_lastIndex++;

				fileStream.BaseStream.Seek(chunkSize, SeekOrigin.Current);
			}
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

		private bool CanSkip(double similarityCoefficient)
		{
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
