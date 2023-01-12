using CalculusFunctions.Contracts;
using CalculusFunctions;
using Common.Catalogs;

namespace POS_Test
{

    [TestClass]
    public class CalculateRightTest
    {
        readonly IChangeCalculation changeCalculation = ChangeCalculation.Instance;

        [TestMethod]
        public void CompleteRightCalculate_TestMethod()
        {
            var denominationsResult = changeCalculation.SetDenominations("USA");
            Dictionary<double, int> dictionary2 = new()
            {
                { 100, 0 },
                { 50, 1 },
                { 20, 0 },
                { 10, 0 },
                { 5, 0 },
                { 2, 0 },
                { 1, 0 },
                { 0.5, 0 },
                { 0.25, 0 },
                { 0.1, 0 },
                { 0.05, 0 },
                { 0.01, 0 }
            };
            Dictionary<double, double> dictionary = new()
            {
                { 10, 10 }
            };
            var changeResult = changeCalculation.CalculateChange(dictionary, 50);
            Assert.IsNotNull(changeResult);
            Assert.AreEqual(true, changeResult.DidSucceed);
            foreach (var val in changeResult.Value)
            {
                Assert.AreEqual(val,dictionary2.First(x => x.Key == val.Key));
            }
            
            Assert.IsNotNull(denominationsResult);
            Assert.AreEqual(true, denominationsResult.DidSucceed);

        }

        [TestMethod]
        public void RightCalculateWrongPaid_TestMethod()
        {
            var denominationsResult = changeCalculation.SetDenominations("USA");
            Dictionary<double, double> dictionary = new()
            {
                { 10, 10 }
            };
            var changeResult = changeCalculation.CalculateChange(dictionary, 500);
            Assert.IsNotNull(changeResult);
            Assert.AreEqual(ErrorCodes.InsufficientAmount, changeResult.ErrorCode);
            Assert.IsNotNull(denominationsResult);
            Assert.AreEqual(true, denominationsResult.DidSucceed);

        }
    }
}
