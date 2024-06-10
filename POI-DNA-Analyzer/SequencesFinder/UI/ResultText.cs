using System.Windows.Controls;

namespace POI_DNA_Analyzer
{
	internal class ResultText
	{
		private TextBlock _textBlock;

		public ResultText(TextBlock textBlock)
		{
			_textBlock = textBlock;
		}

		public void Clear()
		{
			_textBlock.Text = "";
		}

		public void ShowOccurrencesCount(string text)
		{
			_textBlock.Text = $"{text}";
		}

		public void ShowOccurrencesIndexes(string indexes)
		{
			_textBlock.Text += "\n";
			_textBlock.Text += indexes;
		}
	}
}
