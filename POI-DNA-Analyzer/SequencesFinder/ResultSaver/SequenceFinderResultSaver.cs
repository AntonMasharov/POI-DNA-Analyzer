using System.IO;
using System.Text;
namespace POI_DNA_Analyzer
{
	internal class SequenceFinderResultSaver
	{
		private CommonFilePath _commonFilePath;
		private string _resultFolderName = "SequenceFinder";
		private string _format = ".txt";

		public SequenceFinderResultSaver(CommonFilePath commonFilePath) 
		{ 
			_commonFilePath = commonFilePath;
		}

		public void Save(string countInfo, LinkedList<int> indexes)
		{
			if (countInfo == "")
				return;

			string fileName = "sequence-finder-" + DateTime.Now.Date.ToString("yyyy-MM-dd") + _format;
			string destination = Path.Combine(_commonFilePath.FilePath, _resultFolderName);

			if (_commonFilePath.IsRootFileDestinationChosen == false)
				return;

			NoExtentionFileSaver fileSaver = new NoExtentionFileSaver();
			fileSaver.SaveTo(destination, fileName, BuildWholeString(countInfo, indexes));
		}

		public void SaveIndividually(string countInfo, LinkedList<int> indexes)
		{
			if (countInfo == "")
				return;

			NoExtentionFileSaver fileSaver = new NoExtentionFileSaver();
			fileSaver.Save(BuildWholeString(countInfo, indexes));
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
