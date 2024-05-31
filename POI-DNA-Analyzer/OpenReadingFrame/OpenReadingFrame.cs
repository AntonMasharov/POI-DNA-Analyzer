using System.IO;

namespace POI_DNA_Analyzer.OpenReadingFrame
{
	internal class OpenReadingFrame
	{
		private StreamReader _streamReader;

		public void Start(StreamReader streamReader)
		{
			if (streamReader == null)
				return;

			_streamReader = streamReader;
		}

		private void Read(int indent)
		{
			if (indent < 1 || indent > 3)
				return;
		}

		private void Codon()
		{

		}
	}
}
