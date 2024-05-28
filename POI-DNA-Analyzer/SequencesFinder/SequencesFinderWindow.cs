﻿using System.IO;
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

		public void Find(string sequenceToFind, StreamReader fileStream)
		{
			if (fileStream == null)
				return;

			int result = _sequencesFinder.GetOccurrencesCount(fileStream.ReadToEnd(), sequenceToFind);

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