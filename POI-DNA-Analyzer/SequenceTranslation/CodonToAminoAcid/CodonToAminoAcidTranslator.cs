namespace POI_DNA_Analyzer
{
	internal class CodonToAminoAcidTranslator
	{
		private Codon _codon;
		private int _codonSize = 3;
		private Dictionary<int, string> _translatedSequences;

		public CodonToAminoAcidTranslator(Codon codon)
		{
			_codon = codon;
			_translatedSequences = new Dictionary<int, string>();
		}

		public IReadOnlyDictionary<int, string> TranslatedSequences => _translatedSequences;

		public void Translate(string text, Languages language)
		{
			_translatedSequences.Clear();

			for (int indent = 0; indent < _codonSize; indent++)
			{
				_translatedSequences.Add(indent, ReadCodons(text, indent, language));
			}
		}

		private string ReadCodons(string text, int indent, Languages language)
		{
			if (indent < 0 || indent > 2)
				return "";

			string outputText = "";
			int leftBorder = indent;

			while (leftBorder + _codonSize < text.Length)
			{
				string codon = text.Substring(leftBorder, _codonSize);
				outputText += _codon.GetCorrespondingAminoAcid(codon, language) + ",";
				leftBorder += _codonSize;
			}

			return outputText;
		}
	}
}
