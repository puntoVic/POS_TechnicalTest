using CalculusFunctions;
using CalculusFunctions.Contracts;
using Common.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Test
{
    [TestClass]
    public  class CalculateErrorsTest
    {
        readonly IChangeCalculation changeCalculation = ChangeCalculation.Instance;

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
            var denominationsResult = changeCalculation.CalculateChange(new Dictionary<double, double>(), 0);
            Assert.IsNotNull(denominationsResult);
            Assert.AreEqual(ErrorCodes.NullReferenceException, denominationsResult.ErrorCode);
        }



        [TestMethod]
        public void CalculateWithWrongValues_TestMethod()
        {
            changeCalculation.SetDenominations("USA");
            Dictionary<double, double> dictionary = new()
            {
                { 0.4, 10 }
            };
            var changeResult = changeCalculation.CalculateChange(dictionary, 1000);
            Assert.IsNotNull(changeResult);
            Assert.AreEqual(ErrorCodes.WrongDenomination, changeResult.ErrorCode);
        }
    }
}
