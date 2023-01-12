namespace Data.Catalogs
{
    /// <summary>
    /// Denominations Catalog
    /// Simulate a DB with currencies info
    /// </summary>
    public static class Denominations
    {
        private static double[] USA = { 100, 50, 20, 10, 5, 2, 1, 0.5, 0.25, 0.1, 0.05, 0.01 };

        private static double[] EUR = { 200, 100, 50, 20, 10, 5, 2, 1, 0.5, 0.25, 0.1, 0.05, 0.01 };

        private static double[] JPY = { 10000, 5000, 2000, 1000, 500, 100, 50, 10, 5, 1 };

        private static double[] CHF = { 200, 100, 50, 20, 10, 5, 2, 1, 0.5, 0.25, 0.1, 0.05 };

        public static Dictionary<string, double[]> Currencies = new Dictionary<string, double[]>()
        {
            { "USA", USA },
            { "EUR", EUR},
            { "JPY", JPY},
            { "CHF", CHF}
        };
    }
}
