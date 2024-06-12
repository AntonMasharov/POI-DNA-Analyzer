using System.Text;

namespace POI_DNA_Analyzer
{
	class ATGCResultSaver : ResultSaver
    {
		private IATGCCounterResult _result;
		private CommonFilePath _commonFilePath;
		private string _format = ".csv";

		public ATGCResultSaver(IATGCCounterResult result, CommonFilePath commonFilePath) : base(commonFilePath)
		{
			_result = result;
			_commonFilePath = commonFilePath;
		}

		public override FileSaver GetFileSaver()
		{
			return new CSVFileSaver();
		}

		public override string GetFileName()
		{
			return "at-gc-percents" + _format;
		}

		public override string GetDestination()
		{
			return _commonFilePath.FilePath;
		}

		public override string GetContent()
		{
			return CreateFileText();
		}

		public override bool CanSave()
		{
			if (_result.Percents == null || IsDictionaryEmpty())
				return false;
			else
				return true;
		}

		private string CreateFileText()
		{
			List<int> indexes = new List<int>(_result.Indexes);
			StringBuilder sb = new StringBuilder();

			sb.AppendLine(CreateHeader());

			for (int i = 0; i < indexes.Count; i++)
			{
				string row = indexes[i].ToString();

				foreach (string key in _result.Percents.Keys)
				{
					row += "," + _result.Percents[key][i].ToString("0.00");
				}

				sb.AppendLine(row);
			}

			string result = sb.ToString();

			return result;
		}

		private string CreateHeader()
		{
			string header = "Chunk";

			foreach (string key in _result.Percents.Keys.ToList())
			{
				header += "," + key;
			}

			return header;
		}

		private bool IsDictionaryEmpty()
		{
			bool isEmpty = true;

			foreach (string key in _result.Percents.Keys.ToList())
			{
				isEmpty = _result.Percents[key].Count == 0;

				if (isEmpty == false)
					return isEmpty;
			}

			return isEmpty;
		}
	}
}
