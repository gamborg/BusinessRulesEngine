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
    public class VideoTests
    {
        private readonly Video _product;
        private readonly IMediator _mediator;
        private readonly IDbContext _dbContext;

        public VideoTests()
        {
            _mediator = new MockMediator();
            _dbContext = new MockContext().GetDbContext("BookTests");
            _product = new Video(_mediator, _dbContext);
        }

        [TestMethod]
        public void ShouldContainThreeRules()
        {
            Assert.AreEqual(3, _product.Rules.Count);
        }

        [TestMethod]
        public void ShouldHaveRightHandlerName()
        {
            Assert.AreEqual("video", _product.HandlerName);
        }

        [TestMethod]
        public void RulesShouldBeCorrectType()
        {
            var firstRule = _product.Rules[0];
            var secondRule = _product.Rules[1];
            var thirdRule = _product.Rules[2];

            Assert.AreEqual(typeof(AddFirstAidVideo), firstRule.GetType());
            Assert.AreEqual(typeof(GeneratePackingSlipForShipping), secondRule.GetType());
            Assert.AreEqual(typeof(GenerateCommissionPaymentToAgent), thirdRule.GetType());

            Assert.AreEqual("shipping department", ((GeneratePackingSlipForShipping)secondRule).Department);
        }

    }
}
