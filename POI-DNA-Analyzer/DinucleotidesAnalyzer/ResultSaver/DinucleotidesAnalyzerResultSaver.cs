using System.Text;

namespace POI_DNA_Analyzer
{
	internal class DinucleotidesAnalyzerResultSaver
	{
		private DinucleotidesAnalyzer _dinucleotidesAnalyzer;

		public DinucleotidesAnalyzerResultSaver(DinucleotidesAnalyzer dinucleotidesAnalyzer)
		{
			_dinucleotidesAnalyzer = dinucleotidesAnalyzer;
		}

		public void Save()
		{
			if (_dinucleotidesAnalyzer.DinucleotidesProbabilities == null || IsDictionaryEmpty())
				return;

			CSVFileSaver fileSaver = new CSVFileSaver();
			fileSaver.Save(CreateFile());
		}

		private string CreateFile()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendLine(CreateHeader());

			for (int i = 0; i < _dinucleotidesAnalyzer.Indexes.Count; i++)
			{
				string row = _dinucleotidesAnalyzer.Indexes[i].ToString();

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
