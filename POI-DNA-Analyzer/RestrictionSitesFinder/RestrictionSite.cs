using System;

namespace POI_DNA_Analyzer
{
	internal class RestrictionSite
	{
		public RestrictionSite(string name, IEnumerable<int> indexes) 
		{ 
			Name = name;
			Indexes = indexes;
		}

		public string Name { get; private set; }

		public IEnumerable<int> Indexes { get; private set; }

		public string IndexesString => ConvertIndexesToString();

		public string ConvertIndexesToString()
		{
			List<int> indexes = new List<int>(Indexes);
			string result = "";

			for (int i = 0; i < indexes.Count; i++)
			{
				if (i + 1 == indexes.Count)
				{
					result += $"{indexes[i]}";
					continue;
				}

				result += $"{indexes[i]}, ";
			}

			return result;
		}
	}
}
