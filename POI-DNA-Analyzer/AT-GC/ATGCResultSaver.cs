namespace POI_DNA_Analyzer
{
	class ATGCResultSaver
    {
		private CommonFilePath _commonFilePath;
		private string _format = ".txt";

		public ATGCResultSaver(CommonFilePath commonFilePath)
		{
			_commonFilePath = commonFilePath;
		}

		public void Save(string text)
		{
			if (text == "")
				return;

			string fileName = "at-gc-percent" + _format;
			string destination = _commonFilePath.FilePath;

			if (_commonFilePath.IsRootFileDestinationChosen == false)
				return;

			NoExtentionFileSaver fileSaver = new NoExtentionFileSaver();
			fileSaver.SaveTo(destination, fileName, text);
		}

		public void SaveIndividually(string text)
		{
			if (text == "")
				return;

			NoExtentionFileSaver fileSaver = new NoExtentionFileSaver();
			fileSaver.Save(text);
		}
	}
}
