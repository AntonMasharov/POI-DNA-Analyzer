using OxyPlot.Wpf;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace POI_DNA_Analyzer
{
	internal class DinucleotidesAnalyzerWindow
	{
		private DinucleotidesAnalyzer _dinucleotidesAnalyzer;
		private IProbabilityGraph _probabilityGraph;
		private CheckBox _checkBox;

		private string _fileText = "";
		private string _currentDinucleotide = "A";
		private int _defaultChunkSize = 100;
		private int _chunkSize;
		private double _similarityCoefficient;

		public DinucleotidesAnalyzerWindow(PlotView plotView, CheckBox checkBox, ScrollBar scrollBar)
		{
			_dinucleotidesAnalyzer = new DinucleotidesAnalyzer(new CosineSimilarityMatrixComparator());
			_checkBox = checkBox;

			ProbabilityGraphFactory probabilityGraphFactory = new ProbabilityGraphFactory(plotView, scrollBar);
			_probabilityGraph = probabilityGraphFactory.Get();
		}

		public void UpdateText(string text)
		{
			if (text == null || text == "") 
				return;

			_fileText = text;
		}

		public void Analyze(TextBox textBox, double similarityCoefficient)
		{
			_similarityCoefficient = similarityCoefficient;

			if (int.TryParse(textBox.Text, out _chunkSize) == false)
				_chunkSize = _defaultChunkSize;

			if (_chunkSize <= 0)
				_chunkSize = _defaultChunkSize;

			ShowGraph();
		}

		public void Save()
		{
			DinucleotidesAnalyzerResultSaver resultSaver = new DinucleotidesAnalyzerResultSaver(_dinucleotidesAnalyzer);

			resultSaver.Save();
		}

		public void ShowGraph(string tag)
		{
			_currentDinucleotide = tag;
			ShowGraph();
		}

		private void ShowGraph()
		{
			if (_fileText == null || _fileText == "")
				return;

			_dinucleotidesAnalyzer.Analyze(_fileText, _chunkSize, _similarityCoefficient, RetrieveCheckBoxInfo());
			_probabilityGraph.Clear();

			if (_currentDinucleotide.Length == 1)
				ShowNN();
			else
				ShowN();

			_probabilityGraph.Show();
		}

		private void ShowNN()
		{
			List<System.Drawing.Color> listOfColors = new List<System.Drawing.Color>()
			{
				System.Drawing.Color.Red,
				System.Drawing.Color.Green,
				System.Drawing.Color.Blue,
				System.Drawing.Color.Orange,
			};

			int i = 0;

			foreach (string key in _dinucleotidesAnalyzer.DinucleotidesProbabilities.Keys.ToList())
			{
				if (key[0].ToString() == _currentDinucleotide[0].ToString())
				{
					_probabilityGraph.ProvideData(_dinucleotidesAnalyzer.Indexes, _dinucleotidesAnalyzer.DinucleotidesProbabilities[key], listOfColors[i], key);
					i++;
				}
			}
		}

		private void ShowN()
		{
			_probabilityGraph.ProvideData(_dinucleotidesAnalyzer.Indexes, _dinucleotidesAnalyzer.DinucleotidesProbabilities[_currentDinucleotide], System.Drawing.Color.Red, _currentDinucleotide);
		}

		private bool RetrieveCheckBoxInfo()
		{
			if (_checkBox.IsChecked == true)
				return true;
			else
				return false; 
		}
	}
}
