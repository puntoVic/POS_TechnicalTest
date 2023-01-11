using CalculusFunctions.Contracts;

namespace CalculusFunctions
{
    public class ChangeCalculation : IChangeCalculation
    {
        Dictionary<int, int> IChangeCalculation.CalculateChange(Dictionary<int, int> amountPayment, decimal price)
        {
            throw new NotImplementedException();
        }
    }
}