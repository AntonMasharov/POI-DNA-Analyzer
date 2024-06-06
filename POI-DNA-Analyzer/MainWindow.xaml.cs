using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	public partial class MainWindow : Window
	{
		private SequencesFinderWindow _sequencesFinderWindow;
		private DinucleotidesAnalyzerWindow _dinucleotidesAnalyzerWindow;
		private OpenReadingFrameWindow _openReadingFrameWindow;
		private StreamReader _fileStream;
		private CommonFilePath _commonFilePath;
		private SaveContextMenu _saveContextMenu;
		private string _fileText = "";
		private string _filePath = "";

		public MainWindow()
		{
			InitializeComponent();
			Localize("ru");

			_saveContextMenu = new SaveContextMenu(SaveIndividuallyCheckbox, SaveTogetherCheckbox);
			_commonFilePath = new CommonFilePath();
			_sequencesFinderWindow = new SequencesFinderWindow(ResultText, List, _commonFilePath);
			_dinucleotidesAnalyzerWindow = new DinucleotidesAnalyzerWindow(DinucleotidesAnalyzerProgressBar, OxyPlot, EnableSliderCheckBox, HorizontalScrollBar, _commonFilePath);
			_openReadingFrameWindow = new OpenReadingFrameWindow();
		}

		private void OpenFileButtonClick(object sender, RoutedEventArgs e)
		{
			FilePicker filePicker = new FilePicker();

			_filePath = filePicker.PickFilePath();

			OpenFile();
			ReadTextFromFile(_fileStream);
		}

		private void SaveFileButtonClick(object sender, RoutedEventArgs e)
		{
			if (SaveIndividuallyCheckbox.IsChecked == false)
				_sequencesFinderWindow.Save(ResultText.Text);
			else
				_sequencesFinderWindow.SaveIndividually(ResultText.Text);
		}

		private void EnterPromptButtonClick(object sender, RoutedEventArgs e)
		{
			_sequencesFinderWindow.Find(PromptField.Text, _fileText);
		}

		private void ClearResultButtonClick(object sender, RoutedEventArgs e)
		{
			_sequencesFinderWindow.Clear();
		}

		private void SaveDinucleotidesAnalyzerButtonClick(object sender, RoutedEventArgs e)
		{
			if (SaveIndividuallyCheckbox.IsChecked == false)
				_dinucleotidesAnalyzerWindow.Save();
			else
				_dinucleotidesAnalyzerWindow.SaveIndividually();
		}

		private void StartDinucleotidesAnalyzerButtonClick(object sender, RoutedEventArgs e)
		{
			_dinucleotidesAnalyzerWindow.UpdateText(_fileText);
			_dinucleotidesAnalyzerWindow.Analyze(ChunkSizeTextBox, SimilaritySlider.Value);
		}

		private void HorizontalScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{

		}

		private void CreateComplementaryDNAButtonClick(object sender, RoutedEventArgs e)
		{
			_openReadingFrameWindow.StartEverything(_fileText);
		}

		private void SaveComplementaryDNAFileButtonClick(object sender, RoutedEventArgs e)
		{
			//_openReadingFrameWindow.Save();
		}

		private void ShowGraph(object sender, RoutedEventArgs e)
		{
			_dinucleotidesAnalyzerWindow.UpdateText(_fileText);
			_dinucleotidesAnalyzerWindow.ShowGraph(((Button)sender).Tag.ToString());
		}

		private void SaveMenuButtonClick(object sender, RoutedEventArgs e)
		{

			SaveMenuButton.ContextMenu.PlacementTarget = ((Button)sender);
			SaveMenuButton.ContextMenu.IsOpen = true;
		}

		private void OpenFile()
		{
			if (_fileStream != null)
				_fileStream.Close();

			FileOpener fileOpener = new FileOpener();

			_fileStream = fileOpener.OpenFile(_filePath);
		}

		private void ReadTextFromFile(StreamReader streamReader)
		{
			FilePreparator filePreparator = new FilePreparator();
			_fileText = filePreparator.GetString(streamReader);

			_commonFilePath.TrySetSequenceFolderName(filePreparator.Header);
		}

		private void SetRULang(object sender, RoutedEventArgs e)
		{
			Localize("ru");
		}

		private void SetENLang(object sender, RoutedEventArgs e)
		{
			Localize("en");
		}

		private void Localize(string code)
		{
			string path = "..\\Resources\\StringResources." + code + ".xaml";

			ResourceDictionary dictionary = new ResourceDictionary();
			dictionary.Source = new Uri(path, UriKind.Relative);

			this.Resources.MergedDictionaries.Add(dictionary);
		}
	}
}