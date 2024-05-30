using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	public partial class MainWindow : Window
	{
		private SequencesFinderWindow _sequencesFinderWindow;
		private DinucleotidesAnalyzerWindow _dinucleotidesAnalyzerWindow;
		private StreamReader _fileStream;

		private string _filePath = "";

		public MainWindow()
		{
			InitializeComponent();

			_sequencesFinderWindow = new SequencesFinderWindow(ResultText, List);
			_dinucleotidesAnalyzerWindow = new DinucleotidesAnalyzerWindow(OxyPlot, EnableSliderCheckBox, HorizontalScrollBar);
		}

		private void OpenFileButtonClick(object sender, RoutedEventArgs e)
		{
			FilePicker filePicker = new FilePicker();

			_filePath = filePicker.PickFilePath();

			OpenFile();
		}

		private void SaveFileButtonClick(object sender, RoutedEventArgs e)
		{
			_sequencesFinderWindow.Save(ResultText.Text);
		}

		private void EnterPromptButtonClick(object sender, RoutedEventArgs e)
		{
			_sequencesFinderWindow.Find(PromptField.Text, _fileStream);

			OpenFile();
		}

		private void ClearResultButtonClick(object sender, RoutedEventArgs e)
		{
			_sequencesFinderWindow.Clear();
		}

		private void SaveDinucleotidesAnalyzerButtonClick(object sender, RoutedEventArgs e)
		{
			_dinucleotidesAnalyzerWindow.Save();
		}

		private void StartDinucleotidesAnalyzerButtonClick(object sender, RoutedEventArgs e)
		{
			_dinucleotidesAnalyzerWindow.UpdateFileStream(_fileStream);
			_dinucleotidesAnalyzerWindow.Analyze(ChunkSizeTextBox, SimilaritySlider.Value);

			OpenFile();
		}

		private void HorizontalScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{

		}

		private void ShowGraph(object sender, RoutedEventArgs e)
		{
			_dinucleotidesAnalyzerWindow.UpdateFileStream(_fileStream);
			_dinucleotidesAnalyzerWindow.ShowGraph(((Button)sender).Tag.ToString());

			OpenFile();
		}

		private void OpenFile()
		{
			if (_fileStream != null)
				_fileStream.Close();

			FileOpener fileOpener = new FileOpener();

			_fileStream = fileOpener.OpenFile(_filePath);
		}
	}
}