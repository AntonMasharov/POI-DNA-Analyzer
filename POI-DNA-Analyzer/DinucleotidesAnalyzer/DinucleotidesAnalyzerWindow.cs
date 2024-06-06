using OxyPlot.Wpf;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace POI_DNA_Analyzer
{
	internal class DinucleotidesAnalyzerWindow
	{
		private DinucleotidesAnalyzerProgressBar _dinucleotidesAnalyzerProgressBar;
		private DinucleotidesAnalyzer _dinucleotidesAnalyzer;
		private IProbabilityGraph _probabilityGraph;
		private CheckBox _checkBox;
		private CommonFilePath _commonFilePath;

		private string _fileText = "";
		private string _currentDinucleotide = "A";
		private int _defaultChunkSize = 100;
		private int _chunkSize;
		private double _similarityCoefficient;

		public DinucleotidesAnalyzerWindow(ProgressBar progressBar, PlotView plotView, CheckBox checkBox, ScrollBar scrollBar, CommonFilePath commonFilePath)
		{
			_dinucleotidesAnalyzer = new DinucleotidesAnalyzer(new CosineSimilarityMatrixComparator());
			_checkBox = checkBox;
			_commonFilePath = commonFilePath;

			ProbabilityGraphFactory probabilityGraphFactory = new ProbabilityGraphFactory(plotView, scrollBar);
			_probabilityGraph = probabilityGraphFactory.Get();

			_dinucleotidesAnalyzerProgressBar = new DinucleotidesAnalyzerProgressBar(progressBar);
			_dinucleotidesAnalyzer.Executing += _dinucleotidesAnalyzerProgressBar.Update;
			_dinucleotidesAnalyzer.Started += _dinucleotidesAnalyzerProgressBar.Show;
		}

		~DinucleotidesAnalyzerWindow()
		{
			_dinucleotidesAnalyzer.Executing -= _dinucleotidesAnalyzerProgressBar.Update;
			_dinucleotidesAnalyzer.Started -= _dinucleotidesAnalyzerProgressBar.Show;
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

		}

		public void SaveIndividually()
		{
			DinucleotidesAnalyzerResultSaver resultSaver = new DinucleotidesAnalyzerResultSaver(_dinucleotidesAnalyzer, _commonFilePath);
			resultSaver.SaveIndividually();
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
