using System.Linq;
using System.Text;
using BikeDistributor.Orders;

namespace BikeDistributor.RecieptFormatters
{
    public class HtmlReceiptFormatter : IReceiptFormatter
    {
        public string GetReceiptForOrder(OrderInfo orderInfo)
        {
            var totalAmount = orderInfo.Lines.Sum(l => l.AdjustedLinePrice);
            var result = new StringBuilder($"<html><body><h1>Order Receipt for {orderInfo.Company}</h1>");
            if (orderInfo.Lines.Any())
            {
                result.Append("<ul>");
                foreach (var line in orderInfo.Lines)
                {
                    result.Append($"<li>{line.Quantity} x {line.Bike.Brand} {line.Bike.Model} = {line.AdjustedLinePrice:C}</li>");
                }
                result.Append("</ul>");
            }
            result.Append($"<h3>Sub-Total: {totalAmount:C}</h3>");
            var tax = totalAmount * orderInfo.TaxRate;
            result.Append($"<h3>Tax: {tax:C}</h3>");
            result.Append($"<h2>Total: {(totalAmount + tax):C}</h2>");
            result.Append("</body></html>");
            return result.ToString();
        }
    }
}
