namespace POI_DNA_Analyzer
{
	internal class RestrictionSitesTableFile : ConfigFile
	{
		public RestrictionSitesTableFile() : base()
		{

		}

		protected override string DefaultFileName { get; set; } = "restriction-sites.csv";
	}
}
