using CalculusFunctions.Contracts;
using CalculusFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Test
{
    [TestClass]
    public class DenominationRightTest
    {
        readonly IChangeCalculation changeCalculation = ChangeCalculation.Instance;

        [TestMethod]
        public void SetDenomination_TestMethod()
        {
            var denominationsResult = changeCalculation.SetDenominations("USA");
            Assert.IsNotNull(denominationsResult);
            Assert.AreEqual(true, denominationsResult.DidSucceed);

        }
    }
}
