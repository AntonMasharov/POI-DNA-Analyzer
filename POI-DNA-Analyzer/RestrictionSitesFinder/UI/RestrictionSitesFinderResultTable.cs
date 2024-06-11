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
			//_dataGrid.Columns[1].Width = _dataGrid.Columns[1].ActualWidth;

			//foreach (DataGridColumn column in _dataGrid.Columns)
			//{
			//	if (column.Header.ToString() == "Indexes")
			//	{
			//		column.Width = new DataGridLength(300);
			//		break;
			//	}
			//}
		}

		public void Clear()
		{
			_dataGrid.ItemsSource = null;
		}
	}
}
