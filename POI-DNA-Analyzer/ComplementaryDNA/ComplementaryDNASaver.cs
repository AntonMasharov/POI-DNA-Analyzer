namespace POI_DNA_Analyzer
{
	class ComplementaryDNASaver : ResultSaver
    {
		private ComplementaryDNA _complementaryDNA;
		private CommonFilePath _commonFilePath;
		private string _format = ".txt";

		public ComplementaryDNASaver(ComplementaryDNA complementaryDNA, CommonFilePath commonFilePath):base(commonFilePath)
		{
			_complementaryDNA = complementaryDNA;
			_commonFilePath = commonFilePath;
		}

		public override FileSaver GetFileSaver()
		{
			return new NoExtentionFileSaver();
		}

		public override string GetFileName()
		{
			return "complementary-sequence" + _format;
		}

		public override string GetDestination()
		{
			return _commonFilePath.FilePath;
		}

		public override string GetContent()
		{
			return _complementaryDNA.Get();
		}

		public override bool CanSave()
		{
			if (_complementaryDNA.Get() == "")
				return false;
			else
				return true;
		}
    }
}
