using OxyPlot.Wpf;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace POI_DNA_Analyzer
{
	class ATGCWindowController
    {
		private TextBlock _resultTextBlock;
		private ATGCCounter _counter;
		private ChunkAnalyzer _chunkAnalyzer;
		private CommonFilePath _commonFilePath;
		private ResultSaver _resultSaver;
		private IProbabilityGraph _probabilityGraph;

		private int _defaultChunkSize = 100;
		private int _chunkSize;
		double _atPercent = 0;
		double _gcPercent = 0;

		public ATGCWindowController(PlotView plotView, ScrollBar scrollBar, TextBlock resultTextBlock, CommonFilePath commonFilePath)
		{
			_chunkAnalyzer = new ChunkAnalyzer();
			_counter = new ATGCCounter();
			_resultTextBlock = resultTextBlock;
			_commonFilePath = commonFilePath;
			_resultSaver = new ATGCResultSaver(_counter, _commonFilePath);

			ProbabilityGraphFactory probabilityGraphFactory = new ProbabilityGraphFactory(plotView, scrollBar);
			_probabilityGraph = probabilityGraphFactory.Get();
		}

		public void Start(string text, int chunkSize = 100)
		{
			if (text == "")
				return;

			if (chunkSize == 0)
				_chunkSize = _defaultChunkSize;
			else
				_chunkSize = chunkSize;

			_counter.CountByChunk(text, _chunkSize);
			_counter.CountOverall(text);

			_atPercent = _counter.ATPercent;
			_gcPercent = _counter.GCPercent;

			_probabilityGraph.Clear();
			UpdateText();
			ShowGraph();
		}

		public string Save()
		{
			_resultSaver.Save();
			return _resultSaver.GetFullPath();
		}

		public string SaveIndividually()
		{
			_resultSaver.SaveIndividually();
			return _resultSaver.GetFullPath();
		}

		private void ShowGraph()
		{
			List<System.Drawing.Color> listOfColors = new List<System.Drawing.Color>()
			{
				System.Drawing.Color.Red,
				System.Drawing.Color.Green,
				System.Drawing.Color.Blue,
				System.Drawing.Color.Orange,
			};

			int i = 0;

			foreach (string key in _counter.Percents.Keys.ToList())
			{
				_probabilityGraph.ProvideData(_counter.Indexes, _counter.Percents[key], listOfColors[i], key);
				i++;
			}

			_probabilityGraph.Show();
		}

		private void UpdateText()
		{
			_resultTextBlock.Text = $"A-T: {_atPercent.ToString("0.00")}%\nG-C: {_gcPercent.ToString("0.00")}%";
		}
    }
}
