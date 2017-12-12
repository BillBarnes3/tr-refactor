using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Orders.Discounts
{
    public class QuantityAtPriceDiscount
    {
        public double QualifyingPrice { get; }
        public int QuantityRequired { get; }
        public double Multiplier { get; }

        public QuantityAtPriceDiscount(double qualifyingPrice, int quantityRequired, double multiplier)
        {
            QualifyingPrice = qualifyingPrice;
            QuantityRequired = quantityRequired;
            Multiplier = multiplier;
        }
    }
}
