using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor.Orders
{
    public class OrderLine : Line
    {
        public OrderLine(Line line) : base(line.Bike, line.Quantity)
        {
        }

        public double BaseLinePrice => Bike.Price;
        public double AdjustedLinePrice { get; set; }
    }
}
