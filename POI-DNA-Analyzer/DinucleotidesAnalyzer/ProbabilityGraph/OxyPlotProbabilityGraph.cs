using OxyPlot.Series;
using OxyPlot;
using System.Drawing;
using OxyPlot.Wpf;
using OxyPlot.Axes;
using OxyPlot.Legends;

namespace POI_DNA_Analyzer
{
	class OxyPlotProbabilityGraph : IProbabilityGraph
	{
		public event Action Updated;

		private PlotView _plotView;
		private PlotModel _model;
		private LinearAxis _x;
		private LinearAxis _y;
		private LineSeries _lineSeries;

		public OxyPlotProbabilityGraph(PlotView plotView)
		{
			_plotView = plotView;
			_model = new PlotModel { };

			CreateLegend();
			AddXAxis();
			AddYAxis();
		}

		public PlotView View 
		{ 
			get { return _plotView; } 
		}

		public PlotModel Model 
		{ 
			get { return _model; } 
		}

		public int DataPointsCount
		{
			get { return _lineSeries == null ? 0 : _lineSeries.Points.Count; }
		}

		public void ProvideData(IEnumerable<int> dataIndexes, IEnumerable<double> dataProbabilities, Color color, string name)
		{
			List<int> indexes = new List<int>(dataIndexes);
			List<double> probabilities = new List<double>(dataProbabilities);

			_lineSeries = new LineSeries
			{
				MarkerType = MarkerType.Circle,
				MarkerSize = 4,
				MarkerStroke = OxyColors.White,
				Color = OxyColor.FromRgb(color.R, color.G, color.B),
				StrokeThickness = 2,
				Title = name
			};

			for (int i = 0; i < probabilities.Count; i++)
			{
				DataPoint dataPoint = new DataPoint(i, probabilities[i]);
				_lineSeries.Points.Add(dataPoint);
			}

			UpdateXAxis(indexes);

			_model.Series.Add(_lineSeries);
		}

		public void Show()
		{
			_plotView.Model = _model;
			_model.InvalidatePlot(true);
			Updated?.Invoke();
		}

		public void Clear()
		{
			_model.Series.Clear();
		}

		private void UpdateXAxis(List<int> indexes)
		{
			if (_model == null || _x == null)
				return;

			_x.LabelFormatter = value => FormatLabelValue(indexes, value);

			_plotView.InvalidatePlot();
		}

		private string FormatLabelValue(List<int> indexes, double value)
		{
			if (value >= indexes.Count || value < 0 || IsInteger(value) == false)
				return "";

			string result = indexes[Convert.ToInt32(value)].ToString();

			return result;
		}

		private bool IsInteger(double value)
		{
			return value == Math.Floor(value) || value == Math.Ceiling(value);
		}

		private void CreateLegend()
		{
			Legend legend = new Legend()
			{
				LegendPlacement = LegendPlacement.Outside,
				LegendPosition = LegendPosition.LeftMiddle,
				LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
				LegendBorder = OxyColors.Black,
				LegendFontSize = 12,
			};

			_model.Legends.Add(legend);
		}

		private void AddXAxis()
		{
			_x = new LinearAxis()
			{
				Position = AxisPosition.Bottom,
				Minimum = 1,
				Maximum = 5,
				MinorTickSize = 0
			};

			_model.Axes.Add(_x);
		}

		private void AddYAxis()
		{
			_y = new LinearAxis()
			{
				Position = AxisPosition.Left,
				Minimum = 0,
				Maximum = 100,
				FractionUnit = 10,
				MinorStep = 10,
				MajorStep = 20,
				LabelFormatter = value => $"{value}%",
				IsPanEnabled = false,
				IsZoomEnabled = false,
			};

			_model.Axes.Add(_y);
		}
	}
}
