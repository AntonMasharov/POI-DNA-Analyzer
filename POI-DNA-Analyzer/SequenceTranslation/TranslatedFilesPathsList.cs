namespace POI_DNA_Analyzer
{
	internal struct TranslatedFilesPathsList
	{
		public TranslatedFilesPathsList()
		{
			ResultFilesPaths = new List<string>();
			ResultFilesPathsWithoutName = new List<string>();
			ResultFilesNames = new List<string>();
		}

		public List<string> ResultFilesPaths { get; private set; }

		public List<string> ResultFilesPathsWithoutName { get; private set; }

		public List<string> ResultFilesNames { get; private set; }
	}
}
