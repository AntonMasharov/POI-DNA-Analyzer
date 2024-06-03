using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	class DinucleotidesAnalyzerProgressBar
    {
		private ProgressBar _progressBar;

		public DinucleotidesAnalyzerProgressBar(ProgressBar progressBar) 
		{ 
			_progressBar = progressBar;
			Hide();
		}

		public void Update(int maxValue, int currentValue)
		{
			_progressBar.Maximum = maxValue;
			_progressBar.Value = currentValue;

			UpdateVisibility(maxValue, currentValue);
		}

		public void Show()
		{
			_progressBar.Visibility = System.Windows.Visibility.Visible;
		}

		private void UpdateVisibility(int maxValue, int currentValue)
		{
			if (currentValue == maxValue)
				Hide();
		}

		private void Hide()
		{
			_progressBar.Visibility = System.Windows.Visibility.Collapsed;
		}
    }
}
