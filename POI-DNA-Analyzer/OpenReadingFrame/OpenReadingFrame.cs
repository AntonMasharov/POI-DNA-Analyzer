using System.IO;

namespace POI_DNA_Analyzer
{
	internal class OpenReadingFrame
	{
		private ResultAminoAcidsFileReader _resultAminoAcidsFileReader;
		private Codon _codon;
		private AminoAcid _aminoAcid;
		private int _codonSize = 3;
		private List<string> _resultFilesNames = new List<string>();

		public OpenReadingFrame()
		{
			_resultAminoAcidsFileReader = new ResultAminoAcidsFileReader();
			_codon = new Codon();
		}

		public void Start(string text, bool isComplementary, string cultureCode)
		{
			_resultFilesNames.Clear();

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
				outputText += _codon.GetCorrespondingAminoAcid(codon, cultureCode) + ",";
				leftBorder += _codonSize;
			}

			return outputText;
		}

		private void ReadAminoAcids()
		{
			foreach (string name in _resultFilesNames)
			{
				Algorithm(_resultAminoAcidsFileReader.GetString(name));
			}
		}

		private void Algorithm(string text)
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
			string fileName = "";

			if (isComplementary == true)
				fileName = "standard-animoacids-indent" + indent.ToString() + ".txt";
			else
				fileName = "complementary-animoacids-indent" + indent.ToString() + ".txt";

			_resultFilesNames.Add(fileName);
			return fileName;
		}
	}
}
