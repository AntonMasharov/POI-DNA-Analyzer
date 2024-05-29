using System.Drawing;

namespace POI_DNA_Analyzer
{
	internal interface IProbabilityGraph
	{
		void ProvideData(List<int> indexes, List<double> probabilities, Color color, string name);
	}
}
