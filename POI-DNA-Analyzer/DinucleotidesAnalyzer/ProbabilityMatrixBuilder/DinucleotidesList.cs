namespace POI_DNA_Analyzer
{
	internal class DinucleotidesList
	{
		public List<string> Get { get; private set; } = new List<string>();

		public DinucleotidesList() 
		{
			BuildList();
		}

		private void BuildList()
		{
			NucleotidesList nucleotidesList = new NucleotidesList();

			for (int i = 0; i < nucleotidesList.Get.Count; i++)
			{
				for (int j = 0; j <  nucleotidesList.Get.Count; j++)
				{
					string dinucleotide = nucleotidesList.Get[i].ToString() + nucleotidesList.Get[j].ToString();

					Get.Add(dinucleotide);
				}
			}
		}
	}
}
