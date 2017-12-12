using BikeDistributor.Orders;
using System;
using System.Linq;
using System.Text;

namespace BikeDistributor.RecieptFormatters
{
    public class TextReceiptFormatter : IReceiptFormatter
    {
        public string GetReceiptForOrder(OrderInfo orderInfo)
        {
            var totalAmount = orderInfo.Lines.Sum(l => l.AdjustedLinePrice);
            var result = new StringBuilder($"Order Receipt for {orderInfo.Company}{Environment.NewLine}");
            foreach (var line in orderInfo.Lines)
            {
                result.AppendLine($"\t{line.Quantity} x {line.Bike.Brand} {line.Bike.Model} = {line.AdjustedLinePrice:C}");
            }

            result.AppendLine($"Sub-Total: {totalAmount:C}");
            var tax = totalAmount * orderInfo.TaxRate;
            result.AppendLine($"Tax: {tax:C}");
            result.Append($"Total: {(totalAmount + tax):C}");

            return result.ToString();
        }
    }
}
