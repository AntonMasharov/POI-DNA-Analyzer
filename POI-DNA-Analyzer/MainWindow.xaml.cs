using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	public partial class MainWindow : Window
	{
		private SequencesFinderWindowController _sequencesFinderWindow;
		private DinucleotidesAnalyzerWindowController _dinucleotidesAnalyzerWindow;
		private OpenReadingFrameWindowController _openReadingFrameWindow;
		private ATGCWindowController _atgcWindow;
		private RestrictionSiteFinderWindowController _restrictionSiteFinderWindow;

		private FeedbackText _feedbackText;

		private StreamReader _fileStream;
		private CommonFilePath _commonFilePath;
		private SaveContextMenu _saveContextMenu;

		private string _fileText = "";
		private string _filePath = "";

		public MainWindow()
		{
			InitializeComponent();

			_saveContextMenu = new SaveContextMenu(SaveIndividuallyCheckbox, SaveTogetherCheckbox);
			_commonFilePath = new CommonFilePath();
			_sequencesFinderWindow = new SequencesFinderWindowController(ResultText, List, _commonFilePath);
			_dinucleotidesAnalyzerWindow = new DinucleotidesAnalyzerWindowController(OxyPlot, EnableSliderCheckBox, HorizontalScrollBar, _commonFilePath);
			_openReadingFrameWindow = new OpenReadingFrameWindowController(_commonFilePath);
			_restrictionSiteFinderWindow = new RestrictionSiteFinderWindowController(RestrictionSitesDataGrid, _commonFilePath);
			_atgcWindow = new ATGCWindowController(OxyPlotATCGPercent, OxyPlotATCGPercentHorizontalScrollBar, ATGCResult, _commonFilePath);
			_feedbackText = new FeedbackText(Feedback);

			Localize("ru");
		}

		private void OpenFileButtonClick(object sender, RoutedEventArgs e)
		{
			FilePicker filePicker = new FilePicker();

			_filePath = filePicker.PickFilePath(filePicker.FilterTXTandFASTA);

			if (_filePath == null)
				return;

			_feedbackText.DisplayFileUploaded(_filePath);

			OpenFile();
			ReadTextFromFile(_fileStream);
		}

		private void SaveFileButtonClick(object sender, RoutedEventArgs e)
		{
			string path;

			if (SaveIndividuallyCheckbox.IsChecked == false)
				path = _sequencesFinderWindow.Save();
			else
				path = _sequencesFinderWindow.SaveIndividually();

			CheckSaved(path);
		}

		private void EnterPromptButtonClick(object sender, RoutedEventArgs e)
		{
			CheckPath();
			_sequencesFinderWindow.Find(PromptField.Text, _fileText);
		}

		private void ClearResultButtonClick(object sender, RoutedEventArgs e)
		{
			_sequencesFinderWindow.Clear();
		}

		private void SaveDinucleotidesAnalyzerButtonClick(object sender, RoutedEventArgs e)
		{
			string path;

			if (SaveIndividuallyCheckbox.IsChecked == false)
				path = _dinucleotidesAnalyzerWindow.Save();
			else
				path = _dinucleotidesAnalyzerWindow.SaveIndividually();

			CheckSaved(path);
		}

		private void StartDinucleotidesAnalyzerButtonClick(object sender, RoutedEventArgs e)
		{
			CheckPath();
			_dinucleotidesAnalyzerWindow.UpdateText(_fileText);
			_dinucleotidesAnalyzerWindow.Analyze( SimilaritySlider.Value, CheckInputToTextBlock(ChunkSizeTextBox));
		}

		private void CreateComplementaryDNAButtonClick(object sender, RoutedEventArgs e)
		{
			CheckPath();

			string result = _openReadingFrameWindow.GenerateComplementaryDNA(_fileText);

			if (result != "")
				_feedbackText.DisplayComplementaryDNACreated();
		}

		private void SaveComplementaryDNAFileButtonClick(object sender, RoutedEventArgs e)
		{
			string path;

			if (SaveIndividuallyCheckbox.IsChecked == false)
				path = _openReadingFrameWindow.SaveComplementaryDNA();
			else
				path = _openReadingFrameWindow.SaveComplementaryDNAIndividually();

			CheckSaved(path);
		}

		private void StartTranslation(object sender, RoutedEventArgs e)
		{
			CheckPath();
			bool result = _openReadingFrameWindow.TranslateCodonsToFiles(_fileText);

			if (result == true)
				_feedbackText.DisplayTranslationCompleted();
		}

		private void ChangeTranslationResultPath(object sender, RoutedEventArgs e)
		{
			_openReadingFrameWindow.ChangeTranslationResultPath();
		}

		private void ChangeTranslationConfig(object sender, RoutedEventArgs e)
		{
			if (_openReadingFrameWindow.ChangeTranslationConfig())
				_feedbackText.DisplayConfigUpdated();
		}

		private void ResetTranslationConfigButtonClick(object sender, RoutedEventArgs e)
		{
			_openReadingFrameWindow.ResetTranslationConfig();
			_feedbackText.DisplayConfigReset();
		}

		private void OpenReadingFrameStartButtonClick(object sender, RoutedEventArgs e)
		{
			CheckPath();
			bool result = _openReadingFrameWindow.StartOpenReadingFrame(CheckInputToTextBlock(MinSizeToSaveTextBox));

			if (result == true)
				_feedbackText.DisplayOpenReadingFramesCreated();
		}

		private void ChangeOpenReadingFramesResultPath(object sender, RoutedEventArgs e)
		{
			_openReadingFrameWindow.ChangeOpenReadingFramesResultPath();
		}

		private void ChangeOpenReadingFramesConfig(object sender, RoutedEventArgs e)
		{
			if (_openReadingFrameWindow.ChangeOpenReadingFramesConfig())
				_feedbackText.DisplayConfigUpdated();
		}

		private void ResetOpenReadingFramesConfigButtonClick(object sender, RoutedEventArgs e)
		{
			_openReadingFrameWindow.ResetOpenReadingFramesConfig();
			_feedbackText.DisplayConfigReset();
		}

		private void CreateEverythingButtonClick(object sender, RoutedEventArgs e)
		{
			CheckPath();
			bool result = _openReadingFrameWindow.StartEverything(_fileText, CheckInputToTextBlock(MinSizeToSaveTextBox));

			if (result == true)
				_feedbackText.DisplayOpenReadingFramesCreated();
		}

		private void ChooseOpenReadingFramesSavePathButtonClick(object sender, RoutedEventArgs e)
		{
			_commonFilePath.ChooseFolder();
		}

		private void StartATGCPercentButtonClick(object sender, RoutedEventArgs e)
		{
			CheckPath();
			_atgcWindow.Start(_fileText, CheckInputToTextBlock(ATCGChunkSizeTextBox));
		}

		private void SaveATGCPercentButtonClick(object sender, RoutedEventArgs e)
		{
			string path;

			if (SaveIndividuallyCheckbox.IsChecked == false)
				path = _atgcWindow.Save();
			else
				path = _atgcWindow.SaveIndividually();

			CheckSaved(path);
		}

		private void StartRestrictionSitesFinderButtonClick(object sender, RoutedEventArgs e)
		{
			CheckPath();
			_restrictionSiteFinderWindow.Start(_fileText);
		}

		private void SaveRestrictionSitesFinderResultButtonClick(object sender, RoutedEventArgs e)
		{
			string path;

			if (SaveIndividuallyCheckbox.IsChecked == false)
				path = _restrictionSiteFinderWindow.Save();
			else
				path = _restrictionSiteFinderWindow.SaveIndividually();

			CheckSaved(path);
		}

		private void ChangeRestrictionSiteFinderConfigButtonClick(object sender, RoutedEventArgs e)
		{
			_restrictionSiteFinderWindow.UpdateConfig();
		}

		private void ResetRestrictionSiteFinderConfigButtonClick(object sender, RoutedEventArgs e)
		{
			_restrictionSiteFinderWindow.ResetConfig();
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

			//Not the responsibility of this method, but for now it's normal
			_openReadingFrameWindow.ResetComplementaryDNA();
			_openReadingFrameWindow.ResetLists();
		}

		private void ReadTextFromFile(StreamReader streamReader)
		{
			if (streamReader == null)
				return;

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

			if (code == "ru")
			{
				_openReadingFrameWindow.ChangeResultLanguage(Languages.Russian);
				_sequencesFinderWindow.ChangeResultLanguage(Languages.Russian);
				_feedbackText.ChangeLanguage(Languages.Russian);
			}
			else
			{
				_openReadingFrameWindow.ChangeResultLanguage(Languages.English);
				_sequencesFinderWindow.ChangeResultLanguage(Languages.English);
				_feedbackText.ChangeLanguage(Languages.English);
			}
		}

		private bool IsPathChoosen()
		{
			if (_fileText == null || _fileText == "")
				return false;
			else
				return true;
		}

		private void CheckSaved(string path)
		{
			if (path == "")
			{
				_feedbackText.DisplayNothingToSave();
				return;
			}

			_feedbackText.DisplayFileSaved(path);
		}

		private void CheckPath()
		{
			if (IsPathChoosen() == false)
				_feedbackText.DisplayChooseFileFirst();
		}

		private int CheckInputToTextBlock(TextBox textBox)
		{
			int value = 0;

			if (int.TryParse(textBox.Text, out value) == false || value <= 0)
			{
				value = 100;
				textBox.Text = value.ToString();
				_feedbackText.DisplayIncorrectInput();
			}

			return value;
		}
	}
}