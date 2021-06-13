using BusinessRulesEngine.Handlers.BusinessRules;
using BusinessRulesEngine.Handlers.PaymentTypes;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Tests.Utils;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.Tests.BusinessRules
{
    [TestClass]
    public class BookTests
    {
        private readonly Book _product;
        private readonly IMediator _mediator;
        private readonly IDbContext _dbContext;

        public BookTests()
        {
            _mediator = new MockMediator();
            _dbContext = new MockContext().GetDbContext("BookTests");
            _product = new Book(_mediator, _dbContext);
        }

        [TestMethod]
        public void ShouldContainThreeRules()
        {
            Assert.AreEqual(3, _product.Rules.Count);
        }

        [TestMethod]
        public void ShouldHaveRightHandlerName()
        {
            Assert.AreEqual("book", _product.HandlerName);
        }

        [TestMethod]
        public void RulesShouldBeCorrectType()
        {
            var firstRule = _product.Rules[0];
            var secondRule = _product.Rules[1];
            var thirdRule = _product.Rules[2];

            Assert.AreEqual(typeof(GeneratePackingSlipForShipping), firstRule.GetType());
            Assert.AreEqual(typeof(GenerateCommissionPaymentToAgent), secondRule.GetType());
            Assert.AreEqual(typeof(GeneratePackingSlipForShipping), thirdRule.GetType());

            Assert.AreEqual("shipping department", ((GeneratePackingSlipForShipping)firstRule).Department);
            Assert.AreEqual("royalty department", ((GeneratePackingSlipForShipping)thirdRule).Department);
        }
    }
}
