namespace POI_DNA_Analyzer
{
	internal class CSVFileSaver : FileSaver
	{
		public override string Filter { get; } = "CSV Files (*.csv)|*.csv";

		public override string DefaultExtenion { get; protected set; } = "csv";
	}
}
