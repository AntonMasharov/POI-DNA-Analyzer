using Microsoft.Win32;
using System.IO;

namespace POI_DNA_Analyzer
{
	internal abstract class FileSaver
	{
		public abstract string Filter { get; }

		public virtual string DefaultExtenion { get; protected set; } = "";

		public string Save(string text)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = Filter;
			saveFileDialog.DefaultExt = DefaultExtenion;

			if (saveFileDialog.ShowDialog() == true)
			{
				File.WriteAllText(saveFileDialog.FileName, text);
				return saveFileDialog.FileName;
			}

			return "";
		}

		public string SaveTo(string destination, string fileName, string text)
		{
			FileCopyMaker fileCopyMaker = new FileCopyMaker();
			string path = fileCopyMaker.HandlePathWithWarning(destination, fileName);

			if (Path.Exists(destination) == false)
				Directory.CreateDirectory(destination);

			File.WriteAllText(path, text);

			return path;
		}
	}
}
