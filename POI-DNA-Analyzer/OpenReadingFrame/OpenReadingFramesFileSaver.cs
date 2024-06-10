using Microsoft.Win32;
using System.IO;

namespace POI_DNA_Analyzer
{
	internal class OpenReadingFramesFileSaver
	{
		private string _resultFolderName = "OpenReadingFrames";
		private string _savePathWithoutName = AppDomain.CurrentDomain.BaseDirectory;
		private string _format = ".csv";

		public void Save(string fileName, IReadOnlyDictionary<int, string> openReadingFrames, int minSizeToSave = 100)
		{
			string destination = Path.Combine(_savePathWithoutName, _resultFolderName);
			fileName = "open-reading-frame-" + RemoveFileExtension(fileName) + _format;
			string text = MakeHeader();

			foreach (int key in openReadingFrames.Keys)
			{
				if (openReadingFrames[key].Length < minSizeToSave)
					continue;

				text += "\n" + key.ToString() + "," + openReadingFrames[key].Length + "," + openReadingFrames[key];
			}

			CSVFileSaver noExtentionFileSaver = new CSVFileSaver();
			noExtentionFileSaver.SaveTo(destination, fileName, text);
		}

		public void ChoosePath()
		{
			OpenFolderDialog openFolderDialog = new OpenFolderDialog();

			if (openFolderDialog.ShowDialog() == true)
			{
				_savePathWithoutName = openFolderDialog.FolderName;
			}
			else
			{
				return;
			}
		}

		public void ChangePath(string path)
		{
			if (path == "")
				return;

			_savePathWithoutName = path;
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
