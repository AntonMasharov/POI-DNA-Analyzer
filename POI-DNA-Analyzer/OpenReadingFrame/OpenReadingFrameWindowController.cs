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

		public void StartEverything(string standardSequence, string minSizeToSave)
		{
			if (standardSequence == null || standardSequence == "")
				return;

			_translatedFileSaver.ClearPath();
			_translatedFileSaver.ChangePath(_commonFilePath.FilePath);
			_openReadingFrameFileSaver.ChangePath(_commonFilePath.FilePath);

			GenerateComplementaryDNA(standardSequence);
			TranslateCodonsToFiles(standardSequence);
			StartOpenReadingFrame(minSizeToSave);
		}

		public void GenerateComplementaryDNA(string standardSequence)
		{
			if (standardSequence == null || standardSequence == "")
				return;

			_complementaryDNA.Create(standardSequence);
		}

		public void SaveComplementaryDNA()
		{
			_complementaryDNASaver.Save();
		}

		public void SaveComplementaryDNAIndividually()
		{
			_complementaryDNASaver.SaveIndividually();
		}

		public void ResetComplementaryDNA()
		{
			_complementaryDNA.Reset();
		}

		public void TranslateCodonsToFiles(string standardSequence)
		{
			if (_complementaryDNA.Get() == "")
			{
				WarningBoxStep1();
				return;
			}

			_translatedFileSaver.ClearLists();

			_codonToAminoAcidTranslator.Translate(standardSequence, _language);
			_translatedFileSaver.Save(false);

			_codonToAminoAcidTranslator.Translate(_complementaryDNA.Get(), _language);
			_translatedFileSaver.Save(true);
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

		public void ChangeTranslationConfig()
		{
			FilePicker filePicker = new FilePicker();
			_codonTableFile.SetNewPath(filePicker.PickFilePath(filePicker.FilterCSV));
			_codonTableFileReader.Read();
		}

		public void ResetTranslationConfig()
		{
			_codonTableFile.ResetPath();
			_codonTableFileReader.Read();
		}

		public void StartOpenReadingFrame(string minSizeToSave)
		{
			if (_complementaryDNA.Get() == "")
			{
				WarningBoxStep1();
				return; 
			}
			
			if (_translatedFilesPathsList.ResultFilesPaths == null || _translatedFilesPathsList.ResultFilesPaths.Count == 0)
			{
				WarningBoxStep2();
				return;
			}

			FileOpener fileOpener = new FileOpener();

			for (int i = 0; i < _translatedFilesPathsList.ResultFilesPaths.Count; i++)
			{
				string path = Path.Combine(_translatedFilesPathsList.ResultFilesPaths[i], _translatedFilesPathsList.ResultFilesNames[i]);
				StreamReader file = fileOpener.OpenFile(path);

				_openReadingFrame.FindOpenReadingFrames(file.ReadToEnd());
				SaveOpenReadingFramesResult(i, minSizeToSave);
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

		public void ResetLists()
		{
			_translatedFileSaver.ClearLists();
		}

		private void SaveOpenReadingFramesResult(int indent, string minSizeToSave)
		{
			IReadOnlyDictionary<int, string> result = _openReadingFrame.OpenReadingFrameSequences;
			string name = _translatedFilesPathsList.ResultFilesNames[indent];

			_openReadingFrameFileSaver.Save(name, result, HandleMinSizeToSave(minSizeToSave));
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

		private void WarningBoxStep1()
		{
			MessageBox.Show(
				"Create a complementary DNA first",
				"Complete previous step",
				MessageBoxButton.OKCancel,
				MessageBoxImage.Warning
			);
		}

		private void WarningBoxStep2()
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
