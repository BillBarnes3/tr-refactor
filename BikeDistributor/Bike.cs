using System;

namespace BikeDistributor
{
    public class Bike
    {
        [Obsolete("Using Constant values is no longer necessary for use with discounting.  Use QuantityAtPriceDiscount instead.")]
        public const int OneThousand = 1000;

        [Obsolete("Using Constant values is no longer necessary for use with discounting.  Use QuantityAtPriceDiscount instead.")]
        public const int TwoThousand = 2000;

        [Obsolete("Using Constant values is no longer necessary for use with discounting.  Use QuantityAtPriceDiscount instead.")]
        public const int FiveThousand = 5000;
    
        public Bike(string brand, string model, int price)
        {
            Brand = brand;
            Model = model;
            Price = price;
        }

        public string Brand { get; private set; }
        public string Model { get; private set; }
        public int Price { get; set; }
    }
}
