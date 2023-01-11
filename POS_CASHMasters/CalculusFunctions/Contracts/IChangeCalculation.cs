using System.Reflection;

namespace CalculusFunctions.Contracts
{
    public interface IChangeCalculation
    {
        public Dictionary<int, int> CalculateChange(Dictionary<int, int> amountPayment, decimal price);
    }
}