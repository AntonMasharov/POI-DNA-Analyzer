using System.IO;
using System.Text;

namespace POI_DNA_Analyzer
{
	internal class SequenceFinderResultSaver : ResultSaver
	{
		private SequenceFinderOutput _sequenceFinderOutput;
		private CommonFilePath _commonFilePath;
		private string _resultFolderName = "SequenceFinder";
		private string _format = ".txt";

		public SequenceFinderResultSaver(SequenceFinderOutput sequenceFinder, CommonFilePath commonFilePath) : base(commonFilePath)
		{ 
			_sequenceFinderOutput = sequenceFinder;
			_commonFilePath = commonFilePath;
		}

		public override FileSaver GetFileSaver()
		{
			return new NoExtentionFileSaver();
		}

		public override string GetFileName()
		{
			return "sequence-finder-" + DateTime.Now.Date.ToString("yyyy-MM-dd") + _format;
		}

		public override string GetDestination()
		{
			return Path.Combine(_commonFilePath.FilePath, _resultFolderName);
		}

		public override string GetContent()
		{
			return BuildWholeString();
		}

		public override bool CanSave()
		{
			if (_sequenceFinderOutput.OccurencesIndexes.Count() == 0)
				return false;

			return true;
		}

		private string BuildIndexes(IEnumerable<int> indexes)
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

		private string BuildWholeString()
		{
			return _sequenceFinderOutput.OccurencesIndexes.Count() + "\n\n" + BuildIndexes(_sequenceFinderOutput.OccurencesIndexes);
		}
	}
}
