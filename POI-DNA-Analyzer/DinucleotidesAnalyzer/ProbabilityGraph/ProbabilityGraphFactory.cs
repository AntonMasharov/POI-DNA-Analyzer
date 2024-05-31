using OxyPlot.Wpf;
using System.Windows.Controls.Primitives;

namespace POI_DNA_Analyzer
{
	internal class ProbabilityGraphFactory
	{
		private ScrollBar _scrollBar;
		private PlotView _plotView;

		public ProbabilityGraphFactory(PlotView plotView, ScrollBar scrollBar)
		{
			_scrollBar = scrollBar;
			_plotView = plotView;
		}

		public IProbabilityGraph Get()
		{
			OxyPlotProbabilityGraph probabilityGraph = new OxyPlotProbabilityGraph(_plotView);
			OxyPlotProbabilityGraphMediator mediator = new OxyPlotProbabilityGraphMediator(_scrollBar, probabilityGraph);

			return probabilityGraph;
		}
	}
}
