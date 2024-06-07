using System.IO;
using System.Windows;

namespace POI_DNA_Analyzer
{
	internal class OpenReadingFrameWindow
	{
		private ComplementaryDNACreator _complementaryDNACreator;
		private ComplementaryDNASaver _complementaryDNASaver;

		private Codon _codon;
		private CodonToAminoAcidTranslator _codonToAminoAcidTranslator;
		private TranslatedFileSaver _translatedFileSaver;
		private DNACodonTableFile _codonTableFile;
		private DNACodonTableFileReader _codonTableFileReader;

		private OpenReadingFrame _openReadingFrame;
		private OpenReadingFramesFileSaver _openReadingFrameFileSaver;

		private int _defaultMinSizeToSave = 100;
		private string _complementaryDNA = "";
		private Languages _language = Languages.English;
		private CommonFilePath _commonFilePath;

		public OpenReadingFrameWindow(CommonFilePath commonFilePath, TranslatedFileSaver translatedFileSaver)
		{
			_complementaryDNACreator = new ComplementaryDNACreator();

			_codon = new Codon();
			_translatedFileSaver = translatedFileSaver;
			_codonTableFile = new DNACodonTableFile();
			_codonTableFileReader = new DNACodonTableFileReader(_codon, _codonTableFile);
			_codonTableFileReader.Read();
			_codonToAminoAcidTranslator = new CodonToAminoAcidTranslator(_codon, _translatedFileSaver, commonFilePath);

			_openReadingFrame = new OpenReadingFrame();
			_openReadingFrameFileSaver = new OpenReadingFramesFileSaver();
			_commonFilePath = commonFilePath;
			_complementaryDNASaver = new ComplementaryDNASaver(_commonFilePath);
		}

		public void StartEverything(string standardSequence, string minSizeToSave)
		{
			_translatedFileSaver.ClearPath();
			_translatedFileSaver.ChangePath(_commonFilePath.FilePath);
			_openReadingFrameFileSaver.ChangePath(_commonFilePath.FilePath);

			GenerateComplementaryDNA(standardSequence);
			TranslateCodonsToFiles(standardSequence);
			StartOpenReadingFrame(minSizeToSave);
		}

		public void GenerateComplementaryDNA(string standardSequence)
		{
			_complementaryDNA = _complementaryDNACreator.Create(standardSequence);
		}

		public void SaveComplementaryDNA()
		{
			_complementaryDNASaver.Save(_complementaryDNA);
		}

		public void SaveComplementaryDNAIndividually()
		{
			_complementaryDNASaver.SaveIndividually(_complementaryDNA);
		}

		public void ResetComplementaryDNA()
		{
			_complementaryDNA = "";
		}

		public void TranslateCodonsToFiles(string standardSequence)
		{
			if (_complementaryDNA == null || _complementaryDNA == "")
			{
				WarningBox1();
				return;
			}

			_translatedFileSaver.ClearLists();
			_codonToAminoAcidTranslator.TranslateToFiles(standardSequence, false, _language);
			_codonToAminoAcidTranslator.TranslateToFiles(_complementaryDNA, true, _language);
		}

		public void ChangeTranslationConfig()
		{
			FilePicker filePicker = new FilePicker();
			_codonTableFile.SetNewPath(filePicker.PickFilePath(filePicker.FilterCSV));
		}

		public void ResetTranslationConfig()
		{
			_codonTableFile.ResetPath();
		}

		public void StartOpenReadingFrame(string minSizeToSave)
		{
			if (_complementaryDNA == null || _complementaryDNA == "")
			{
				WarningBox1();
				return; 
			}
			
			if (_translatedFileSaver.ResultFilesPaths == null || _translatedFileSaver.ResultFilesPaths.Count == 0)
			{
				WarningBox2();
				return;
			}

			FileOpener fileOpener = new FileOpener();

			for (int i = 0; i < _translatedFileSaver.ResultFilesPaths.Count; i++)
			{
				StreamReader file = fileOpener.OpenFile(Path.Combine(_translatedFileSaver.ResultFilesPaths[i], _translatedFileSaver.ResultFilesNames[i]));

				_openReadingFrame.HandleSequence(file.ReadToEnd());

				Dictionary<int, string> result = _openReadingFrame.OpenReadingFrameSequences;
				string name = _translatedFileSaver.ResultFilesNames[i];

				_openReadingFrameFileSaver.Save(name, result, HandleMinSizeToSave(minSizeToSave));
			}
		}

		public void ChangeOpenReadingFramesResultPath()
		{
			_openReadingFrameFileSaver.ChoosePath();
		}

		public void ChangeOpenReadingFramesConfig()
		{
			_openReadingFrame.ChangeConfig();
		}

		public void ResetOpenReadingFramesConfig()
		{
			_openReadingFrame.ResetConfig();
		}

		public void ChangeResultLanguage(Languages language)
		{
			_language = language;
		}

		private int HandleMinSizeToSave(string value)
		{
			int output = 0;

			if (int.TryParse(value, out output) == false)
				output = _defaultMinSizeToSave;

			if (output <= 0)
				output = _defaultMinSizeToSave;

			return output;
		}

		private void WarningBox1()
		{
			MessageBox.Show(
				"Create a complementary DNA first",
				"Complete previous step",
				MessageBoxButton.OKCancel,
				MessageBoxImage.Warning
			);
		}

		private void WarningBox2()
		{
			MessageBox.Show(
				"Make a translation files first",
				"Complete previous step",
				MessageBoxButton.OKCancel,
				MessageBoxImage.Warning
			);
		}
	}
}
