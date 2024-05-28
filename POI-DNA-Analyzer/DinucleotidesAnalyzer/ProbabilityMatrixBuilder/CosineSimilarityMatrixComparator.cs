namespace POI_DNA_Analyzer
{
	internal class CosineSimilarityMatrixComparator : IMatrixComparator
	{
		private Dictionary<string, Dictionary<string, float>> _firstMatrixProbabilities;
		private Dictionary<string, Dictionary<string, float>> _secondMatrixProbabilities;

		public CosineSimilarityMatrixComparator()
		{
			_firstMatrixProbabilities = new Dictionary<string, Dictionary<string, float>>();
			_secondMatrixProbabilities = new Dictionary<string, Dictionary<string, float>>();
		}

		public bool IsSimilar(Dictionary<string, float> firstMatrix, Dictionary<string, float> secondMatrix, double similarityCoefficient)
		{
			_firstMatrixProbabilities = GetDataFromMatrix(firstMatrix);
			_secondMatrixProbabilities = GetDataFromMatrix(secondMatrix);

			double similarity = CalculateCosineSimilarity(_firstMatrixProbabilities, _secondMatrixProbabilities);

			if (similarity >= similarityCoefficient)
				return true;
			else
				return false;
		}

		private Dictionary<string, Dictionary<string, float>> GetDataFromMatrix(Dictionary<string, float> inputMatrix)
		{
			Dictionary<string, Dictionary<string, float>> result = new Dictionary<string, Dictionary<string, float>>(GetTemplateMatrix()) { };

			foreach (string key in inputMatrix.Keys.ToList())
			{
				string newKey = key[0] + "N";

				result[newKey].Add(key, MathF.Round(inputMatrix[key] * 100, 2));
			}

			return result;
		}	

		private Dictionary<string, Dictionary<string, float>> GetTemplateMatrix()
		{
			Dictionary<string, Dictionary<string, float>> templateMatrixProbabilities = new Dictionary<string, Dictionary<string, float>>();

			foreach (char letter in new NucleotidesList().Get)
			{
				string dinucleotide = letter.ToString() + "N";
				templateMatrixProbabilities.Add(dinucleotide, new Dictionary<string, float>());
			}

			return templateMatrixProbabilities;
		}

		private double CalculateCosineSimilarity(Dictionary<string, Dictionary<string, float>> firstMatrix, Dictionary<string, Dictionary<string, float>> secondMatrix)
		{
			if (firstMatrix.Count != secondMatrix.Count)
				throw new ArgumentException("Arrays must be of the same length.");

			double similarity = 0;

			foreach(string vectorName in firstMatrix.Keys.ToList())
			{
				double innerProduct = 0;
				double firstNorm = 0;
				double secondNorm = 0;

				foreach (string axis in firstMatrix[vectorName].Keys)
				{
					innerProduct += firstMatrix[vectorName][axis] * secondMatrix[vectorName][axis];
					firstNorm += Math.Pow(firstMatrix[vectorName][axis], 2);
					secondNorm += Math.Pow(secondMatrix[vectorName][axis], 2);
				}

				firstNorm = Math.Sqrt(firstNorm);
				secondNorm = Math.Sqrt(secondNorm);
				similarity += innerProduct / (firstNorm * secondNorm);
			}

			return similarity / 4 * 100;
		}
	}
}