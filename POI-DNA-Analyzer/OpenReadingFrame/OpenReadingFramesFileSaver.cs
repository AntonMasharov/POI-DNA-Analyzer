using System.IO;

namespace POI_DNA_Analyzer
{
	internal class OpenReadingFramesFileSaver
	{
		private string _resultFolderName = "OpenReadingFrames";
		private string _format = ".txt";

		public void Save(string filePath, string fileName, Dictionary<int, string> openReadingFrames, int minSizeToSave = 100)
		{
			string destination = Path.Combine(filePath, _resultFolderName);
			string text = MakeHeader();

			foreach (int key in openReadingFrames.Keys)
			{
				if (openReadingFrames[key].Length < minSizeToSave)
					continue;

				text += "\n" + key.ToString() + "," + openReadingFrames[key].Length + "," + openReadingFrames[key];
			}

			NoExtentionFileSaver noExtentionFileSaver = new NoExtentionFileSaver();
			noExtentionFileSaver.SaveTo(destination, fileName, text);
		}

		private string MakeHeader()
		{
			return "index,length,sequence";
		}
	}
}
