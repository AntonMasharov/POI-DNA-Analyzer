using System.IO;

namespace POI_DNA_Analyzer
{
	internal class RestrictionSiteResultSaver : ResultSaver
	{
		private IRestrictionSitesFinderResult _result;
		private CommonFilePath _commonFilePath;
		private string _resultFolderName = "RestrictionSitesFinder";
		private string _format = ".csv";

		public RestrictionSiteResultSaver(IRestrictionSitesFinderResult result, CommonFilePath commonFilePath) : base(commonFilePath)
		{
			_result = result;
			_commonFilePath = commonFilePath;
		}

		public override FileSaver GetFileSaver()
		{
			return new CSVFileSaver();
		}

		public override string GetFileName()
		{
			return "restriction-sites-" + DateTime.Now.Date.ToString("yyyy-MM-dd") + _format;
		}

		public override string GetDestination()
		{
			return Path.Combine(_commonFilePath.FilePath, _resultFolderName);
		}

		public override string GetContent()
		{
			return CreateFileText();
		}

		public override bool CanSave()
		{
			if (_result.RestrictionSites.Count() == 0)
				return false;
			else
				return true;
		}

		private string CreateFileText()
		{
			string text = "";

			foreach (RestrictionSite restrictionSite in _result.RestrictionSites)
			{
				text += $"{restrictionSite.Name},{restrictionSite.IndexesString}\n";
			}

			return text;
		}
	}
}
