namespace POI_DNA_Analyzer
{
	internal class Codon
	{
		private Dictionary<string, string> _codonsRU;
		private Dictionary<string, string> _codonsEN;

		public Codon()
		{
			_codonsRU = new Dictionary<string, string>();
			_codonsEN = new Dictionary<string, string>();
		}

		public string GetCorrespondingAminoAcid(string nucleotides, string cultureCode)
		{
			if (cultureCode == "ru-RU")
			{
				return _codonsRU[nucleotides];
			}
            else
            {
				return _codonsEN[nucleotides];
			}
		}

		public void InitializeDictionaries(string[] contentLines, Dictionary<string, int> header)
		{
			foreach (string line in contentLines)
			{
				string[] parts = line.Split(',');

				if (parts.Length >= 3)
				{
					string key = parts[header["Codon"]];
					string valueEN = parts[header["EN amino acid"]];
					string valueRU = parts[header["RU amino acid"]];

					_codonsEN.Add(key, valueEN);
					_codonsRU.Add(key, valueRU);
				}
			}
		}
	}
}
