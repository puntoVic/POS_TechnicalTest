

using CalculusFunctions;
using CalculusFunctions.Contracts;

internal class Program
{
    private static void Main(string[] args)
    {
        Dictionary<double,double> amountDictionary = new Dictionary<double,double>();
        amountDictionary.Add(1000, 1);
        IChangeCalculation changeCalculation = new ChangeCalculation();
        changeCalculation.CalculateChange(amountDictionary, 545.5);
        
        
    }
}