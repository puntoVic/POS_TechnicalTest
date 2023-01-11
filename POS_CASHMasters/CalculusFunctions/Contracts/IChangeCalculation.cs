using System.Reflection;

namespace CalculusFunctions.Contracts
{
    public interface IChangeCalculation
    {
        public Dictionary<double, int> CalculateChange(Dictionary<double, double> amountPayment, double price);
    }
}