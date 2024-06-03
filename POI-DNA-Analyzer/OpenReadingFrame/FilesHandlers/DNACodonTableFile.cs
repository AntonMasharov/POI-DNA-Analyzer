namespace POI_DNA_Analyzer
{
	internal class DNACodonTableFile : GencodeFile
	{
		public DNACodonTableFile() : base()
		{

		}

		protected override string DefaultFileName { get; set; } = "codons.csv";
	}
}
