using CalculusFunctions.Contracts;

namespace CalculusFunctions
{
    public class ChangeCalculation : IChangeCalculation
    {
        private double[] denominations;
        public ChangeCalculation(double[] denominations)
        {
            this.denominations = denominations;
        }

        Dictionary<double, int> IChangeCalculation.CalculateChange(Dictionary<double, double> amountPayment, double price)
        {
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
            
            return coinsUsed;
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