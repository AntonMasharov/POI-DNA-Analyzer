namespace POI_DNA_Analyzer
{
	[Obsolete]
	internal class EuclideanDistanceMatrixComparator : IMatrixComparator
	{
		private List<float> _firstMatrixProbabilities = new List<float>();
		private List<float> _secondMatrixProbabilities = new List<float>();

		public bool IsSimilar(Dictionary<string, float> firstMatrix, Dictionary<string, float> secondMatrix, double similarityCoefficient)
		{
			_firstMatrixProbabilities = GetDataFromMatrix(firstMatrix);
			_secondMatrixProbabilities = GetDataFromMatrix(secondMatrix);

			double similarity = CalculateEuclideanDistance(_firstMatrixProbabilities, _secondMatrixProbabilities);

			if (similarity >= similarityCoefficient)
				return true;
			else
				return false;
		}

		private List<float> GetDataFromMatrix(Dictionary<string, float> inputMatrix)
		{
			List<float> result = new List<float>();

			foreach (string key in inputMatrix.Keys.ToList())
			{
				result.Add(inputMatrix[key]);
			}

			return result;
		}

		private double CalculateEuclideanDistance(List<float> firstMatrix, List<float> secondMatrix)
		{
			if (firstMatrix.Count != secondMatrix.Count)
				throw new ArgumentException("Arrays must be of the same length.");

			double sumOfSquares = 0;

			for (int i = 0; i < firstMatrix.Count; i++)
			{
				double diff = firstMatrix[i]*100 - secondMatrix[i]*100;
				sumOfSquares += diff * diff;
			}

			return Math.Sqrt(sumOfSquares);
		}
	}
}
