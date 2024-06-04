using System.IO;

namespace POI_DNA_Analyzer
{
	internal class OpenReadingFramesFileSaver
	{
		public void Save(string filePath, string fileName, Dictionary<int, string> openReadingFrames, int minSizeToSave = 100)
		{
			CreateDirectory(filePath);
			string newFilePath = filePath + "OpenReadingFrames\\" + fileName;
			string text = MakeHeader();

			foreach (int key in openReadingFrames.Keys)
			{
				if (openReadingFrames[key].Length < minSizeToSave)
					continue;

				text += "\n" + key.ToString() + "," + openReadingFrames[key].Length + "," + openReadingFrames[key];
			}

			File.WriteAllText(newFilePath, text);
		}

		private string MakeHeader()
		{
			return "index,length,sequence";
		}

		private void CreateDirectory(string filePath)
		{
			string newFilePath = filePath + "OpenReadingFrames\\";

			if (File.Exists(newFilePath) == false)
				Directory.CreateDirectory(newFilePath);
		}
	}
}
