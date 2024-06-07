using Microsoft.Win32;

namespace POI_DNA_Analyzer
{
	internal class FilePicker
    {
		public string FilterCSV { get; private set; } = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";

		public string FilterTXTandFASTA { get; private set; } = "Text and FASTA Files (*.txt;*.fasta)|*.txt;*.fasta|Text Files (*.txt)|*.txt|FASTA Files (*.fasta)|*.fasta\"";

		public string PickFilePath(string filter)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = filter;

			if (openFileDialog.ShowDialog() == true)
				return openFileDialog.FileName;
			else
				return "";
		}
	}
}
