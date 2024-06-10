namespace POI_DNA_Analyzer
{
	class PalindromicSequencesFinderWindow
    {
		private PalindromicSequencesFinder _palindromicSequencesFinder;
		private PalindromicSequencesFinderResultSaver _resultSaver;

		public PalindromicSequencesFinderWindow(CommonFilePath commonFilePath)
		{
			_palindromicSequencesFinder = new PalindromicSequencesFinder();
			_resultSaver = new PalindromicSequencesFinderResultSaver(commonFilePath);
		}

		public void Start(string text)
		{
			if (text == "")
				return;

			_palindromicSequencesFinder.Start(text);
		}

		public void Save()
		{
			if (_palindromicSequencesFinder.PalindromicSequences == null
				|| _palindromicSequencesFinder.PalindromicSequences.Count == 0)
				return;

			_resultSaver.Save(_palindromicSequencesFinder.PalindromicSequences);
		}

		public void SaveIndividually()
		{
			if (_palindromicSequencesFinder.PalindromicSequences == null
				|| _palindromicSequencesFinder.PalindromicSequences.Count == 0)
				return;

			_resultSaver.SaveIndividually(_palindromicSequencesFinder.PalindromicSequences);
		}
	}
}
