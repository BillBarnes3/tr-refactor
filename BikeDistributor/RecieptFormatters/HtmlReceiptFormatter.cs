using System.Linq;
using System.Text;
using BikeDistributor.Orders;

namespace BikeDistributor.RecieptFormatters
{
    public class HtmlReceiptFormatter : IReceiptFormatter
    {
        public string GetReceiptForOrder(OrderInfo orderInfo)
        {
            var totalAmount = 0d;
            var result = new StringBuilder(string.Format("<html><body><h1>Order Receipt for {0}</h1>", orderInfo.Company));
            if (orderInfo.Lines.Any())
            {
                result.Append("<ul>");
                foreach (var line in orderInfo.Lines)
                {
                    var thisAmount = 0d;
                    switch (line.Bike.Price)
                    {
                        case Bike.OneThousand:
                            if (line.Quantity >= 20)
                                thisAmount += line.Quantity * line.Bike.Price * .9d;
                            else
                                thisAmount += line.Quantity * line.Bike.Price;
                            break;
                        case Bike.TwoThousand:
                            if (line.Quantity >= 10)
                                thisAmount += line.Quantity * line.Bike.Price * .8d;
                            else
                                thisAmount += line.Quantity * line.Bike.Price;
                            break;
                        case Bike.FiveThousand:
                            if (line.Quantity >= 5)
                                thisAmount += line.Quantity * line.Bike.Price * .8d;
                            else
                                thisAmount += line.Quantity * line.Bike.Price;
                            break;
                    }
                    result.Append(string.Format("<li>{0} x {1} {2} = {3}</li>", line.Quantity, line.Bike.Brand, line.Bike.Model, thisAmount.ToString("C")));
                    totalAmount += thisAmount;
                }
                result.Append("</ul>");
            }
            result.Append(string.Format("<h3>Sub-Total: {0}</h3>", totalAmount.ToString("C")));
            var tax = totalAmount * orderInfo.TaxRate;
            result.Append(string.Format("<h3>Tax: {0}</h3>", tax.ToString("C")));
            result.Append(string.Format("<h2>Total: {0}</h2>", (totalAmount + tax).ToString("C")));
            result.Append("</body></html>");
            return result.ToString();
        }
    }
}
