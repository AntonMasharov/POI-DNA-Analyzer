using System.IO;

namespace POI_DNA_Analyzer
{
	internal abstract class ResultSaver
	{
		private CommonFilePath _commonFilePath;
		private FileSaver _fileSaver;

		private string _path = "";

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

		public string GetFullPath()
		{
			return _path;
		}

		public virtual bool Save()
		{
			if (CanSave() == false)
				return false;

			string fileName = GetFileName();
			string destination = GetDestination();

			if (_commonFilePath.IsRootFileDestinationChosen == false)
				return false;

			_path = _fileSaver.SaveTo(destination, fileName, GetContent());

			return true;
		}

		public virtual bool SaveIndividually()
		{
			if (CanSave() == false)
				return false;

			_path = _fileSaver.Save(GetContent());

			return true;
		}
	}
}
