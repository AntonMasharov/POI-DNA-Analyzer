using static System.Net.Mime.MediaTypeNames;

namespace POI_DNA_Analyzer
{
	class PalindromicSequencesFinderResultSaver
	{
		private CommonFilePath _commonFilePath;
		private string _format = ".txt";

		public PalindromicSequencesFinderResultSaver(CommonFilePath commonFilePath)
		{
			_commonFilePath = commonFilePath;
		}

		public void Save(Dictionary<int, string> palindromicSequences)
		{
			string fileName = "palindromic-sequences" + _format;
			string destination = _commonFilePath.FilePath;

			if (_commonFilePath.IsRootFileDestinationChosen == false)
				return;

			NoExtentionFileSaver fileSaver = new NoExtentionFileSaver();
			fileSaver.SaveTo(destination, fileName, BuildString(palindromicSequences));
		}

		public void SaveIndividually(Dictionary<int, string> palindromicSequences)
		{
			NoExtentionFileSaver fileSaver = new NoExtentionFileSaver();
			fileSaver.Save(BuildString(palindromicSequences));
		}

		private string BuildString(Dictionary<int, string> palindromicSequences)
		{
			string result = "";

			foreach (int key in palindromicSequences.Keys)
			{
				result += $"{key}: {palindromicSequences[key]}\n";
			}

			return result;
		}
	}
}
