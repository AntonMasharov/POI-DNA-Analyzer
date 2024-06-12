namespace POI_DNA_Analyzer
{
	internal class FeedbackFile : ConfigFile
	{
		public FeedbackFile() : base()
		{

		}

		protected override string DefaultFileName { get; set; } = "feedback.csv";
	}
}
