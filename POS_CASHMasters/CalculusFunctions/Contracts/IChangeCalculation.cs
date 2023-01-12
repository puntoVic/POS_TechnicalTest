using Common.Models;
using System.Reflection;

namespace CalculusFunctions.Contracts
{
    public interface IChangeCalculation
    {
        public ManagerResult<Dictionary<double, int>> CalculateChange(Dictionary<double, double>? amountPayment, double price);

        public ManagerResult<double[]> GetDenominations();

        public ManagerResult<double[]> SetDenominations(string? currency);


    }
}