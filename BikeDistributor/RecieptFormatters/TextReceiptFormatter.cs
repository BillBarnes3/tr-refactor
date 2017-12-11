using BikeDistributor.Orders;
using System;
using System.Text;

namespace BikeDistributor.RecieptFormatters
{
    public class TextReceiptFormatter : IReceiptFormatter
    {
        public string GetReceiptForOrder(OrderInfo orderInfo)
        {
            var totalAmount = 0d;
            var result = new StringBuilder(string.Format("Order Receipt for {0}{1}", orderInfo.Company, Environment.NewLine));
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
                result.AppendLine(string.Format("\t{0} x {1} {2} = {3}", line.Quantity, line.Bike.Brand, line.Bike.Model, thisAmount.ToString("C")));
                totalAmount += thisAmount;
            }
            result.AppendLine(string.Format("Sub-Total: {0}", totalAmount.ToString("C")));
            var tax = totalAmount * orderInfo.TaxRate;
            result.AppendLine(string.Format("Tax: {0}", tax.ToString("C")));
            result.Append(string.Format("Total: {0}", (totalAmount + tax).ToString("C")));

            return result.ToString();
        }
    }
}
