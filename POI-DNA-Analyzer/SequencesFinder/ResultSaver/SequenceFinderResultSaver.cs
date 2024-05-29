using Microsoft.Win32;
using System.IO;
using System.Text;

namespace POI_DNA_Analyzer
{
	internal class SequenceFinderResultSaver
	{
		public void Save(string countInfo, LinkedList<int> indexes)
		{
			if (countInfo == "")
				return;

			SaveFileDialog saveFileDialog = new SaveFileDialog();

			if (saveFileDialog.ShowDialog() == true)
			{
				File.WriteAllText(saveFileDialog.FileName, BuildWholeString(countInfo, indexes));
			}
		}

		private string BuildIndexes(LinkedList<int> indexes)
		{
			StringBuilder sb = new StringBuilder();

			foreach (int index in indexes)
			{
				sb.Append(index.ToString());
				sb.Append(", ");
			}

			if (sb.Length > 0)
				sb.Length -= 2;

			string result = sb.ToString() + "\n";

			return result;
		}

		private string BuildWholeString(string countInfo, LinkedList<int> indexes)
		{
			return countInfo + "\n\n" + BuildIndexes(indexes);
		}
	}
}
