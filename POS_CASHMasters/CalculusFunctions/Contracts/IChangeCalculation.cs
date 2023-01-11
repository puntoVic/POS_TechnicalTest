using System.Reflection;

namespace CalculusFunctions.Contracts
{
    public interface IChangeCalculation
    {
        public Dictionary<decimal, decimal> CalculateChange(Dictionary<decimal, decimal> amountPayment, decimal price);
    }
}