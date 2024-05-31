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
		private string _fileText = "";
		private string _filePath = "";

		public MainWindow()
		{
			InitializeComponent();
			Localize("ru");

			_sequencesFinderWindow = new SequencesFinderWindow(ResultText, List);
			_dinucleotidesAnalyzerWindow = new DinucleotidesAnalyzerWindow(OxyPlot, EnableSliderCheckBox, HorizontalScrollBar);
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
			_sequencesFinderWindow.Save(ResultText.Text);
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
			_dinucleotidesAnalyzerWindow.Save();
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
			_openReadingFrameWindow.Start(_fileStream);

			OpenFile();
		}

		private void SaveComplementaryDNAFileButtonClick(object sender, RoutedEventArgs e)
		{
			_openReadingFrameWindow.Save();

			OpenFile();
		}

		private void ShowGraph(object sender, RoutedEventArgs e)
		{
			_dinucleotidesAnalyzerWindow.UpdateText(_fileText);
			_dinucleotidesAnalyzerWindow.ShowGraph(((Button)sender).Tag.ToString());
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