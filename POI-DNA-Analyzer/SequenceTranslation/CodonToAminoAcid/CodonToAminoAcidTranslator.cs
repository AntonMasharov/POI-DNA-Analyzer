namespace POI_DNA_Analyzer
{
	internal class CodonToAminoAcidTranslator
	{
		private TranslatedFileSaver _fileSaver;
		private Codon _codon;
		private int _codonSize = 3;

		public CodonToAminoAcidTranslator(Codon codon, TranslatedFileSaver fileSaver)
		{
			_fileSaver = fileSaver;
			_codon = codon;
		}

		public void TranslateToFiles(string text, bool isComplementary, Languages language)
		{
			//_fileSaver.ResultFilesPaths.Clear();

			for (int i = 0; i < 3; i++)
			{
				SaveToFile(ReadCodons(text, i, language), GetFileName(i, isComplementary));
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

		private void SaveToFile(string textToSave, string fileName)
		{
			if (textToSave == "" || fileName == "")
				return;

			_fileSaver.Save(textToSave, fileName);
		}

		private string GetFileName(int indent, bool isComplementary)
		{
			string fileName = "";

			if (isComplementary == true)
				fileName = "standard-animoacids-indent" + indent.ToString() + ".txt";
			else
				fileName = "complementary-animoacids-indent" + indent.ToString() + ".txt";

			return fileName;
		}
	}
}
