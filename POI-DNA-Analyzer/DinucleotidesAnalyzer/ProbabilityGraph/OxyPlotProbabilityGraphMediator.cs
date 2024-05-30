using OxyPlot;
using OxyPlot.Series;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace POI_DNA_Analyzer
{
	internal class OxyPlotProbabilityGraphMediator
	{
		private ScrollBar _scrollBar;
		private OxyPlotProbabilityGraph _oxyPlotProbabilityGraph;

		public OxyPlotProbabilityGraphMediator(ScrollBar scrollBar, OxyPlotProbabilityGraph oxyPlotProbabilityGraph)
		{
			_scrollBar = scrollBar;
			_oxyPlotProbabilityGraph = oxyPlotProbabilityGraph;

			SubscribeToEvents();
			SetNewScrollbarValues();
		}

		~OxyPlotProbabilityGraphMediator()
		{
			UnsubscribeFromEvents();
		}

		private void SubscribeToEvents()
		{
			_oxyPlotProbabilityGraph.Updated += UpdatePlotView;

			_scrollBar.PreviewMouseDown += UnsubscribeScrollBarFromPlotEvents;
			_scrollBar.PreviewMouseUp += SubscribeScrollBarToPlotEvents;
			_oxyPlotProbabilityGraph.View.DragEnter += UnsubscribePlotFromScrollBarEvents;
			_oxyPlotProbabilityGraph.View.DragEnter += SubscribePlotToScrollBarEvents;

			_scrollBar.Scroll += UpdatePlotView;
			_oxyPlotProbabilityGraph.Model.Axes[0].TransformChanged += UpdateScrollBar;
		}

		private void UnsubscribeFromEvents() 
		{
			_oxyPlotProbabilityGraph.Updated -= UpdatePlotView;

			_scrollBar.PreviewMouseDown -= UnsubscribeScrollBarFromPlotEvents;
			_scrollBar.PreviewMouseUp -= SubscribeScrollBarToPlotEvents;
			_oxyPlotProbabilityGraph.View.DragEnter -= UnsubscribePlotFromScrollBarEvents;
			_oxyPlotProbabilityGraph.View.DragEnter -= SubscribePlotToScrollBarEvents;

			_scrollBar.Scroll -= UpdatePlotView;
			_oxyPlotProbabilityGraph.Model.Axes[0].TransformChanged -= UpdateScrollBar;
		}

		private void SubscribePlotToScrollBarEvents(object sender, DragEventArgs e)
		{
			_scrollBar.Scroll += UpdatePlotView;
		}

		private void UnsubscribePlotFromScrollBarEvents(object sender, DragEventArgs e)
		{
			_scrollBar.Scroll -= UpdatePlotView;
		}

		private void SubscribeScrollBarToPlotEvents(object? sender, MouseButtonEventArgs e)
		{
			_oxyPlotProbabilityGraph.Model.Axes[0].TransformChanged += UpdateScrollBar;
		}

		private void UnsubscribeScrollBarFromPlotEvents(object? sender, MouseButtonEventArgs e)
		{
			_oxyPlotProbabilityGraph.Model.Axes[0].TransformChanged -= UpdateScrollBar;
		}

		private void SetNewScrollbarValues()
		{
			int totalDataPoints = _oxyPlotProbabilityGraph.DataPointsCount;
			int visibleRange = GetNumberOfVisiblePoints();

			_scrollBar.Maximum = Math.Abs(totalDataPoints - visibleRange);
			_scrollBar.ViewportSize = visibleRange;
			_scrollBar.SmallChange = 1;
			_scrollBar.LargeChange = visibleRange;
		}

		private void UpdatePlotView(object? sender, EventArgs e)
		{
			UpdatePlotView();
		}

		private void UpdatePlotView()
		{
			if (_oxyPlotProbabilityGraph.Model == null)
				return;

			double start = _scrollBar.Value;
			double end = start + _scrollBar.ViewportSize;

			_oxyPlotProbabilityGraph.Model.Axes[0].Zoom(start, end);
			_oxyPlotProbabilityGraph.Model.InvalidatePlot(false);
		}

		private void UpdateScrollBar(object? sender, EventArgs e)
		{
			double newValue = _oxyPlotProbabilityGraph.Model.Axes[0].ActualMinimum;

			if (newValue <= -1 || newValue >= _oxyPlotProbabilityGraph.DataPointsCount)
				return;

			_scrollBar.Value = newValue;
			SetNewScrollbarValues();
		}

		private int GetNumberOfVisiblePoints()
		{
			if (_oxyPlotProbabilityGraph.Model == null || _oxyPlotProbabilityGraph.Model.Series.Count == 0)
				return 0;

			LineSeries lineSeries = _oxyPlotProbabilityGraph.Model.Series.OfType<LineSeries>().First();
			List<DataPoint> points = new List<DataPoint>();

			foreach (DataPoint point in lineSeries.Points)
			{
				if (lineSeries.GetScreenRectangle().Contains(lineSeries.Transform(point)))
				{
					points.Add(point);
				}
			}

			return points.Count;
		}
	}
}
