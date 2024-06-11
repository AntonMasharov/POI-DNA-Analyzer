namespace POI_DNA_Analyzer
{
	internal interface IRestrictionSitesFinderResult
	{
		IEnumerable<RestrictionSite> RestrictionSites { get; }
	}
}
