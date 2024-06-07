using System.Windows;
using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	internal class SaveContextMenu
	{
		private CheckBox _saveIndividuallyCheckBox;
		private CheckBox _saveTogetherCheckBox;

		public SaveContextMenu(CheckBox saveIndividuallyCheckBox, CheckBox saveTogetherCheckBox)
		{
			_saveIndividuallyCheckBox = saveIndividuallyCheckBox;
			_saveTogetherCheckBox = saveTogetherCheckBox;

			SubscribeToEvents();
		}

		private void SubscribeToEvents()
		{
			_saveIndividuallyCheckBox.Checked += UncheckSaveTogetherCheckBox;
			_saveTogetherCheckBox.Checked += UncheckSaveIndividuallyCheckBox;
		}

		private void UncheckSaveTogetherCheckBox(object sender, RoutedEventArgs e)
		{
			_saveTogetherCheckBox.IsChecked = false;
		}

		private void UncheckSaveIndividuallyCheckBox(object sender, RoutedEventArgs e)
		{
			_saveIndividuallyCheckBox.IsChecked = false;
		}
	}
}
