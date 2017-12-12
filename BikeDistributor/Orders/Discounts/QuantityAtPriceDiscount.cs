using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Orders.Discounts
{
    public class QuantityAtPriceDiscount
    {
        public double QualifyingPrice { get; private set; }
        public int QuantityRequired { get; private set; }
        public double Multiplier { get; private set; }

        public QuantityAtPriceDiscount(double qualifyingPrice, int quantityRequired, double multiplier)
        {
            QualifyingPrice = qualifyingPrice;
            QuantityRequired = quantityRequired;
            Multiplier = multiplier;
        }
    }
}
