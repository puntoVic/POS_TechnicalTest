using CalculusFunctions.Contracts;

namespace CalculusFunctions
{
    public class ChangeCalculation : IChangeCalculation
    {
        
        Dictionary<double, int> IChangeCalculation.CalculateChange(Dictionary<double, double> amountPayment, double price)
        {
            double[] denominations = {1000, 500, 100, 50, 20, 10, 5, 1, 0.5 };
            double totalAmount = calculateAmountPayment(amountPayment);
            Dictionary<double, int> coinsUsed = new();
            if (totalAmount > price)
            {
                double  totalChange = totalAmount - price;
                int n = denominations.Length;
                for (int i = 0; i < n; i += 1)
                {
                    coinsUsed.Add(denominations[i], 0);
                }
                


        
                var changeTemp = totalChange;
                foreach (var coin in denominations)
                {
                    if (coin < totalChange)
                    {
                        coinsUsed[coin] = (int)(changeTemp / coin);
                        changeTemp = changeTemp - (coinsUsed[coin] * coin);
                    }
                }
                
            }
            foreach (var coin in coinsUsed)
                if(coin.Value > 0)
                Console.Write("You need to give " + coin.Value + " of denomination "+ coin.Key + " \n");

            return coinsUsed;
;
        }

        private double calculateAmountPayment(Dictionary<double, double> amountPayment)
        {
            double totalAmount = 0;
            foreach (var amount in amountPayment)
            {
                totalAmount += amount.Key * amount.Value;
            }
            return totalAmount;
        }
    }
}