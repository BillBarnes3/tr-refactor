using BikeDistributor.Orders.Discounts;
using System.Collections.Generic;
using System.Linq;

namespace BikeDistributor.Orders
{
    public class OrderInfo
    {
        public double TaxRate { get; set; }
        public string Company { get; set; }
        public List<OrderLine> Lines { get; set; }
        public IEnumerable<QuantityAtPriceDiscount> Discounts { get; set; }

        public void ApplyQuantityDiscounts()
        {
            //Assumption here - one discount per order line, and only the best one gets counted.
            foreach (var orderLine in Lines)
            {
                var validDiscount = Discounts.FirstOrDefault(d =>
                    Lines.Count() >= d.QuantityRequired  && orderLine.BaseLinePrice >= d.QualifyingPrice);
                if (validDiscount != null)
                {
                    orderLine.AdjustedLinePrice = orderLine.BaseLinePrice * validDiscount.Multiplier;
                }
                else
                {
                    orderLine.AdjustedLinePrice = orderLine.BaseLinePrice;
                }
            }
        }

    }
}
