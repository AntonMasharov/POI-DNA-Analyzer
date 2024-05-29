namespace POI_DNA_Analyzer
{
	internal interface IMatrixComparator
	{
		bool IsSimilar(Dictionary<string, float> firstMatrix, Dictionary<string, float> secondMatrix, double similarityCoefficient);
	}
}
