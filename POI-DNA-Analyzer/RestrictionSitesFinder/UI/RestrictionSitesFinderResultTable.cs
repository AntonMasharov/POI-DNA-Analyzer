using System;
using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	internal class RestrictionSitesFinderResultTable
	{
		private DataGrid _dataGrid;

		public RestrictionSitesFinderResultTable(DataGrid dataGrid)
		{
			_dataGrid = dataGrid;
		}

		public void Show(IEnumerable<RestrictionSite> restrictionSites)
		{
			List<RestrictionSite> list = new List<RestrictionSite>(restrictionSites);

			_dataGrid.ItemsSource = list;
		}

		public void Clear()
		{
			_dataGrid.ItemsSource = null;
		}
	}
}
