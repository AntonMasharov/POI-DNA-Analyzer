using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	internal class SequencesFinderWindowController
	{
		private ResultText _resultText;
		private ListOfIndexes _listOfIndexes;
		private Languages _language = Languages.Russian;

		private ISequencesFinder _sequencesFinder;
		private SequenceFinderOutput _sequenceFinderOutput;
		private ResultSaver _resultSaver;
		private CommonFilePath _commonFilePath;

		public SequencesFinderWindowController(TextBlock resultText, ListBox listOfIndexes, CommonFilePath commonFilePath)
		{
			_resultText = new ResultText(resultText);
			_listOfIndexes = new ListOfIndexes(listOfIndexes);
			_sequencesFinder = new SequencesFinder();
			_commonFilePath = commonFilePath;
			_sequenceFinderOutput = new SequenceFinderOutput();
			_resultSaver = new SequenceFinderResultSaver(_sequenceFinderOutput, _commonFilePath);
		}

		public void Find(string sequenceToFind, string text)
		{
			if (text == null || text == "")
				return;

			Clear();
			_sequencesFinder.GetOccurrencesCount(text, sequenceToFind);
			_sequenceFinderOutput.Update(_sequencesFinder.OccurencesIndexes);

			UpdateUI();
		}

		public void Save()
		{
			_resultSaver.Save();
		}

		public void SaveIndividually()
		{
			_resultSaver.SaveIndividually();
		}

		public void Clear()
		{
			_resultText.Clear();
			_listOfIndexes.Clear();
		}

		public void ChangeResultLanguage(Languages language)
		{
			_language = language;
		}

		private void UpdateUI()
		{
			_resultText.ShowOccurrencesCount(_sequenceFinderOutput.GetOccurencesCountText(_language));
			_listOfIndexes.ShowOccurrencesIndexes(_sequenceFinderOutput.OccurencesIndexes);
		}
	}
}
