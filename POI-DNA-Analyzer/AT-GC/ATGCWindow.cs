using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	class ATGCWindow
    {
		private TextBlock _textBlock;
		private ChunkAnalyzer _chunkAnalyzer;
		private CommonFilePath _commonFilePath;
		private ATGCResultSaver _resultSaver;
		double _atCount = 0;
		double _gcCount = 0;
		double _wholeCount = 0;
		double _atPercent = 0;
		double _gcPercent = 0;

		public ATGCWindow(TextBlock textBlock, CommonFilePath commonFilePath)
		{
			_chunkAnalyzer = new ChunkAnalyzer();
			_textBlock = textBlock;
			_commonFilePath = commonFilePath;
			_resultSaver = new ATGCResultSaver(_commonFilePath);
		}

		public void Start(string text)
		{
			if (text == "")
				return;

			_chunkAnalyzer.AnalyzeChunk(text);

			_atCount = GetNucleotideCount('A') + GetNucleotideCount('T');
			_gcCount = GetNucleotideCount('G') + GetNucleotideCount('C');
			_wholeCount = _atCount + _gcCount;

			_atPercent = new PercentCalculator().GetPercent(_atCount, _wholeCount) * 100;
			_gcPercent = new PercentCalculator().GetPercent(_gcCount, _wholeCount) * 100;

			UpdateText();
		}

		public void Save()
		{
			_resultSaver.Save(_textBlock.Text);
		}

		public void SaveIndividually()
		{
			_resultSaver.SaveIndividually(_textBlock.Text);
		}

		private int GetNucleotideCount(char nucleotide)
		{
			if (_chunkAnalyzer.NucleotidesCount.ContainsKey(nucleotide) == false)
				return 0;

			return _chunkAnalyzer.NucleotidesCount[nucleotide];
		}

		private void UpdateText()
		{
			_textBlock.Text = $"A-T: {_atPercent.ToString("0.00")}%\nG-C: {_gcPercent.ToString("0.00")}%";
		}
    }
}
