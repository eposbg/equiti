using Equiti.OrderManager.Business.OrderManagement;
using System;
using System.Collections.Generic;
using Xunit;

namespace Equiti.OrderManager.Business.Tests.OrderManagement
{
    public class OrderManagerTests
    {
        [Fact]
        public void OrderManager_CustomersWithOrders_3OrdersWithOneInTheLastYearReturns1Customer()
        {
            // Arrange
            var orders = new List<IOrder>();
            var customer1 = new Models.Customer { Id = 1, Name = "Customer 1" };
            var customer2 = new Models.Customer { Id = 2, Name = "Customer 2" };
            var customer3 = new Models.Customer { Id = 3, Name = "Customer 3" };
            orders.Add(new DomesticOrder { Customer = customer1, OrderDateTime = DateTime.Now.AddDays(-30) });
            orders.Add(new DomesticOrder { Customer = customer2, OrderDateTime = DateTime.Now.AddMonths(-30) });
            orders.Add(new OverSeaOrder { Customer = customer3, OrderDateTime = DateTime.Now.AddYears(-1).AddDays(-1) });


            var orderManager = new Business.OrderManagement.OrderManager();
            var fromDate = DateTime.Now.AddYears(-1).Date;

            // Act
            var customers = orderManager.CustomersWithOrders(orders, fromDate);

            //Assert
            Assert.NotNull(customers);
            Assert.Single(customers);
        }

        [Fact]
        public void OrderManager_CustomersWithOrders_NoOrdersShouldReturnEmptyListWithCustomers()
        {
            // Arrange
            var orders = new List<IOrder>();

            var orderManager = new Business.OrderManagement.OrderManager();
            var fromDate = DateTime.Now.AddYears(-1).Date;

            // Act
            var customers = orderManager.CustomersWithOrders(orders, fromDate);

            //Assert
            Assert.NotNull(customers);
            Assert.Empty(customers);
        }

        [Fact]
        public void OrderManager_CustomersWithOrders_OrdersWithNoCustomersShouldBeHandled()
        {
            // Arrange
            var orders = new List<IOrder>();
            var customer1 = new Models.Customer { Id = 1, Name = "Customer 1" };

            orders.Add(new DomesticOrder { Customer = null, OrderDateTime = DateTime.Now.AddDays(-30) });
            orders.Add(new DomesticOrder { Customer = customer1, OrderDateTime = DateTime.Now.AddDays(-30) });

            var orderManager = new Business.OrderManagement.OrderManager();
            var fromDate = DateTime.Now.AddYears(-1).Date;

            // Act
            var customers = orderManager.CustomersWithOrders(orders, fromDate);

            //Assert
            Assert.NotNull(customers);
            Assert.Single(customers);
        }

        [Fact]
        public void OrderManager_CustomersWithOrders_OrdersWithNoOrderDateShouldBeHandled()
        {
            // Arrange
            var orders = new List<IOrder>();
            var customer1 = new Models.Customer { Id = 1, Name = "Customer 1" };

            orders.Add(new DomesticOrder { Customer = customer1 });
            orders.Add(new DomesticOrder { Customer = customer1, OrderDateTime = DateTime.Now.AddDays(-30) });

            var orderManager = new Business.OrderManagement.OrderManager();
            var fromDate = DateTime.Now.AddYears(-1).Date;

            // Act
            var customers = orderManager.CustomersWithOrders(orders, fromDate);

            //Assert
            Assert.NotNull(customers);
            Assert.Single(customers);
        }
    }
}
