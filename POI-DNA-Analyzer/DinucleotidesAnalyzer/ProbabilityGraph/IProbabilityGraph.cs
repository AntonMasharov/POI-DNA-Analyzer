using System.Drawing;

namespace POI_DNA_Analyzer
{
	internal interface IProbabilityGraph
	{
		void ProvideData(IEnumerable<int> indexes, IEnumerable<double> probabilities, Color color, string name);

		void Show();

		void Clear();
	}
}
