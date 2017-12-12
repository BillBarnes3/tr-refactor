using BikeDistributor.Orders;
using BikeDistributor.RecieptFormatters;
using System.Collections.Generic;
using System.Linq;
using BikeDistributor.Orders.Discounts;

namespace BikeDistributor
{
    public class Order
    {
        private const double TaxRate = .0725d;
        private readonly IList<Line> _lines = new List<Line>();

        private readonly IEnumerable<QuantityAtPriceDiscount> _discounts;
        private static IEnumerable<QuantityAtPriceDiscount> defaultDiscounts = new List<QuantityAtPriceDiscount>()
        {
            new QuantityAtPriceDiscount(Bike.OneThousand, 20, .9d),
            new QuantityAtPriceDiscount(Bike.TwoThousand, 10, .8d),
            new QuantityAtPriceDiscount(Bike.FiveThousand, 5, .8d)
        };


        public Order(string company) : this(company, defaultDiscounts)
        {
        }

        public Order(string company, IEnumerable<QuantityAtPriceDiscount> discounts)
        {
            Company = company;
            _discounts = discounts;

        }

        public string Company { get; private set; }

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
                TaxRate = TaxRate,
                Discounts = _discounts
            };

            orderInfo.ApplyQuantityDiscounts();
            return formatterType.GetReceiptForOrder(orderInfo);
        }

    }
}
