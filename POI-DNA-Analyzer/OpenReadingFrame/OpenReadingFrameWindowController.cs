using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace POI_DNA_Analyzer
{
	internal class OpenReadingFrameWindowController
	{
		private ComplementaryDNA _complementaryDNA;
		private ComplementaryDNASaver _complementaryDNASaver;

		private Codon _codon;
		private CodonToAminoAcidTranslator _codonToAminoAcidTranslator;
		private TranslatedFileSaver _translatedFileSaver;
		private DNACodonTableFile _codonTableFile;
		private DNACodonTableFileReader _codonTableFileReader;
		private TranslatedFilesPathsList _translatedFilesPathsList;

		private OpenReadingFrame _openReadingFrame;
		private OpenReadingFramesFileSaver _openReadingFrameFileSaver;

		private int _defaultMinSizeToSave = 100;
		private Languages _language = Languages.English;
		private CommonFilePath _commonFilePath;

		public OpenReadingFrameWindowController(CommonFilePath commonFilePath)
		{
			_complementaryDNA = new ComplementaryDNA();

			_codon = new Codon();
			_codonTableFile = new DNACodonTableFile();
			_codonTableFileReader = new DNACodonTableFileReader(_codon, _codonTableFile);
			_codonTableFileReader.Read();
			_translatedFilesPathsList = new TranslatedFilesPathsList();
			_codonToAminoAcidTranslator = new CodonToAminoAcidTranslator(_codon);
			_translatedFileSaver = new TranslatedFileSaver(_codonToAminoAcidTranslator, _translatedFilesPathsList, commonFilePath);

			_openReadingFrame = new OpenReadingFrame();
			_openReadingFrameFileSaver = new OpenReadingFramesFileSaver();
			_commonFilePath = commonFilePath;
			_complementaryDNASaver = new ComplementaryDNASaver(_complementaryDNA, _commonFilePath);
		}

		public bool StartEverything(string standardSequence, int minSizeToSave = 100)
		{
			if (standardSequence == null || standardSequence == "")
				return false;

			_translatedFileSaver.ClearPath();
			_translatedFileSaver.ChangePath(_commonFilePath.FilePath);
			_openReadingFrameFileSaver.ChangePath(_commonFilePath.FilePath);

			GenerateComplementaryDNA(standardSequence);
			TranslateCodonsToFiles(standardSequence);
			StartOpenReadingFrame(minSizeToSave);

			return true;
		}

		public string GenerateComplementaryDNA(string standardSequence)
		{
			if (standardSequence == null || standardSequence == "")
				return "";

			return _complementaryDNA.Create(standardSequence);
		}

		public string SaveComplementaryDNA()
		{
			_complementaryDNASaver.Save();
			return _complementaryDNASaver.GetFullPath();
		}

		public string SaveComplementaryDNAIndividually()
		{
			_complementaryDNASaver.SaveIndividually();
			return _complementaryDNASaver.GetFullPath();
		}

		public void ResetComplementaryDNA()
		{
			_complementaryDNA.Reset();
		}

		public bool TranslateCodonsToFiles(string standardSequence)
		{
			if (_complementaryDNA.Get() == "")
			{
				WarningBoxStep1();
				return false;
			}

			_translatedFileSaver.ClearLists();

			_codonToAminoAcidTranslator.Translate(standardSequence, _language);
			_translatedFileSaver.Save(false);

			_codonToAminoAcidTranslator.Translate(_complementaryDNA.Get(), _language);
			_translatedFileSaver.Save(true);

			return true;
		}

		public void ChangeTranslationResultPath()
		{
			OpenFolderDialog openFolderDialog = new OpenFolderDialog();

			if (openFolderDialog.ShowDialog() == true)
			{
				_translatedFileSaver.ChangePath(openFolderDialog.FolderName);
			}
			else
			{
				return;
			}
		}

		public bool ChangeTranslationConfig()
		{
			FilePicker filePicker = new FilePicker();
			string path = filePicker.PickFilePath(filePicker.FilterCSV);

			if (path == null || path == "")
				return false;

			_codonTableFile.SetNewPath(path);
			_codonTableFileReader.Read();

			return true;
		}

		public void ResetTranslationConfig()
		{
			_codonTableFile.ResetPath();
			_codonTableFileReader.Read();
		}

		public bool StartOpenReadingFrame(int minSizeToSave = 100)
		{
			if (minSizeToSave == 0)
				minSizeToSave = _defaultMinSizeToSave;

			if (_complementaryDNA.Get() == "")
			{
				WarningBoxStep1();
				return false; 
			}
			
			if (_translatedFilesPathsList.ResultFilesPaths == null || _translatedFilesPathsList.ResultFilesPaths.Count == 0)
			{
				WarningBoxStep2();
				return false;
			}

			FileOpener fileOpener = new FileOpener();

			for (int i = 0; i < _translatedFilesPathsList.ResultFilesPaths.Count; i++)
			{
				string path = Path.Combine(_translatedFilesPathsList.ResultFilesPaths[i], _translatedFilesPathsList.ResultFilesNames[i]);
				StreamReader file = fileOpener.OpenFile(path);

				_openReadingFrame.FindOpenReadingFrames(file.ReadToEnd());
				SaveOpenReadingFramesResult(i, minSizeToSave);
			}

			return true;
		}

		public void ChangeOpenReadingFramesResultPath()
		{
			_openReadingFrameFileSaver.ChoosePath();
		}

		public bool ChangeOpenReadingFramesConfig()
		{
			return _openReadingFrame.ChangeConfig();
		}

		public void ResetOpenReadingFramesConfig()
		{
			_openReadingFrame.ResetConfig();
		}

		public void ChangeResultLanguage(Languages language)
		{
			_language = language;
		}

		public void ResetLists()
		{
			_translatedFileSaver.ClearLists();
		}

		private void SaveOpenReadingFramesResult(int indent, int minSizeToSave)
		{
			IReadOnlyDictionary<int, string> result = _openReadingFrame.OpenReadingFrameSequences;
			string name = _translatedFilesPathsList.ResultFilesNames[indent];

			_openReadingFrameFileSaver.Save(name, result, minSizeToSave);
		}

		private void WarningBoxStep1()
		{
			MessageBox.Show(
				"Create a complementary DNA first",
				"Complete previous step",
				MessageBoxButton.OK,
				MessageBoxImage.Warning
			);
		}

		private void WarningBoxStep2()
		{
			MessageBox.Show(
				"Make translation files first",
				"Complete previous step",
				MessageBoxButton.OK,
				MessageBoxImage.Warning
			);
		}
	}
}
