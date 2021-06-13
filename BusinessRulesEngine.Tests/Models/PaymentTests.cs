using BusinessRulesEngine.Models;
using BusinessRulesEngine.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.Tests.Models
{
    [TestClass]
    public class PaymentTests
    {
        [TestMethod]
        public void PaymentProperties()
        {
            var properties = new List<PropertyTestObject>()
            {
                new PropertyTestObject
                {
                    PropertyName = "OrderId",
                    PropertyType = typeof(Guid)
                },
                new PropertyTestObject
                {
                    PropertyName = "Products",
                    PropertyType = typeof(List<Product>)
                },
                new PropertyTestObject
                {
                    PropertyName = "Agent",
                    PropertyType = typeof(Guid)
                },
                new PropertyTestObject
                {
                    PropertyName = "Customer",
                    PropertyType = typeof(Customer)
                }
            };

            var propertiesCreateDeviceCommand = typeof(Payment).GetProperties();
            foreach (var property in propertiesCreateDeviceCommand)
            {
                var found = properties.Find(x => x.PropertyName == property.Name);
                Assert.IsNotNull(found);
                Assert.AreEqual(found.PropertyType, property.PropertyType);
            }

            Assert.AreEqual(propertiesCreateDeviceCommand.Length, properties.Count);
        }

        [TestMethod]
        public void CreatePayment()
        {
            var orderId = Guid.NewGuid();
            var agentId = Guid.NewGuid();

            var payment = new Payment
            {
                OrderId = orderId,
                Products = new List<Product>()
                {
                    new Product
                    {
                        Name = "Test product",
                        ProductType = "test-product"
                    }
                },
                Agent = agentId,
                Customer = new Customer
                {
                    Email = "hans@hansen.dk",
                    Name = "Hans Hansen"
                }
            };

            Assert.AreEqual(orderId, payment.OrderId);
            Assert.AreEqual(1, payment.Products.Count);
            Assert.AreEqual("Test product", payment.Products[0].Name);
            Assert.AreEqual("test-product", payment.Products[0].ProductType);
            Assert.AreEqual(agentId, payment.Agent);

            Assert.IsNotNull(payment.Customer);
            Assert.AreEqual("hans@hansen.dk", payment.Customer.Email);
            Assert.AreEqual("Hans Hansen", payment.Customer.Name);
        }
    }
}
