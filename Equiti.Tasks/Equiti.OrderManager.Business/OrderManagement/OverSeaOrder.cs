using Equiti.OrderManager.Business.Models;
using System;
using System.Collections.Generic;

namespace Equiti.OrderManager.Business.OrderManagement
{
    public class OverSeaOrder : IOrder
    {
        public IList<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Customer Customer { get; set; }
        public DateTime OrderDateTime { get; set; } = DateTime.Now;
    }
}
