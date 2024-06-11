namespace POI_DNA_Analyzer
{
	internal class DNACodonTableFile : ConfigFile
	{
		public DNACodonTableFile() : base()
		{

		}

		protected override string DefaultFileName { get; set; } = "codons.csv";
	}
}
