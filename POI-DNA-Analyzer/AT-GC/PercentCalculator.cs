namespace POI_DNA_Analyzer
{
	class PercentCalculator
    {
		public double GetPercent(double part, double whole)
		{
			if (whole == 0)
				throw new ArgumentException("Whole is zero");

			return Math.Abs(part / whole);
		}
    }
}
