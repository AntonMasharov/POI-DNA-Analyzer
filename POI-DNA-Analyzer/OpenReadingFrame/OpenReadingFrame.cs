namespace POI_DNA_Analyzer
{
	internal class OpenReadingFrame
	{
		private AminoAcid _aminoAcid;
		private StartAminoAcidFile _startAminoAcidFile;
		private StartAminoAcidTableFileReader _startAminoAcidTableFileReader;
		private ResultAminoAcidsFileReader _resultFilesReader;

		public OpenReadingFrame()
		{
			_aminoAcid = new AminoAcid();
			_startAminoAcidFile = new StartAminoAcidFile();
			_startAminoAcidTableFileReader = new StartAminoAcidTableFileReader(_aminoAcid, _startAminoAcidFile);
			_startAminoAcidTableFileReader.Read();
			_resultFilesReader = new ResultAminoAcidsFileReader();

			OpenReadingFrameSequences = new Dictionary<int, string>();
		}

		public Dictionary<int, string> OpenReadingFrameSequences { get; private set; } //<index, sequence>

		//СТОП КОДОНЫ ДОЛЖНЫ БЫТЬ В ПОСЛЕДОВАТЕЛЬНОСТИ
		public void HandleSequence(string sequence)
		{
			OpenReadingFrameSequences.Clear();
			string[] aminoAcids = sequence.Split(',');

			for (int i = 0; i < aminoAcids.Length; i++)
			{
				if (_aminoAcid.IsStart(aminoAcids[i]) == false)
					continue;

				int startIndex = i;
				bool isStopEncoutered = false;
				OpenReadingFrameSequences.Add(startIndex, "");

				for (int j = i; j < aminoAcids.Length; j++)
				{
					i = j;

					if (_aminoAcid.IsStop(aminoAcids[j]) == false && isStopEncoutered == true)
						break;

					if (_aminoAcid.IsStop(aminoAcids[j]) == true)
						isStopEncoutered = true;

					OpenReadingFrameSequences[startIndex] += aminoAcids[j];
				}
			}
		}

		public void ChangeConfig()
		{
			FilePicker filePicker = new FilePicker();
			_startAminoAcidFile.SetNewPath(filePicker.PickFilePath(filePicker.FilterCSV));
		}

		public void ResetConfig()
		{
			_startAminoAcidFile.ResetPath();
		}
	}
}
