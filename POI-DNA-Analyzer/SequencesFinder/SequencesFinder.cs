namespace POI_DNA_Analyzer
{
	internal class SequencesFinder : ISequencesFinder
	{
		private LinkedList<int> _occurencesIndexes = new LinkedList<int>();

		public IEnumerable<int> OccurencesIndexes => _occurencesIndexes;

		public int GetOccurrencesCount(string source, string sequenceToFind)
		{
			if (sequenceToFind == null || sequenceToFind == "")
				return 0;

			_occurencesIndexes.Clear();
			int count = 0;
			int index = 0;

			while ((index = source.IndexOf(sequenceToFind, index, StringComparison.OrdinalIgnoreCase)) != -1)
			{
				_occurencesIndexes.AddLast(index);

				index += sequenceToFind.Length;
				count++;
			}

			return count;
		}
	}
}
