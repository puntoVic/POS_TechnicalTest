using Common.Models;
using System.Reflection;

namespace CalculusFunctions.Contracts
{
    public interface IChangeCalculation
    {
        public ManagerResult<Dictionary<double, int>> CalculateChange(Dictionary<double, double> amountPayment, double price);
    }
}