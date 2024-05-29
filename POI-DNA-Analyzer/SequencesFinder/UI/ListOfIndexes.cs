using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	class ListOfIndexes
	{
		private ListBox _listBox;

		public ListOfIndexes(ListBox listBox)
		{
			_listBox = listBox;
		}

		public void ShowOccurrencesIndexes(LinkedList<int> indexes)
		{
			foreach (int index in indexes)
			{
				_listBox.Items.Add(index);
			}
		}

		public void Clear()
		{
			_listBox.Items.Clear();
		}
	}
}
