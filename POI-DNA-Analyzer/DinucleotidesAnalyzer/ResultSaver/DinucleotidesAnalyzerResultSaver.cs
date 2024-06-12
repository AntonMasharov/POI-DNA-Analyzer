using System.IO;
using System.Text;

namespace POI_DNA_Analyzer
{
	internal class DinucleotidesAnalyzerResultSaver : ResultSaver
	{
		private DinucleotidesAnalyzer _dinucleotidesAnalyzer;
		private CommonFilePath _commonFilePath;
		private string _resultFolderName = "DinucleotidesAnalyzer";
		private string _format = ".csv";

		public DinucleotidesAnalyzerResultSaver(DinucleotidesAnalyzer dinucleotidesAnalyzer, CommonFilePath commonFilePath):base(commonFilePath)
		{
			_dinucleotidesAnalyzer = dinucleotidesAnalyzer;
			_commonFilePath = commonFilePath;
		}

		public override FileSaver GetFileSaver()
		{
			return new CSVFileSaver();
		}

		public override string GetFileName()
		{
			return "dinucleotides-analyzer-" + DateTime.Now.Date.ToString("yyyy-MM-dd") + _format;
		}

		public override string GetDestination()
		{
			return Path.Combine(_commonFilePath.FilePath, _resultFolderName);
		}

		public override string GetContent()
		{
			return CreateFileText();
		}

		public override bool CanSave()
		{
			if (_dinucleotidesAnalyzer.DinucleotidesProbabilities == null || IsDictionaryEmpty())
				return false;
			else
				return true;
		}

		private string CreateFileText()
		{
			List<int> indexes = new List<int>(_dinucleotidesAnalyzer.Indexes);
			StringBuilder sb = new StringBuilder();

			sb.AppendLine(CreateHeader());

			for (int i = 0; i < indexes.Count; i++)
			{
				string row = indexes[i].ToString();

				foreach (string key in _dinucleotidesAnalyzer.DinucleotidesProbabilities.Keys)
				{
					row += "," + _dinucleotidesAnalyzer.DinucleotidesProbabilities[key][i].ToString("0.00");
				}

				sb.AppendLine(row);
			}

			string result = sb.ToString();

			return result;
		}

		private string CreateHeader()
		{
			string header = "Chunk";

			foreach (string key in _dinucleotidesAnalyzer.DinucleotidesProbabilities.Keys.ToList())
			{
				header += "," + key;
			}

			return header;
		}

		private bool IsDictionaryEmpty()
		{
			bool isEmpty = true;

			foreach (string key in _dinucleotidesAnalyzer.DinucleotidesProbabilities.Keys.ToList())
			{
				isEmpty = _dinucleotidesAnalyzer.DinucleotidesProbabilities[key].Count == 0;

				if (isEmpty == false)
					return isEmpty;
			}

			return isEmpty;
		}
	}
}
