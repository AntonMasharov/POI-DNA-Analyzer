using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	internal class FeedbackText
	{
		private TextBlock _text;
		private FeedbackEntities _feedbackEntities;
		private FeedbackFileReader _feedbackFileReader;
		private FeedbackFile _feedbackFile;
		private Languages _language = Languages.English;

		public FeedbackText(TextBlock textBlock)
		{
			_feedbackEntities = new FeedbackEntities();
			_feedbackFile = new FeedbackFile();
			_feedbackFileReader = new FeedbackFileReader(_feedbackEntities, _feedbackFile);
			_feedbackFileReader.Read();

			_text = textBlock;
		}

		public void DisplayFileUploaded(string filePath)
		{
			string text = string.Format(_feedbackEntities.GetCorrespondingFeedbackEntity("FileUploaded", _language), filePath);
			_text.Text = text;
		}

		public void DisplayFileSaved(string filePath)
		{
			string text = string.Format(_feedbackEntities.GetCorrespondingFeedbackEntity("FileSaved", _language), filePath);
			_text.Text = text;
		}

		public void DisplayChooseFileFirst()
		{
			_text.Text = _feedbackEntities.GetCorrespondingFeedbackEntity("ChooseFileFirst", _language);
		}

		public void DisplayNothingToSave()
		{
			_text.Text = _feedbackEntities.GetCorrespondingFeedbackEntity("NothingToSave", _language);
		}

		public void DisplayConfigUpdated()
		{
			_text.Text = _feedbackEntities.GetCorrespondingFeedbackEntity("ConfigUpdated", _language);
		}

		public void DisplayConfigReset()
		{
			_text.Text = _feedbackEntities.GetCorrespondingFeedbackEntity("ConfigReset", _language);
		}

		public void DisplayIncorrectInput()
		{
			_text.Text = _feedbackEntities.GetCorrespondingFeedbackEntity("IncorrectInput", _language);
		}

		public void DisplayComplementaryDNACreated()
		{
			_text.Text = _feedbackEntities.GetCorrespondingFeedbackEntity("ComplementaryDNACreated", _language);
		}

		public void DisplayOpenReadingFramesCreated()
		{
			_text.Text = _feedbackEntities.GetCorrespondingFeedbackEntity("OpenReadingFramesCompleted", _language);
		}

		public void DisplayTranslationCompleted()
		{
			_text.Text = _feedbackEntities.GetCorrespondingFeedbackEntity("TranslationCompleted", _language);
		}

		public void ChangeLanguage(Languages language)
		{
			_language = language;
		}
	}
}
