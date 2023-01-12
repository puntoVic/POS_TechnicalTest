using CalculusFunctions;
using CalculusFunctions.Contracts;
using Common.Catalogs;

namespace POS_Test
{
    [TestClass]
    public class DenominationErrorsTest
    {
        readonly IChangeCalculation changeCalculation = ChangeCalculation.Instance;
        
        [TestMethod]
        public void SetDenominationWrong_TestMethod()
        {
            var denominationsResult1 = changeCalculation.SetDenominations("");
            var denominationsResult2 = changeCalculation.SetDenominations(null);
            Assert.IsNotNull(denominationsResult1);
            Assert.IsNotNull(denominationsResult2);
            Assert.AreEqual(ErrorCodes.NotExistCurrencyCatalog, denominationsResult1.ErrorCode);
            Assert.AreEqual(ErrorCodes.NotExistCurrencyCatalog, denominationsResult2.ErrorCode);
        }

        [TestMethod]
        public void CalculateWithoutSetDenomination_TestMethod()
        {
            Dictionary<double, double> dictionary = new();
            dictionary.Add(0.4, 10);
            var denominationsResult = changeCalculation.CalculateChange(dictionary, 1000);
            Assert.IsNotNull(denominationsResult);
            Assert.AreEqual(ErrorCodes.NullReferenceException, denominationsResult.ErrorCode);
        }





    }
}