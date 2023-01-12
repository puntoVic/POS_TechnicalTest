using CalculusFunctions.Contracts;
using Common.Catalogs;
using Common.Models;

namespace CalculusFunctions
{
    public class ChangeCalculation : IChangeCalculation
    {
        private double[] denominations;
        private Dictionary<double, int> coinsUsed;
        public ChangeCalculation(double[] denominations)
        {
            this.denominations = denominations;
            coinsUsed = new();
            for (int i = 0; i < denominations.Length; i += 1)
            {
                coinsUsed.Add(denominations[i], 0);
            }
        }

        public ManagerResult<Dictionary<double, int>> CalculateChange(Dictionary<double, double> amountPayment, double price)
        {
            double totalAmount = calculateAmountPayment(amountPayment);
            
            if (totalAmount > price)
            {
                double  totalChange = totalAmount - price;
                
                var changeTemp = totalChange;
                foreach (var coin in denominations)
                {
                    if (coin < totalChange)
                    {
                        coinsUsed[coin] = (int)(changeTemp / coin);
                        changeTemp -= (coinsUsed[coin] * coin);
                    }
                }
                
            }
            else
            {
                return ManagerResult<Dictionary<double, int>>.FromError(ErrorCodes.InsufficientAmount);
            }
            
            return ManagerResult<Dictionary<double,int>>.FromSuccess(value: coinsUsed);
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