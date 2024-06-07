namespace POI_DNA_Analyzer
{
	class ComplementaryDNASaver
    {
		private CommonFilePath _commonFilePath;
		private string _format = ".txt";

		public ComplementaryDNASaver(CommonFilePath commonFilePath)
		{
			_commonFilePath = commonFilePath;
		}

		public void Save(string text)
		{
			if (text == null || text == "")
				return;

			string fileName = "complementary-sequence" + _format;
			string destination = _commonFilePath.FilePath;

			if (_commonFilePath.IsRootFileDestinationChosen == false)
				return;

			NoExtentionFileSaver noExtentionFileSaver = new NoExtentionFileSaver();
			noExtentionFileSaver.SaveTo(destination, fileName, text);
		}

		public void SaveIndividually(string text)
		{
			if (text == null || text == "")
				return;

			NoExtentionFileSaver noExtentionFileSaver = new NoExtentionFileSaver();
			noExtentionFileSaver.Save(text);
		}
    }
}
