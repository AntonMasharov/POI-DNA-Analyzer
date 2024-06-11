namespace POI_DNA_Analyzer
{
	internal class RestrictionSites
	{
		private Dictionary<string, string> _restrictionSites;

		public RestrictionSites()
		{
			_restrictionSites = new Dictionary<string, string>();
		}

		public int MinSequenceLength { get; private set; }

		public int MaxSequenceLength { get; private set; }

		public IReadOnlyDictionary<string, string> RestrictionSitesDictionary => _restrictionSites;

		public bool IsRestrictionSite(string sequence)
		{
			if (_restrictionSites.ContainsKey(sequence) == true)
				return true;
			else
				return false;
		}

		public string GetCorrespondingRestrictionSite(string sequence)
		{
			if (IsRestrictionSite(sequence) == false)
				throw new Exception("Not a restriction site");

            return _restrictionSites[sequence];
		}

		public void Initialize(string[] contentLines, Dictionary<string, int> header)
		{
			_restrictionSites.Clear();
			MinSequenceLength = int.MaxValue;
			MaxSequenceLength = 0;

			foreach (string line in contentLines)
			{
				string[] parts = line.Split(',');

				if (parts.Length >= 2)
				{
					string key = parts[header["Sequence"]];
					string value = parts[header["Restriction site"]];

					UpdateBoundaries(key.Length);

                    _restrictionSites.Add(key, value);
				}
			}
		}

		private void UpdateBoundaries(int length)
		{
			if (length > MaxSequenceLength)
				MaxSequenceLength = length;
			else if (length < MinSequenceLength)
				MinSequenceLength = length;
		}
	}
}
