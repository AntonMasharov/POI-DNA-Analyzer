using System.IO;

namespace POI_DNA_Analyzer
{
	internal class OpenReadingFrameWindow
	{
		private ComplementaryDNACreator _complementaryDNACreator;
		private string _result = "";

		public OpenReadingFrameWindow()
		{
			_complementaryDNACreator = new ComplementaryDNACreator();
		}

		public void Start(StreamReader fileStream)
		{
			if (fileStream == null)
				return;

			_result = _complementaryDNACreator.Create(fileStream);
		}

		public void Save()
		{
			ComplementaryDNAFileSaver complementaryDNAFileSaver = new ComplementaryDNAFileSaver();

			complementaryDNAFileSaver.Save(_result);
		}
	}
}
