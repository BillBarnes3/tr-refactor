using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BikeDistributor.Orders.Discounts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BikeDistributor.Test
{
    [TestClass]
    public class DiscountTests
    {
        [TestMethod]
        public void DiscountAppliesWhenOverThreshold()
        {
            var order = new Order("fooCompany", new List<QuantityAtPriceDiscount>()
            {
                new QuantityAtPriceDiscount(1000d, 5, 0.9d)
            });

            for (var i = 0; i < 5; i++)
            {
                order.AddLine(new Line(new Bike("Canyon", "Aeroad CF SLX 9.0", 3499), 1));
            }
            var receipt = order.Receipt();
            Assert.AreEqual(receipt, @"Order Receipt for fooCompany
	1 x Canyon Aeroad CF SLX 9.0 = $3,149.10
	1 x Canyon Aeroad CF SLX 9.0 = $3,149.10
	1 x Canyon Aeroad CF SLX 9.0 = $3,149.10
	1 x Canyon Aeroad CF SLX 9.0 = $3,149.10
	1 x Canyon Aeroad CF SLX 9.0 = $3,149.10
Sub-Total: $15,745.50
Tax: $1,141.55
Total: $16,887.05");
        }

        [TestMethod]
        public void DiscountDoesNotApplyIfNotOverThreshold()
        {
            var order = new Order("fooCompany", new List<QuantityAtPriceDiscount>()
            {
                new QuantityAtPriceDiscount(1000d, 5, 0.9d)
            });

            for (var i = 0; i < 4; i++)
            {
                order.AddLine(new Line(new Bike("Canyon", "Aeroad CF SLX 9.0", 3499), 1));
            }
            var receipt = order.Receipt();
            Assert.AreEqual(receipt, @"Order Receipt for fooCompany
	1 x Canyon Aeroad CF SLX 9.0 = $3,499.00
	1 x Canyon Aeroad CF SLX 9.0 = $3,499.00
	1 x Canyon Aeroad CF SLX 9.0 = $3,499.00
	1 x Canyon Aeroad CF SLX 9.0 = $3,499.00
Sub-Total: $13,996.00
Tax: $1,014.71
Total: $15,010.71");
        }

        [TestMethod]
        public void OnlyBestDiscountIsAppliedWhenMultipleQualify()
        {
            var order = new Order("fooCompany", new List<QuantityAtPriceDiscount>()
            {
                new QuantityAtPriceDiscount(2000d, 5, 0.9d),
                new QuantityAtPriceDiscount(1000d, 3, 0.8d)
            });

            for (var i = 0; i < 5; i++)
            {
                order.AddLine(new Line(new Bike("Canyon", "Aeroad CF SLX 9.0", 3499), 1));
            }
            var receipt = order.Receipt();
            Assert.AreEqual(receipt, @"Order Receipt for fooCompany
	1 x Canyon Aeroad CF SLX 9.0 = $3,149.10
	1 x Canyon Aeroad CF SLX 9.0 = $3,149.10
	1 x Canyon Aeroad CF SLX 9.0 = $3,149.10
	1 x Canyon Aeroad CF SLX 9.0 = $3,149.10
	1 x Canyon Aeroad CF SLX 9.0 = $3,149.10
Sub-Total: $15,745.50
Tax: $1,141.55
Total: $16,887.05");
        }

    }
}
