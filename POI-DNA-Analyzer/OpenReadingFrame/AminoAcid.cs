namespace POI_DNA_Analyzer
{
	internal class AminoAcid
	{
		private Dictionary<string, bool> _startAminoAcids;

		public AminoAcid() 
		{
			_startAminoAcids = new Dictionary<string, bool>();
		}

		public bool IsStart(string aminoAcid)
		{
			if (_startAminoAcids.ContainsKey(aminoAcid) == false)
				return false;

			if (_startAminoAcids[aminoAcid] == false)
				return false;

			return true;
		}

		public bool IsStop(string aminoAcid)
		{
			if (aminoAcid == "Start" || aminoAcid == "Старт")
				return true;

			return false;
		}

		public void Initialize(string[] contentLines, Dictionary<string, int> header)
		{
			foreach (string line in contentLines)
			{
				string[] parts = line.Split(',');

				if (parts.Length >= 2)
				{
					string EN = parts[header["Start amino-acid EN"]];
					string RU = parts[header["Start amino-acid RU"]];

					_startAminoAcids.Add(EN, true);
					_startAminoAcids.Add(RU, true);
				}
			}
		}
	}
}
