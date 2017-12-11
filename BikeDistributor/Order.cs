using BikeDistributor.Orders;
using BikeDistributor.RecieptFormatters;
using System.Collections.Generic;

namespace BikeDistributor
{
    public class Order
    {
        private const double TaxRate = .0725d;
        private readonly IList<Line> _lines = new List<Line>();

        public Order(string company)
        {
            Company = company;
        }

        public string Company { get; private set; }

        public void AddLine(Line line)
        {
            _lines.Add(line);
        }

        public string Receipt()
        {
            var orderInfo = new OrderInfo
            {
                Company = Company,
                Lines = _lines,
                TaxRate = TaxRate
            };

            var receiptFormatter = new TextReceiptFormatter();
            return receiptFormatter.GetReceiptForOrder(orderInfo);
        }

        public string HtmlReceipt()
        {
            var orderInfo = new OrderInfo
            {
                Company = Company,
                Lines = _lines,
                TaxRate = TaxRate
            };

            var receiptFormatter = new HtmlReceiptFormatter();
            return receiptFormatter.GetReceiptForOrder(orderInfo);
        }

    }
}
