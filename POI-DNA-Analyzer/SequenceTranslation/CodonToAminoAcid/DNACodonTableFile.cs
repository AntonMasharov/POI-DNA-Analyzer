namespace POI_DNA_Analyzer
{
	internal class DNACodonTableFile : TranslationFile
	{
		public DNACodonTableFile() : base()
		{

		}

		protected override string DefaultFileName { get; set; } = "codons.csv";
	}
}
