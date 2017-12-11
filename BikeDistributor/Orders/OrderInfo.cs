using System.Collections.Generic;

namespace BikeDistributor.Orders
{
    public class OrderInfo
    {
        public double TaxRate { get; set; }
        public string Company { get; set; }
        public IEnumerable<Line> Lines { get; set; }
    }
}
