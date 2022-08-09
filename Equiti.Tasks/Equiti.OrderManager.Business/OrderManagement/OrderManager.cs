using Equiti.OrderManager.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Equiti.OrderManager.Business.OrderManagement
{
    public interface IOrderManager
    {
        List<Customer> CustomersWithOrders(IEnumerable<IOrder> orders, DateTime fromDate);
    }

    public class OrderManager : IOrderManager
    {
        public List<Customer> CustomersWithOrders(IEnumerable<IOrder> orders, DateTime fromDate)
        {

            return orders
                .Where(x => x.OrderDateTime.Date >= fromDate)
                .Where(x => x.Customer != null)
                .Select(x => x.Customer)
                .DistinctBy(x => x.Id)
                .ToList();
        }
    }
}
