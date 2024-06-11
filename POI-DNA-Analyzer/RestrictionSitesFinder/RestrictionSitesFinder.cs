namespace POI_DNA_Analyzer
{
	internal class RestrictionSitesFinder : IRestrictionSitesFinderResult
	{
		private RestrictionSites _restrictionSites;
		private ISequencesFinder _sequencesFinder;
		private List<RestrictionSite> _restrictionSitesList;

		public RestrictionSitesFinder(RestrictionSites restrictionSite)
		{
			_restrictionSites = restrictionSite;
			_sequencesFinder = new SequencesFinder();
			_restrictionSitesList = new List<RestrictionSite>();
		}

		public IEnumerable<RestrictionSite> RestrictionSites => _restrictionSitesList;

		public void Find(string text)
		{
			if (text == null) 
				return;

			_restrictionSitesList.Clear();

			foreach (string key in _restrictionSites.RestrictionSitesDictionary.Keys)
			{
				_sequencesFinder.GetOccurrencesCount(text, key);

				if (_sequencesFinder.OccurencesIndexes.Count() == 0)
					continue;

				List<int> indexes = new List<int>(_sequencesFinder.OccurencesIndexes);
				RestrictionSite restrictionSite = new RestrictionSite(_restrictionSites.RestrictionSitesDictionary[key], indexes);
				_restrictionSitesList.Add(restrictionSite);
			}
		}
	}
}
