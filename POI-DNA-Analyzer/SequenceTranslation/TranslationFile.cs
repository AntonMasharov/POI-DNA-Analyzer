using System.IO;

namespace POI_DNA_Analyzer
{
	internal abstract class TranslationFile
	{
		private string _filePath = "";

		public TranslationFile()
		{
			SetDefaultPath();
		}

		protected abstract string DefaultFileName { get; set; }

		public string GetString()
		{
			StreamReader streamReader = new StreamReader(_filePath);

			if (streamReader == null)
				return "";

			return streamReader.ReadToEnd();
		}

		public void SetNewPath(string newFilePath)
		{
			_filePath = Path.Combine(newFilePath);
		}

		private void SetDefaultPath()
		{
			_filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DefaultFileName);
		}
	}
}
