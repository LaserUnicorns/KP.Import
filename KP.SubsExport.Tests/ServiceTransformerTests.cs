using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KP.SubsExport.Tests
{
    [TestClass]
    public class ServiceTransformerTests
    {
        private DbReceiptItem Item(
            ImasServiceCode serviceCode, decimal amount, bool odn = false)
        {
            return new DbReceiptItem
            {
                AccountNumber = "461702100",
                ServiceCode = serviceCode,
                Amount = amount,
                Flat = "",
                RegisteredCount = 2,
                ODN = odn ? "101" : ""
            };
        }

        private ReceiptItemFull ri(string serviceCode, decimal amount)
        {
            return new ReceiptItemFull
            {
                ServiceCode = serviceCode,
                Amount = amount,
            };
        }

        [TestMethod]
        public void Map()
        {
            var snilsMapFactory = new SnilsMapFactory();
            var serviceCodeMapFactory = new ServiceCodeMapFactory();
            var mapper = new ServiceTransformer(
                snilsMapFactory, serviceCodeMapFactory
                );

            var dbList = new List<DbReceiptItem>
            {
                Item(ImasServiceCode.Repair, 535.81m),
                Item(ImasServiceCode.Heating, 1682.39m),
                Item(ImasServiceCode.ColdWater, 167.48m),
                Item(ImasServiceCode.ColdWater, -8.89m, true),
                Item(ImasServiceCode.Gas, 121.5m),
                Item(ImasServiceCode.Garbage, 119.66m),
                Item(ImasServiceCode.Electricity, 18.12m),
                Item(ImasServiceCode.BankFee, 79.55m),
                Item(ImasServiceCode.ColdWaterSewerage, 111.37m),
                Item(ImasServiceCode.ColdWaterSewerage, -5.91m, true),
                Item(ImasServiceCode.HotWaterSewerage, 17.76m),
                Item(ImasServiceCode.HotWaterSewerage, 6.92m, true),
                Item(ImasServiceCode.HotWater, 131.16m),
                Item(ImasServiceCode.HotWater, 51.11m, true),
                Item(ImasServiceCode.Domophone, 30),
            };

            var oldExpected = new List<ReceiptItemFull>
            {
                ri("01", 535.81m),
                ri("10", 1682.39m),
                ri("12", 167.48m),
                ri("18", -8.89m),
                ri("22", 121.5m),
                ri("05", 119.66m),
                ri("06", 18.12m),
                ri("01", 79.55m),
                ri("13", 111.37m),
                ri("20", -5.91m),
                ri("14", 17.76m),
                ri("20", 6.92m),
                ri("11", 131.16m),
                ri("19", 51.11m),
                ri("01", 30),
            };

            var newExpected = new List<ReceiptItemFull>
            {
                ri("01", 535.81m), // repair
                ri("10", 1682.39m), // heating
                ri("12", 158.59m), // cold water
                ri("22", 121.5m), // gas
                ri("03", 119.66m), // garbage
                ri("06", 18.12m), // electricity
                ri("13", 105.46m), // cold water sew
                ri("14", 24.68m), // hot water sew
                ri("11", 182.27m), // hot water
            };

            var actual = mapper.Map(dbList);

            //Assert.AreEqual(oldExpected.Count, actual.Count);
            //CollectionAssert.AreEqual(oldExpected, actual, 
            //    new ReceiptItemFullComparer());

            Assert.AreEqual(newExpected.Count, actual.Count);
            CollectionAssert.AreEqual(newExpected, actual,
                new ReceiptItemFullComparer());
        }
    }

    class ReceiptItemFullComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            var xVal = (ReceiptItemFull) x;
            var yVal = (ReceiptItemFull) y;
            var xKey = $"{xVal.ServiceCode}-{xVal.Amount}";
            var yKey = $"{yVal.ServiceCode}-{yVal.Amount}";

            return string.CompareOrdinal(xKey, yKey);
        }
    }
}
