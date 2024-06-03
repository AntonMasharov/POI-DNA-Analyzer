using System.IO;

namespace POI_DNA_Analyzer
{
	internal class OpenReadingFrame
	{
		private Codon _codon;
		private int _codonSize = 3;

		public OpenReadingFrame()
		{
			_codon = new Codon();
		}

		public void Start(string text, bool isComplementary, string cultureCode)
		{
			for (int i = 0; i < 3; i++)
			{
				SaveToFile(ReadCodons(text, i, cultureCode), GetFileName(i, isComplementary));
			}
		}

		private string ReadCodons(string text, int indent, string cultureCode)
		{
			if (indent < 0 || indent > 2)
				return "";

			string outputText = "";
			int leftBorder = indent;

			while (leftBorder + _codonSize < text.Length)
			{
				string codon = text.Substring(leftBorder, _codonSize);
				outputText += _codon.GetCorrespondingAminoAcid(codon, cultureCode);
				leftBorder += _codonSize;
			}

			return outputText;
		}

		private void ReadAminoAcids()
		{

		}

		private void SaveToFile(string textToSave, string fileName)
		{
			if (textToSave == "" || fileName == "")
				return;

			string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

			File.WriteAllText(filePath, textToSave);
		}

		private string GetFileName(int indent, bool isComplementary)
		{
			if (isComplementary == true)
				return "standard-animoacids-indent" + indent.ToString() + ".txt";
			else
				return "complementary-animoacids-indent" + indent.ToString() + ".txt";
		}
	}
}
