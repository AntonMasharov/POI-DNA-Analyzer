using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	internal class SequencesFinderWindow
	{
		private ResultText _resultText;
		private ListOfIndexes _listOfIndexes;
		private SequencesFinder _sequencesFinder;
		private CommonFilePath _commonFilePath;

		public SequencesFinderWindow(TextBlock resultText, ListBox listOfIndexes, CommonFilePath commonFilePath)
		{
			_resultText = new ResultText(resultText);
			_listOfIndexes = new ListOfIndexes(listOfIndexes);
			_sequencesFinder = new SequencesFinder();
			_commonFilePath = commonFilePath;
		}

		public void Find(string sequenceToFind, string text)
		{
			if (text == null || text == "")
				return;

			Clear();

			int result = _sequencesFinder.GetOccurrencesCount(text, sequenceToFind);

			_resultText.ShowOccurrencesCount(result.ToString());
			_listOfIndexes.ShowOccurrencesIndexes(_sequencesFinder.SequenceIndexes);
		}

		public void Save(string content)
		{
			SequenceFinderResultSaver resultSaver = new SequenceFinderResultSaver(_commonFilePath);
			resultSaver.Save(content, _sequencesFinder.SequenceIndexes);
		}

		public void SaveIndividually(string content)
		{
			SequenceFinderResultSaver resultSaver = new SequenceFinderResultSaver(_commonFilePath);
			resultSaver.SaveIndividually(content, _sequencesFinder.SequenceIndexes);
		}

		public void Clear()
		{
			_resultText.Clear();
			_listOfIndexes.Clear();
		}
	}
}
