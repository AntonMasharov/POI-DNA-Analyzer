using System.IO;

namespace POI_DNA_Analyzer
{
	internal class TranslatedFileSaver
	{
		private CodonToAminoAcidTranslator _codonToAminoAcidTranslator;
		private TranslatedFilesPathsList _translatedFilesList;
		private CommonFilePath _commonFilePath;
		private FileSaver _fileSaver;

		private string _resultFolderName = "TranslationResult";
		private string _saveDestination = "";
		private string _format = ".txt";

		public TranslatedFileSaver(CodonToAminoAcidTranslator codonToAminoAcidTranslator, TranslatedFilesPathsList translatedFilesList, CommonFilePath commonFilePath)
		{
			_codonToAminoAcidTranslator = codonToAminoAcidTranslator;
			_translatedFilesList = translatedFilesList;
			_commonFilePath = commonFilePath;
			_fileSaver = GetFileSaver();
		}

		public void Save(bool isComplementary)
		{
			if (CanSave() == false)
				return;

			foreach (int key in _codonToAminoAcidTranslator.TranslatedSequences.Keys)
			{
				string fileName = GetFileName(key, isComplementary);
				string destination = GetDestination();

				_fileSaver.SaveTo(destination, fileName, _codonToAminoAcidTranslator.TranslatedSequences[key]);

				_translatedFilesList.ResultFilesPathsWithoutName.Add(_saveDestination);
				_translatedFilesList.ResultFilesPaths.Add(destination);
				_translatedFilesList.ResultFilesNames.Add(fileName);
			}
		}

		public void ChangePath(string filePathWithoutName)
		{
			if (filePathWithoutName == "" || filePathWithoutName == null)
				return;

			_saveDestination = filePathWithoutName;
		}

		public void ClearLists()
		{
			_translatedFilesList.ResultFilesPaths.Clear();
			_translatedFilesList.ResultFilesNames.Clear();
			_translatedFilesList.ResultFilesPathsWithoutName.Clear();
		}

		public void ClearPath()
		{
			_saveDestination = "";
		}

		private FileSaver GetFileSaver()
		{
			return new NoExtentionFileSaver();
		}

		private string GetFileName(int indent, bool isComplementary)
		{
			string fileName = "";

			if (isComplementary == true)
				fileName = "standard-animoacids-indent" + indent.ToString() + _format;
			else
				fileName = "complementary-animoacids-indent" + indent.ToString() + _format;

			return fileName;
		}

		private string GetDestination()
		{
			string destination;

			if (_saveDestination == "")
				destination = Path.Combine(_commonFilePath.FilePath, _resultFolderName);
			else
				destination = Path.Combine(_saveDestination, _resultFolderName);

			return destination;
		}

		private bool CanSave()
		{
			if (_codonToAminoAcidTranslator.TranslatedSequences == null
				|| _codonToAminoAcidTranslator.TranslatedSequences.Count == 0)
				return false;

			return true;
		}
	}
}
