using System.Text.RegularExpressions;

namespace POI_DNA_Analyzer
{
	class PalindromicSequencesFinder
    {
		public PalindromicSequencesFinder() 
		{
			PalindromicSequences = new Dictionary<int, string>();
		}

		public Dictionary<int, string> PalindromicSequences {  get; private set; }

		public void Start(string standardDNA)
		{
			PalindromicSequences.Clear();
			string complementaryDNA = new ComplementaryDNACreator().Create(standardDNA);
			//Read(standardDNA, complementaryDNA);
			Read(standardDNA);
		}

		private void Read(string standardDNA, string complementaryDNA)
		{
			complementaryDNA = InvertText(complementaryDNA);

			for (int i = 0; i < standardDNA.Length; i++)
			{
				if (standardDNA[i] != complementaryDNA[i])
					return;

				int startIndex = i;
				PalindromicSequences.Add(startIndex, "");

				for (int j = i; j < complementaryDNA.Length; j++)
				{
					i = j;

					if (standardDNA[i] != complementaryDNA[i])
						break;

					PalindromicSequences[startIndex] += standardDNA[i];
				}
			}
		}

		private void Read(string seq)
		{
			int seqLen = seq.Length;

			for (int i = 0; i < seqLen + 1; i++)
			{
				for (int j = 0; j <= seqLen && (i + j) <= seqLen; j++)
				{
					string subSeq = seq.Substring(i, j);

					if (DNAIsPalindrome(subSeq) == true && subSeq != "")
					{
						PalindromicSequences[i] = subSeq;
					}
				}
			}
		}

		private bool DNAIsPalindrome(string seq)
		{
			return seq == RevCompDNA(seq);
		}

		private string RevCompDNA(string seq)
		{
			char[] revCompArray = new char[seq.Length];
			for (int i = 0; i < seq.Length; i++)
			{
				revCompArray[seq.Length - 1 - i] = Complement(seq[i]);
			}
			return new string(revCompArray);
		}

		private char Complement(char nucleotide)
		{
			switch (nucleotide)
			{
				case 'A': return 'T';
				case 'T': return 'A';
				case 'C': return 'G';
				case 'G': return 'C';
				default: throw new ArgumentException("Invalid nucleotide");
			}
		}


		private string InvertText(string text)
		{
			char[] charArray = text.ToCharArray();
			Array.Reverse(charArray);
			return new string(charArray);
		}
	}
}
