using Data.Catalogs;
using CalculusFunctions;
using CalculusFunctions.Contracts;
using System.Configuration;
using Common.Catalogs;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

internal class Program
{
    

    private static void Main()
    {
        bool turnOn = true;
        
        string? currency = ConfigurationManager.AppSettings["Currency"];
        IChangeCalculation changeCalculation = ChangeCalculation.Instance;
        var denominationsResult = changeCalculation.SetDenominations(currency);
        if (!denominationsResult.DidSucceed)
        {
            ///For change currency need to replace the value in POS_CASHMasters/App.config file
            ///currencies availables: "USA", "EUR", "JPY", "CHF"
            Console.Write("Currency configuration error");
            turnOn = false;
        }
        while (turnOn)
        {
            var denominations = denominationsResult.Value;
            Console.Write("Denominations available in " + currency + ":");
            foreach (var denomination in denominations)
            {
                Console.Write(denomination + ", ");
            }

            bool ready = false;
            Dictionary<double, double> amountDictionary = new();
            while (!ready)
            {
                try
                {
                    Console.Write("\n\nSet denomination: ");
                    double denomination = double.Parse(Console.ReadLine());
                    if (denominations.Contains(denomination) && !amountDictionary.ContainsKey(denomination))
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
                        Console.Write("\nWrong or repeat denomination. Try again.\n");
                    }
                }
                catch (FormatException ex)
                {
                    Console.Write(ex.Message);
                }
                catch (Exception ex)
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
            else if (coinsUsed.ErrorCode == ErrorCodes.InsufficientAmount)
            {
                Console.Write("\n The amount paid is insufficient \n");
            }
            else if (coinsUsed.ErrorCode == ErrorCodes.WrongDenomination)
            {
                Console.Write("\n Some denomination insert doesn't exist \n");
            }

            Console.Write("\n If you want to exit press 'y' or press enter to continue\n");
            string turnOffresponse = Console.ReadLine();
            if (turnOffresponse == "y")
            {
                turnOn = false;
            }
        }
    }
}