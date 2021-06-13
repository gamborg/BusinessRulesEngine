using BusinessRulesEngine.Models;
using BusinessRulesEngine.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.Tests.Models
{
    [TestClass]
    public class ProductTests
    {
        [TestMethod]
        public void ProductProperties()
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
                    PropertyName = "ProductType",
                    PropertyType = typeof(string)
                }
            };

            var propertiesCreateDeviceCommand = typeof(Product).GetProperties();
            foreach (var property in propertiesCreateDeviceCommand)
            {
                var found = properties.Find(x => x.PropertyName == property.Name);
                Assert.IsNotNull(found);
                Assert.AreEqual(found.PropertyType, property.PropertyType);
            }

            Assert.AreEqual(propertiesCreateDeviceCommand.Length, properties.Count);
        }

        [TestMethod]
        public void CreateProduct()
        {
            var product = new Product
            {
                Name = "Test product name",
                ProductType = "Test product type"
            };

            Assert.AreEqual("Test product name", product.Name);
            Assert.AreEqual("Test product type", product.ProductType);
        }

    }
}
