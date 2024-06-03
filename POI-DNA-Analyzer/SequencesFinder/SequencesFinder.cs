namespace POI_DNA_Analyzer
{
	internal class SequencesFinder
	{
		public LinkedList<int> SequenceIndexes { get; private set; } = new LinkedList<int>();
		
		public string Indexes { get; private set; } = "";

		public int GetOccurrencesCount(string source, string sequenceToFind)
		{
			if (sequenceToFind == null || sequenceToFind == "")
				return 0;

			SequenceIndexes.Clear();
			int count = 0;
			int index = 0;

			while ((index = source.IndexOf(sequenceToFind, index, StringComparison.OrdinalIgnoreCase)) != -1)
			{
				SequenceIndexes.AddLast(index);

				index += sequenceToFind.Length;
				count++;
			}

			return count;
		}
	}
}
