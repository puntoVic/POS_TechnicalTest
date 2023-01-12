
using Data.Catalogs;
using CalculusFunctions;
using CalculusFunctions.Contracts;
using System.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        string currency = ConfigurationManager.AppSettings["Currency"];
        double[] denominations = Denominations.Currencies[currency];
        IChangeCalculation changeCalculation = new ChangeCalculation(denominations);

        Dictionary<double,double> amountDictionary = new Dictionary<double,double>();
        amountDictionary.Add(1000, 1);
        
        changeCalculation.CalculateChange(amountDictionary, 545.5);
        


    }
}