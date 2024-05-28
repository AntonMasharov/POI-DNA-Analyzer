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
		private PlotView _plotView;
		private PlotModel _model;
		private LinearAxis _x;
		private LinearAxis _y;
		private double _viewSpan = 10;

		public OxyPlotProbabilityGraph(PlotView plotView)
		{
			_plotView = plotView;
			_model = new PlotModel { };

			CreateLegend();
			AddXAxis();
			AddYAxis();
		}

		public void ProvideData(List<int> indexes, List<double> probabilities, Color color, string name)
		{
			LineSeries series = new LineSeries
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
				series.Points.Add(dataPoint);
			}

			UpdateXAxis(indexes);

			_model.Series.Add(series);
		}

		public void Show()
		{
			_plotView.Model = _model;

			_model.InvalidatePlot(true);
		}

		public void Clear()
		{
			_model.Series.Clear();
		}

		public void SetXAxisViewRange(double offset)
		{
			_x.Minimum = offset;
			_x.Maximum = offset + _viewSpan;
			_model.InvalidatePlot(true);
		}

		private void UpdateXAxis(List<int> indexes)
		{
			if (_model == null || _x == null)
				return;

			_x.MajorStep = 1;
			_x.MinorStep = 1;
			_x.LabelFormatter = value => FormatValue(indexes, value);

			_plotView.InvalidatePlot();
		}

		private string FormatValue(List<int> indexes, double value)
		{
			if (value >= indexes.Count || value < 0)
				return "";

			string result = indexes[Convert.ToInt32(value)].ToString();

			return result;
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
				FractionUnit = 1,
				MinorStep = 1,
				MajorStep = 1,
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
