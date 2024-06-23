using Microsoft.Win32;
using System.IO;

namespace POI_DNA_Analyzer
{
	internal class OpenReadingFramesFileSaver
	{
		private CommonFilePath _commonFilePath;
		private FileSaver _fileSaver;

		private string _resultFolderName = "OpenReadingFrames";
		private string _saveDestination = "";
		private string _format = ".csv";

		public OpenReadingFramesFileSaver(CommonFilePath commonFilePath)
		{
			_commonFilePath = commonFilePath;
			_fileSaver = GetFileSaver();
		}

		private FileSaver GetFileSaver()
		{
			return new CSVFileSaver();
		}

		public void Save(string fileName, IReadOnlyDictionary<int, string> openReadingFrames, int minSizeToSave = 100)
		{
			string destination = GetDestination();
			fileName = "open-reading-frame-" + RemoveFileExtension(fileName) + _format;
			string text = MakeHeader();

			foreach (int key in openReadingFrames.Keys)
			{
				if (openReadingFrames[key].Length < minSizeToSave)
					continue;

				text += "\n" + key.ToString() + "," + openReadingFrames[key].Length + "," + openReadingFrames[key];
			}

			_fileSaver.SaveTo(destination, fileName, text);
		}

		public void ChoosePath()
		{
			OpenFolderDialog openFolderDialog = new OpenFolderDialog();

			if (openFolderDialog.ShowDialog() == true)
			{
				_saveDestination = openFolderDialog.FolderName;
			}
			else
			{
				return;
			}
		}

		public void ChangePath(string filePathWithoutName)
		{
			if (filePathWithoutName == "" || filePathWithoutName == null)
				return;

			_saveDestination = filePathWithoutName;
		}

		private string GetDestination()
		{
			string destination;

			if (_saveDestination == "")
				destination = Path.Combine(_commonFilePath.FilePath, _resultFolderName);
			else
				destination = Path.Combine(_saveDestination, _resultFolderName);

			return destination;
		}

		private string MakeHeader()
		{
			return "index,length,sequence";
		}

		private string RemoveFileExtension(string filename)
		{
			int lastDotIndex = filename.LastIndexOf('.');

			if (lastDotIndex == -1)
			{
				return filename;
			}

			return filename.Substring(0, lastDotIndex);
		}
	}
}
