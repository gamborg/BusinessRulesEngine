using BusinessRulesEngine.Models;
using BusinessRulesEngine.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.Tests.Models
{
    [TestClass]
    public class CustomerTest
    {
        [TestMethod]
        public void CustomerProperties()
        {
            var properties = new List<PropertyTestObject>()
            {
                new PropertyTestObject
                {
                    PropertyName = "Name",
                    PropertyType = typeof(string)
                },
                new PropertyTestObject
                {
                    PropertyName = "Email",
                    PropertyType = typeof(string)                    
                }
            };

            var propertiesCreateDeviceCommand = typeof(Customer).GetProperties();
            foreach (var property in propertiesCreateDeviceCommand)
            {
                var found = properties.Find(x => x.PropertyName == property.Name);
                Assert.IsNotNull(found);
                Assert.AreEqual(found.PropertyType, property.PropertyType);
            }

            Assert.AreEqual(propertiesCreateDeviceCommand.Length, properties.Count);
        }

        [TestMethod]
        public void CreateCustomer()
        {
            var customer = new Customer
            {
                Name = "Hans Hansen",
                Email = "hans@hansen.dk"
            };

            Assert.AreEqual("Hans Hansen", customer.Name);
            Assert.AreEqual("hans@hansen.dk", customer.Email);
        }
    }
}
