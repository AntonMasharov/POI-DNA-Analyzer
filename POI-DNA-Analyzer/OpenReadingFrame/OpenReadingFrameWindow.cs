using System.IO;

namespace POI_DNA_Analyzer
{
	internal class OpenReadingFrameWindow
	{
		private ComplementaryDNACreator _complementaryDNACreator;

		private Codon _codon;
		private CodonToAminoAcidTranslator _codonToAminoAcidTranslator;
		private TranslatedFileSaver _translatedFileSaver;
		private DNACodonTableFile _codonTableFile;
		private DNACodonTableFileReader _codonTableFileReader;

		private OpenReadingFrame _openReadingFrame;

		private string _complementaryDNA = "";
		private Languages _language = Languages.English;

		public OpenReadingFrameWindow()
		{
			_complementaryDNACreator = new ComplementaryDNACreator();

			_codon = new Codon();
			_translatedFileSaver = new TranslatedFileSaver();
			_codonTableFile = new DNACodonTableFile();
			_codonTableFileReader = new DNACodonTableFileReader(_codon, _codonTableFile);
			_codonTableFileReader.Read();
			_codonToAminoAcidTranslator = new CodonToAminoAcidTranslator(_codon, _translatedFileSaver);

			_openReadingFrame = new OpenReadingFrame();
		}

		public void StartEverything(string standardSequence)
		{
			GenerateComplementarySequence(standardSequence);
			TranslateCodonsToFiles(standardSequence);
			StartOpenReadingFrame();
		}

		public void GenerateComplementarySequence(string standardSequence)
		{
			_complementaryDNA = _complementaryDNACreator.Create(standardSequence);
		}

		public void TranslateCodonsToFiles(string standardSequence)
		{
			_codonToAminoAcidTranslator.TranslateToFiles(standardSequence, false, _language);
			_codonToAminoAcidTranslator.TranslateToFiles(_complementaryDNA, true, _language);
		}

		public void StartOpenReadingFrame()
		{
			FileOpener fileOpener = new FileOpener();
			OpenReadingFramesFileSaver fileSaver = new OpenReadingFramesFileSaver();

			for (int i = 0; i < _translatedFileSaver.ResultFilesPaths.Count; i++)
			{
				StreamReader file = fileOpener.OpenFile(_translatedFileSaver.ResultFilesPaths[i]);
				_openReadingFrame.HandleSequence(file.ReadToEnd());

				fileSaver.Save(_translatedFileSaver.ResultFilesPathsWithoutName[i], _translatedFileSaver.ResultFilesNames[i], _openReadingFrame.OpenReadingFrameSequences);
			}
		}

		public void ChangeResultLanguage(Languages language)
		{
			_language = language;
		}

		public void ChangeSavePath()
		{

		}
	}
}
