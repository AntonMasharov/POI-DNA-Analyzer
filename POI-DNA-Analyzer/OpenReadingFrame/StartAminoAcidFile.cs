namespace POI_DNA_Analyzer
{
	internal class StartAminoAcidFile : ConfigFile
	{
		public StartAminoAcidFile() : base() 
		{ 
		
		}

		protected override string DefaultFileName { get; set; } = "start-amino-acid.csv";
	}
}
