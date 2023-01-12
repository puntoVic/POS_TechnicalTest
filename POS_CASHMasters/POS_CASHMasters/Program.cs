using Data.Catalogs;
using CalculusFunctions;
using CalculusFunctions.Contracts;
using System.Configuration;
using Common.Catalogs;

internal class Program
{
    private static void Main(string[] args)
    {
        string currency = ConfigurationManager.AppSettings["Currency"];
        double[] denominations = Denominations.Currencies[currency];
        IChangeCalculation changeCalculation = new ChangeCalculation(denominations);
        

        Console.Write("Denominations available in " + currency + ":");
        foreach (var denomination in denominations)
        {
            Console.Write(denomination + ", ");
        }

        bool ready = false;
        Dictionary<double, double> amountDictionary = new Dictionary<double, double>();
        while (!ready)
        {
            try
            {
                Console.Write("\n\nSet denomination: ");
                double denomination = double.Parse(Console.ReadLine());
                if (denominations.Contains(denomination))
                {
                    Console.Write("\nAdd quantity of this denomination: ");
                    double quantity = double.Parse(Console.ReadLine());
                    if (quantity > 0)
                    {
                        amountDictionary.Add(denomination, quantity);
                    }
                    Console.Write("\nAre all denominations to load(y/n): ");
                    string response = Console.ReadLine();
                    if (response == "y")
                    {
                        ready = true;
                    }
                }
                else
                {
                    Console.Write("\nWrong denomination. Try again.\n");
                }
            }
            
            catch (FormatException ex) 
            {
                Console.Write(ex.Message);
            }
        }
        double price = 0;
        try
        {
            Console.Write("\n\nSet the price: ");
            price = double.Parse(Console.ReadLine());
        }
        catch (FormatException ex)
        {
            Console.Write(ex.Message);
        }

        var coinsUsed = changeCalculation.CalculateChange(amountDictionary, price);
        if (coinsUsed.DidSucceed)
        {
            foreach (var coin in coinsUsed.Value)
            {
                if (coin.Value > 0)
                {
                    Console.Write("\n\nYou need to give " + coin.Value + " of denomination " + coin.Key + " \n");
                }
            }
        }
        else if(coinsUsed.ErrorCode == ErrorCodes.InsufficientAmount)
        {
            Console.Write("\n The amount paid is insufficient \n");
        }
    }
}