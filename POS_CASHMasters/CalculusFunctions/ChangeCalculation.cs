using CalculusFunctions.Contracts;

namespace CalculusFunctions
{
    public class ChangeCalculation : IChangeCalculation
    {
        static decimal decimalChange;

        Dictionary<decimal, decimal> IChangeCalculation.CalculateChange(Dictionary<decimal, decimal> amountPayment, decimal price)
        {
            decimal[] denominations = { 100, 50, 20, 10, 5, 1 };
            decimal totalAmount = calculateAmountPayment(amountPayment);
            Dictionary<decimal, decimal> minChange = new();
            if (totalAmount > price)
            {
                decimal decimalChange = totalAmount - price;
                int n = denominations.Length;

                minChange.Add(0, 0);

                for (int i = 1; i <= n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (denominations[j] <= i)
                        {
                            decimal temp = minChange[i - denominations[j]] + 1;
                            if (temp < minChange[i])
                                minChange[i] = temp;
                        }
                    }
                }

                Console.WriteLine("The minimum number of coins needed to make change is: " + minChange[(int)decimalChange]);


            }
            return minChange;
;
        }

        private decimal calculateAmountPayment(Dictionary<decimal, decimal> amountPayment)
        {
            decimal totalAmount = 0;
            foreach (var amount in amountPayment)
            {
                totalAmount += amount.Key * amount.Value;
            }
            return totalAmount;
        }
    }
}