using System.IO;
using System.Windows;

namespace POI_DNA_Analyzer
{
	internal class FileCopyMaker
	{
		private string _destination = "";
		private string _fileName = "";
		private string _path = "";

		public string HandlePath(string destination, string fileName)
		{
			UpdateFields(destination, fileName);

			if (File.Exists(_path) == false)
				return _path;

			FormatPath(1);
			return _path;
		}

		public string HandlePathWithWarning(string destination, string fileName)
		{
			UpdateFields(destination, fileName);

			if (File.Exists(_path) == false)
				return _path;

			AskUser();
			return _path;
		}

		private void UpdateFields(string destination, string fileName)
		{
			_destination = destination;
			_fileName = fileName;
			_path = Path.Combine(_destination, _fileName);
		}

		private void FormatPath(int i)
		{
			string fileName = RemoveFileExtension(_fileName) + $"({i})" + GetFileExtension(_fileName);
			_path = Path.Combine(_destination, fileName);

			if (File.Exists(_path) == true)
				FormatPath(++i);
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

		private string GetFileExtension(string filename)
		{
			int lastDotIndex = filename.LastIndexOf('.');

			if (lastDotIndex == -1)
			{
				return "";
			}

			int length = filename.Length - lastDotIndex;
			return filename.Substring(lastDotIndex, length);
		}

		private void AskUser()
		{
			MessageBoxResult result = MessageBox.Show(
			"A file with this name already exists. Do you want to replace it?",
			"File Exists",
			MessageBoxButton.YesNoCancel,
			MessageBoxImage.Warning
			);

			if (result == MessageBoxResult.No)
				FormatPath(1);
		}
	}
}
