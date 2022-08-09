using Equiti.OrderManager.Business.Models;
using System;
using System.Collections.Generic;

namespace Equiti.OrderManager.Business.OrderManagement
{
    public interface IOrder
    {
        public IList<OrderItem> OrderItems { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDateTime { get; set; }
    }
}
