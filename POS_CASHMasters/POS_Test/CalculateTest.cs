using CalculusFunctions;
using CalculusFunctions.Contracts;
using Common.Catalogs;

namespace POS_Test
{
    [TestClass]
    public class CalculateTest
    {
        readonly IChangeCalculation changeCalculation = ChangeCalculation.Instance;
        

        [TestMethod]
        public void SetDenomination_TestMethod()
        {
            var denominationsResult = changeCalculation.SetDenominations("USA");
            Assert.IsNotNull(denominationsResult);
            Assert.AreEqual(true, denominationsResult.DidSucceed);

        }

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
        public void CalculateNullDictionary_TestMethod()
        {
            var denominationsResult = changeCalculation.CalculateChange(null, 0);
            Assert.IsNotNull(denominationsResult);
            Assert.AreEqual(ErrorCodes.NullReferenceException, denominationsResult.ErrorCode);

        }

        [TestMethod]
        public void CalculateEmptyDictionary_TestMethod()
        {
            var denominationsResult = changeCalculation.CalculateChange(new Dictionary<double,double>(), 0);
            Assert.IsNotNull(denominationsResult);
            Assert.AreEqual(ErrorCodes.NullReferenceException, denominationsResult.ErrorCode);
        }

        [TestMethod]
        public void CalculateWithWrongValues_TestMethod()
        {
            var denominationsResult1 = changeCalculation.SetDenominations("USA");
            Dictionary<double, double> dictionary = new();
            dictionary.Add(0.4, 10);
            var denominationsResult = changeCalculation.CalculateChange(dictionary, 1000);
            Assert.IsNotNull(denominationsResult);
            Assert.AreEqual(ErrorCodes.WrongDenomination, denominationsResult.ErrorCode);
        }

        


    }
}