using System.IO;

namespace POI_DNA_Analyzer
{
	internal class ResultAminoAcidsFileReader
	{
		public string GetString(string fileName)
		{
			string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
			StreamReader streamReader = new StreamReader(filePath);

			if (streamReader == null)
				return "";

			return streamReader.ReadToEnd();
		}
	}
}
