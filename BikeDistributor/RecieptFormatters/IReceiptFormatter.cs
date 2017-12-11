using BikeDistributor.Orders;

namespace BikeDistributor.RecieptFormatters
{
    public interface IReceiptFormatter
    {
        string GetReceiptForOrder(OrderInfo orderInfo);
    }
}
