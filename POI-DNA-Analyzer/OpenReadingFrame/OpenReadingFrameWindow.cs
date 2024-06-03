using static System.Net.Mime.MediaTypeNames;

namespace POI_DNA_Analyzer
{
	internal class OpenReadingFrameWindow
	{
		private StartAminoAcidFile _startAminoAcidFile;
		private DNACodonTableFile _DNAcodonTableFile;
		private StartAminoAcidTableFileReader _startAminoAcidTableFileReader;
		private DNACodonTableFileReader _DNAcodonTableFileReader;
		private ComplementaryDNACreator _complementaryDNACreator;
		private OpenReadingFrame _openReadingFrame;

		public OpenReadingFrameWindow()
		{
			_startAminoAcidFile = new StartAminoAcidFile();
			_DNAcodonTableFile = new DNACodonTableFile();
			_startAminoAcidTableFileReader = new StartAminoAcidTableFileReader(_startAminoAcidFile);
			_DNAcodonTableFileReader = new DNACodonTableFileReader(_DNAcodonTableFile);
			_complementaryDNACreator = new ComplementaryDNACreator();
			_openReadingFrame = new OpenReadingFrame();

			_startAminoAcidTableFileReader.ReadTable();
			_DNAcodonTableFileReader.ReadTable();
		}

		public void Start(string text, string cultureCode)
		{
			if (text == null || text == "")
				return;

			ReadStandardSequence(text, cultureCode);
			ReadComplementarySequence(text, cultureCode);
		}

		public void Save()
		{

		}

		private void ReadStandardSequence(string text, string cultureCode)
		{
			_openReadingFrame.Start(text, false, cultureCode);
		}

		private void ReadComplementarySequence(string text, string cultureCode)
		{
			string complementarySequence = _complementaryDNACreator.Create(text);
			_openReadingFrame.Start(complementarySequence, true, cultureCode);
		}
	}
}
