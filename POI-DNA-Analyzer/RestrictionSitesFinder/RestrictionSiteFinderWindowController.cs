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

		public string Save()
		{
			_resultSaver.Save();
			return _resultSaver.GetFullPath();
		}

		public string SaveIndividually()
		{
			_resultSaver.SaveIndividually();
			return _resultSaver.GetFullPath();
		}

		public bool UpdateConfig()
		{
			FilePicker filePicker = new FilePicker();
			string path = filePicker.PickFilePath(filePicker.FilterCSV);

			if (path == null || path == "")
				return false;

			_tableFile.SetNewPath(path);
			_tableFileReader.Read();

			return true;
		}

		public void ResetConfig()
		{
			_tableFile.ResetPath();
			_tableFileReader.Read();
		}
	}
}
