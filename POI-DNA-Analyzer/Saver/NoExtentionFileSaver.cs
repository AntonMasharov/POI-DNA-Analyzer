namespace POI_DNA_Analyzer
{
	internal class NoExtentionFileSaver : FileSaver
	{
		public override string Filter { get; } = "";

		public override string DefaultExtenion { get; protected set; } = "";
	}
}
