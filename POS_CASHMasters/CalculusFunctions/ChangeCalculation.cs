using CalculusFunctions.Contracts;
using Common.Catalogs;
using Common.Models;
using Data.Catalogs;
using System.Configuration;

namespace CalculusFunctions
{
    public class ChangeCalculation : IChangeCalculation
    {
        private double[] denominations;
        private readonly Dictionary<double, int> coinsUsed;
        private static readonly object padlock = new();
        private static ChangeCalculation? instance;
        
        /// <summary>
        /// Constructor with the initial params 
        /// to fill coinUsed Dictionary
        /// </summary>
        private ChangeCalculation()
        {
            coinsUsed = new();
        }

        /// <summary>
        /// Using singleton to use only one instance
        /// </summary>
        public static ChangeCalculation Instance
        {
            get
            {
                lock (padlock)
                {
                    instance ??= new ChangeCalculation();
                    return instance;
                }
            }
        }

        /// <summary>
        /// Return the smallest number of bills and coins equal to the change due
        /// </summary>
        /// <param name="amountPayment">
        /// Dictionary that contains amount of bills and coins according with its denomination
        /// </param>
        /// <param name="price"> Price to pay</param>
        /// <returns></returns>
        public ManagerResult<Dictionary<double, int>> CalculateChange(Dictionary<double, double> amountPayment, double price)
        {
            if (amountPayment == null)
            {
                return ManagerResult<Dictionary<double, int>>.FromError(ErrorCodes.NullReferenceException);
            }
            if (this.denominations == null)
            {
                return ManagerResult<Dictionary<double, int>>.FromError(ErrorCodes.NullReferenceException);
            }
            if (!CheckDenominations(amountPayment))
            {
                return ManagerResult<Dictionary<double, int>>.FromError(ErrorCodes.WrongDenomination);
            }
            double totalAmount = CalculateAmountPayment(amountPayment);
            try
            {
                if (!(totalAmount > price))
                {
                    return ManagerResult<Dictionary<double, int>>.FromError(ErrorCodes.InsufficientAmount);
                }
                
                double totalChange = totalAmount - price;

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
            catch (NullReferenceException)
            {
                return ManagerResult<Dictionary<double, int>>.FromError(ErrorCodes.NullReferenceException);
            }
            
            return ManagerResult<Dictionary<double,int>>.FromSuccess(value: coinsUsed);
        }

        /// <summary>
        /// Private function to calculate total amount entered
        /// </summary>
        /// <param name="amountPayment">
        /// Dictionary that contains amount of bills and coins according with its denomination
        /// </param>
        /// <returns></returns>
        private static double CalculateAmountPayment(Dictionary<double, double> amountPayment)
        {
            double totalAmount = 0;
            foreach (var amount in amountPayment)
            {
                totalAmount += amount.Key * amount.Value;
            }
            
            return totalAmount;
        }

        private bool CheckDenominations(Dictionary<double, double> amountPayment)
        {
            foreach (var amount in amountPayment)
            {
                if (!denominations.Contains(amount.Key))
                    return false;
            }
            return true;
        }

        public ManagerResult<double[]> GetDenominations()
        {
            if(denominations != null)
                return ManagerResult<double[]>.FromSuccess(denominations);
            return ManagerResult<double[]>.FromError(ErrorCodes.NotExistCurrencyCatalog);
        }

        public ManagerResult<double[]> SetDenominations(string? currency)
        {
            try
            {
                this.denominations??= Denominations.Currencies[currency];
                for (int i = 0; i < this.denominations.Length; i += 1)
                {
                    coinsUsed.Add(denominations[i], 0);
                }
                
                return ManagerResult<double[]>.FromSuccess(this.denominations);
            }
            catch
            {
                return ManagerResult<double[]>.FromError(ErrorCodes.NotExistCurrencyCatalog);
            }
        }
    }
}