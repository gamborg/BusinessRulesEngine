using BusinessRulesEngine.Handlers.BusinessRules;
using BusinessRulesEngine.Handlers.PaymentTypes;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;
using BusinessRulesEngine.Tests.Utils;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessRulesEngine.Tests.BusinessRules
{
    [TestClass]
    public class PhysicalProductTests
    {
        private readonly PhysicalProduct _product;
        private readonly IMediator _mediator;
        private readonly IDbContext _dbContext;

        public PhysicalProductTests()
        {
            _mediator = new MockMediator();
            _dbContext = new MockContext().GetDbContext("PhysicalProductTests");
            _product = new PhysicalProduct(_mediator, _dbContext);
        }

        [TestMethod]
        public void ShouldContainTwoRules()
        {
            Assert.AreEqual(2, _product.Rules.Count);
        }

        [TestMethod]
        public void ShouldHaveRightHandlerName()
        {
            Assert.AreEqual("physical", _product.HandlerName);
        }

        [TestMethod]
        public void RulesShouldBeCorrectType()
        {
            var firstRule = _product.Rules[0];
            var secondRule = _product.Rules[1];

            Assert.AreEqual(typeof(GeneratePackingSlipForShipping), firstRule.GetType());
            Assert.AreEqual(typeof(GenerateCommissionPaymentToAgent), secondRule.GetType());

            Assert.AreEqual("shipping department", ((GeneratePackingSlipForShipping)firstRule).Department);
        }

        [TestMethod]
        public void PackingSlipForShippingShouldBeGenerated()
        {
            var payment = new Payment()
            {
                Agent = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                Customer = new Customer
                {
                    Name = "Hans Hansen",
                    Email = "hans@hansen.dk"
                },
                Products = new List<Product>()
                {
                    new Product
                    {
                        Name = "Test product",
                        ProductType = "test-type"
                    }
                }
            };

            _product.Execute(payment);

            var numberOfLines = _dbContext.PackingSlipLines.Where(x => x.Department == "shipping department").Count();
            var line = _dbContext.PackingSlipLines.FirstOrDefault(x => x.Department == "shipping department");
            Assert.AreEqual(1, numberOfLines);
            Assert.AreEqual("Test product", line.ProductName);
            Assert.AreEqual("test-type", line.ProductType);
        }
    }
}
