using Microsoft.Win32;
using System.IO;

namespace POI_DNA_Analyzer
{
	internal class ComplementaryDNAFileSaver
	{
		public void Save(string text)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();

			if (saveFileDialog.ShowDialog() == true)
			{
				File.WriteAllText(saveFileDialog.FileName, text);
			}
		}
	}
}
