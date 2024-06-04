using Microsoft.Win32;
using System.IO;

namespace POI_DNA_Analyzer
{
	internal abstract class FileSaver
	{
		public abstract string Filter { get; }

		public virtual string DefaultExtenion { get; protected set; } = "";

		public void Save(string text)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = Filter;
			saveFileDialog.DefaultExt = DefaultExtenion;

			if (saveFileDialog.ShowDialog() == true)
			{
				File.WriteAllText(saveFileDialog.FileName, text);
			}
		}
	}
}
