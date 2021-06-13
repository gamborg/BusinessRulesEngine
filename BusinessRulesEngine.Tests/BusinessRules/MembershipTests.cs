using BusinessRulesEngine.Handlers.BusinessRules;
using BusinessRulesEngine.Handlers.PaymentTypes;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.MediatR.Notifications.Membership;
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
    public class MembershipTests
    {
        private readonly Membership _product;
        private readonly IMediator _mediator;
        private readonly IDbContext _dbContext;
        private readonly IDateTime _dateTime;

        public MembershipTests()
        {
            _mediator = new MockMediator();
            _dbContext = new MockContext().GetDbContext("Membership");
            _dateTime = new MockDateTime(DateTime.Now);
            _product = new Membership(_mediator, _dbContext, _dateTime);
        }

        [TestMethod]
        public void ShouldContainTwoRules()
        {
            Assert.AreEqual(2, _product.Rules.Count);
        }

        [TestMethod]
        public void RulesShouldBeCorrectType()
        {
            var firstRule = _product.Rules[0];
            var secondRule = _product.Rules[1];

            Assert.AreEqual(typeof(ActivateMembership), firstRule.GetType());
            Assert.AreEqual(typeof(MembershipEmailOwnerActivation), secondRule.GetType());
        }

        [TestMethod]
        public void ShouldActivateMembership()
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

            var updatedRecord = _dbContext.Memberships.FirstOrDefault(x => x.Email == payment.Customer.Email);

            Assert.IsNotNull(updatedRecord);
            Assert.AreEqual(_dateTime.Now, updatedRecord.Created);
            Assert.AreEqual(true, updatedRecord.Active);

            var notification = ((MockMediator)_mediator).LatestNotification;
            Assert.AreEqual(typeof(SendActivationEmailNotification), notification.GetType());
            Assert.AreEqual("Hans Hansen", ((SendActivationEmailNotification)notification).RecipientName);
            Assert.AreEqual("hans@hansen.dk", ((SendActivationEmailNotification)notification).RecipientEmail);
        }

    }
}
