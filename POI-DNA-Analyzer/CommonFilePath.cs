using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace POI_DNA_Analyzer
{
	internal class CommonFilePath
	{
		private string _rootFileDestination = "";
		private string _sequenceFolderName = "";
		private string _fullPath = "";

		public string FilePath 
		{  
			get 
			{ 
				if (IsRootFileDestinationChosen == false)
					ChooseFolder();

				return _fullPath; 
			} 
		}

		public bool IsRootFileDestinationChosen { get { return _rootFileDestination != "";  } }

		public void ChooseFolder()
		{
			OpenFolderDialog openFolderDialog = new OpenFolderDialog();

			if (openFolderDialog.ShowDialog() == true)
			{
				_rootFileDestination = openFolderDialog.FolderName;
			}

			TryMakeFullPath();
			TryCreateDirectory();
		}

		public void TrySetSequenceFolderName(string name)
		{
			if (name == "")
				_sequenceFolderName = GetDefaultSequenceFolderName();
			else
				_sequenceFolderName = name;

			TryMakeFullPath();
		}

		public void TryMakeFullPath()
		{
			if (IsRootFileDestinationChosen == false || _sequenceFolderName == "")
				return;

			_fullPath = Path.Combine(_rootFileDestination, _sequenceFolderName);
		}

		private string GetDefaultSequenceFolderName()
		{
			return "sequence-" + DateTime.Now.Date.ToString("yyyy-MM-dd");
		}

		private void FormatDefaultSequenceFolderName(int i)
		{
			_sequenceFolderName = $"sequence-{DateTime.Now.Date.ToString("yyyy-MM-dd")}({i})";
			TryMakeFullPath();

			if (Path.Exists(_fullPath) == true)
				FormatDefaultSequenceFolderName(++i);
		}

		private void TryCreateDirectory()
		{
			if (Path.Exists(_fullPath) == true)
				AskUser();

			Directory.CreateDirectory(_fullPath);
		}

		private void AskUser()
		{
			MessageBoxResult result = MessageBox.Show(
				"A destination with this sequence name already exists. Do you want to use it?",
				"Folder Exists",
				MessageBoxButton.YesNoCancel,
				MessageBoxImage.Warning
			);

			if (result == MessageBoxResult.No)
				FormatDefaultSequenceFolderName(1);
		}
	}
}
