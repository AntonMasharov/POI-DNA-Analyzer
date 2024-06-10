namespace POI_DNA_Analyzer
{
	internal abstract class ResultSaver
	{
		private CommonFilePath _commonFilePath;
		private FileSaver _fileSaver;

		public ResultSaver(CommonFilePath commonFilePath)
		{ 
			_commonFilePath = commonFilePath;
			_fileSaver = GetFileSaver();
		}

		public abstract FileSaver GetFileSaver();

		public abstract string GetFileName();

		public abstract string GetDestination();

		public abstract string GetContent();

		public abstract bool CanSave();

		public void Save()
		{
			if (CanSave() == false)
				return;

			string fileName = GetFileName();
			string destination = GetDestination();

			if (_commonFilePath.IsRootFileDestinationChosen == false)
				return;

			_fileSaver.SaveTo(destination, fileName, GetContent());
		}

		public void SaveIndividually()
		{
			if (CanSave() == false)
				return;

			_fileSaver.Save(GetContent());
		}
	}
}
