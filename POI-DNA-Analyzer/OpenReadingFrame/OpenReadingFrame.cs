namespace POI_DNA_Analyzer
{
	internal class OpenReadingFrame
	{
		private AminoAcid _aminoAcid;
		private StartAminoAcidFile _startAminoAcidFile;
		private StartAminoAcidTableFileReader _startAminoAcidTableFileReader;
		private ResultAminoAcidsFileReader _resultFilesReader;
		private Dictionary<int, string> _openReadingFrameSequences; //<index, sequence>

		public OpenReadingFrame()
		{
			_aminoAcid = new AminoAcid();
			_startAminoAcidFile = new StartAminoAcidFile();
			_startAminoAcidTableFileReader = new StartAminoAcidTableFileReader(_aminoAcid, _startAminoAcidFile);
			_startAminoAcidTableFileReader.Read();
			_resultFilesReader = new ResultAminoAcidsFileReader();
			_openReadingFrameSequences = new Dictionary<int, string>();
		}

		public IReadOnlyDictionary<int, string> OpenReadingFrameSequences => _openReadingFrameSequences;

		public void FindOpenReadingFrames(string sequence)
		{
			_openReadingFrameSequences.Clear();
			string[] aminoAcids = sequence.Split(',');

			for (int i = 0; i < aminoAcids.Length; i++)
			{
				if (_aminoAcid.IsStart(aminoAcids[i]) == false)
					continue;

				int startIndex = i;
				bool isStopEncoutered = false;
				_openReadingFrameSequences.Add(startIndex, "");

				for (int j = i; j < aminoAcids.Length; j++)
				{
					i = j;

					if (_aminoAcid.IsStop(aminoAcids[j]) == false && isStopEncoutered == true)
						break;

					if (_aminoAcid.IsStop(aminoAcids[j]) == true)
						isStopEncoutered = true;

					_openReadingFrameSequences[startIndex] += aminoAcids[j];
				}
			}
		}

		public bool ChangeConfig()
		{
			FilePicker filePicker = new FilePicker();
			string path = filePicker.PickFilePath(filePicker.FilterCSV);

			if (path == null || path == "")
				return false;

			_startAminoAcidFile.SetNewPath(filePicker.PickFilePath(filePicker.FilterCSV));
			_startAminoAcidTableFileReader.Read();

			return true;
		}

		public void ResetConfig()
		{
			_startAminoAcidFile.ResetPath();
			_startAminoAcidTableFileReader.Read();
		}
	}
}
