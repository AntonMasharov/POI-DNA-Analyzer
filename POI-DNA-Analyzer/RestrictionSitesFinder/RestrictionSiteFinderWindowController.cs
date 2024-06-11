using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	internal class RestrictionSiteFinderWindowController
	{
		private RestrictionSites _restrictionSite;
		private RestrictionSitesTableFile _tableFile;
		private RestrictionSitesTableFileReader _tableFileReader;
		private RestrictionSitesFinder _finder;
		private RestrictionSitesFinderResultTable _finderResultTable;
		private CommonFilePath _commonFilePath;
		private ResultSaver _resultSaver;

		public RestrictionSiteFinderWindowController(DataGrid dataGrid, CommonFilePath commonFilePath)
		{
			_commonFilePath = commonFilePath;
			_restrictionSite = new RestrictionSites();
			_tableFile = new RestrictionSitesTableFile();
			_tableFileReader = new RestrictionSitesTableFileReader(_restrictionSite, _tableFile);
			_tableFileReader.Read();
			_finder = new RestrictionSitesFinder(_restrictionSite);
			_finderResultTable = new RestrictionSitesFinderResultTable(dataGrid);
			_resultSaver = new RestrictionSiteResultSaver(_finder, _commonFilePath);
		}

		public void Start(string text)
		{
			if (text == null || text == "")
				return;

			_finder.Find(text);
			_finderResultTable.Show(_finder.RestrictionSites);
		}

		public void Save()
		{
			_resultSaver.Save();
		}

		public void SaveIndividually()
		{
			_resultSaver.SaveIndividually();
		}

		public void UpdateConfig()
		{
			FilePicker filePicker = new FilePicker();
			_tableFile.SetNewPath(filePicker.PickFilePath(filePicker.FilterCSV));
			_tableFileReader.Read();
		}

		public void ResetConfig()
		{
			_tableFile.ResetPath();
			_tableFileReader.Read();
		}
	}
}
