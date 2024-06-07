using System.IO;

namespace POI_DNA_Analyzer
{
	internal class TranslatedFileSaver
	{
		private string _resultFolderName = "TranslationResult";
		private string _savePathWithoutName = "";
		private string _defaultpathWithoutName = AppDomain.CurrentDomain.BaseDirectory;

		public bool IsCustomPathSet { get { return _savePathWithoutName != ""; } }

		public List<string> ResultFilesPaths { get; private set; } = new List<string>();

		public List<string> ResultFilesPathsWithoutName { get; private set; } = new List<string>();

		public List<string> ResultFilesNames { get; private set; } = new List<string>();

		public void Save(string textToSave, string fileName)
		{
			string destination;

            if (_savePathWithoutName == "")
				destination = Path.Combine(_defaultpathWithoutName, _resultFolderName);
			else
				destination = Path.Combine(_savePathWithoutName, _resultFolderName);

			NoExtentionFileSaver noExtentionFileSaver = new NoExtentionFileSaver();
			noExtentionFileSaver.SaveTo(destination, fileName, textToSave);

			ResultFilesPathsWithoutName.Add(_savePathWithoutName);
			ResultFilesPaths.Add(destination);
			ResultFilesNames.Add(fileName);
		}

		public void ChangePath(string filePathWithoutName)
		{
			if (filePathWithoutName == "" || filePathWithoutName == null)
				return;

			_savePathWithoutName = filePathWithoutName;
		}

		public void ClearLists()
		{
			ResultFilesPaths.Clear();
			ResultFilesNames.Clear();
			ResultFilesPathsWithoutName.Clear();
		}

		public void ClearPath()
		{
			_savePathWithoutName = "";
		}
	}
}
