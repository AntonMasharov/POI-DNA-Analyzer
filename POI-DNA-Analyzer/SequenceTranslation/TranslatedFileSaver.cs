using System.IO;

namespace POI_DNA_Analyzer
{
	internal class TranslatedFileSaver
	{
		private string _savePathWithoutName = "";

		public TranslatedFileSaver() 
		{ 
			SetDefaultPath();
		}

		public List<string> ResultFilesPaths { get; private set; } = new List<string>();

		public List<string> ResultFilesPathsWithoutName { get; private set; } = new List<string>();

		public List<string> ResultFilesNames { get; private set; } = new List<string>();

		public void Save(string textToSave, string fileName)
		{
			string fullPath = Path.Combine(_savePathWithoutName, fileName);
			File.WriteAllText(fullPath, textToSave);

			ResultFilesPathsWithoutName.Add(_savePathWithoutName);
			ResultFilesPaths.Add(fullPath);
			ResultFilesNames.Add(fileName);
		}

		public void ChangePath(string filePathWithoutName)
		{
			if (filePathWithoutName == "" || filePathWithoutName == null)
				return;

			_savePathWithoutName = filePathWithoutName;
		}

		private void SetDefaultPath()
		{
			_savePathWithoutName = AppDomain.CurrentDomain.BaseDirectory;
		}
	}
}
