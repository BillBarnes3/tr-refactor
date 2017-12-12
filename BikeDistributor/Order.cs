using BikeDistributor.Orders;
using BikeDistributor.RecieptFormatters;
using System.Collections.Generic;
using System.Linq;
using BikeDistributor.Orders.Discounts;

namespace BikeDistributor
{
    public class Order
    {
        private const double DefaultTaxRate = .0725d;
        private readonly IList<Line> _lines = new List<Line>();

        private readonly IEnumerable<QuantityAtPriceDiscount> _discounts;
        private static readonly IEnumerable<QuantityAtPriceDiscount> defaultDiscounts = new List<QuantityAtPriceDiscount>()
        {
            new QuantityAtPriceDiscount(Bike.OneThousand, 20, .9d),
            new QuantityAtPriceDiscount(Bike.TwoThousand, 10, .8d),
            new QuantityAtPriceDiscount(Bike.FiveThousand, 5, .8d)
        };

        /// <summary>
        /// Creates a new order with the default discounting structure in place.
        /// </summary>
        /// <param name="company">The name of the company on the order.</param>
        public Order(string company) : this(company, DefaultTaxRate, defaultDiscounts)
        {
        }

        /// <summary>
        /// Creates a new order with the default discounting structure in place.
        /// </summary>
        /// <param name="company">The name of the company on the order.</param>
        /// <param name="taxRate">The tax rate to use for the order</param>
        public Order(string company, double taxRate) : this(company, taxRate, defaultDiscounts)
        {
        }

        /// <summary>
        /// Creates a new order with specified quantity/price discounting.
        /// </summary>
        /// <param name="company">The name of the company on the order.</param>
        /// <param name="discounts">A Collection of price/discount objects that sets thresholds for discounting (only the best discount is used)</param>
        public Order(string company, IEnumerable<QuantityAtPriceDiscount> discounts) : this(company, DefaultTaxRate, discounts)
        {
        }

        /// <summary>
        /// Creates a new order with specified quantity/price discounting.
        /// </summary>
        /// <param name="company">The name of the company on the order.</param>
        /// <param name="taxRate">The tax rate to use for the order</param>
        /// <param name="discounts">A Collection of price/discount objects that sets thresholds for discounting (only the best discount is used)</param>
        public Order(string company, double taxRate, IEnumerable<QuantityAtPriceDiscount> discounts)
        {
            Company = company;
            _discounts = discounts;
        }
        

        public string Company { get; }

        public void AddLine(Line line)
        {
            _lines.Add(line);
        }

        public string Receipt()
        {
            return GetReceiptString(new TextReceiptFormatter());
        }

        public string HtmlReceipt()
        {
            return GetReceiptString(new HtmlReceiptFormatter());
        }

        private string GetReceiptString(IReceiptFormatter formatterType)
        {
            var orderInfo = new OrderInfo
            {
                Company = Company,
                Lines = _lines.Select(line => new OrderLine(line)).ToList(),
                TaxRate = DefaultTaxRate,
                Discounts = _discounts
            };

            orderInfo.ApplyQuantityDiscounts();
            return formatterType.GetReceiptForOrder(orderInfo);
        }

    }
}
