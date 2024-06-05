using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	internal class SequencesFinderWindow
	{
		private ResultText _resultText;
		private ListOfIndexes _listOfIndexes;
		private SequencesFinder _sequencesFinder;

		public SequencesFinderWindow(TextBlock resultText, ListBox listOfIndexes)
		{
			_resultText = new ResultText(resultText);
			_listOfIndexes = new ListOfIndexes(listOfIndexes);
			_sequencesFinder = new SequencesFinder();
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
			SequenceFinderResultSaver resultSaver = new SequenceFinderResultSaver();

			resultSaver.Save(content, _sequencesFinder.SequenceIndexes);
		}

		public void Clear()
		{
			_resultText.Clear();
			_listOfIndexes.Clear();
		}
	}
}
