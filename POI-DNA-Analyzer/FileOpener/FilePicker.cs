using Microsoft.Win32;

namespace POI_DNA_Analyzer
{
	internal class FilePicker
    {
		public string PickFilePath()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Text and FASTA Files (*.txt;*.fasta)|*.txt;*.fasta|Text Files (*.txt)|*.txt|FASTA Files (*.fasta)|*.fasta\"";

			if (openFileDialog.ShowDialog() == true)
				return openFileDialog.FileName;
			else
				return "";
		}
	}
}
