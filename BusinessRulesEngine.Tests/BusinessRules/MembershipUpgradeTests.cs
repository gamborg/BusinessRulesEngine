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
    public class MembershipUpgradeTests
    {
        private readonly MembershipUpgrade _product;
        private readonly IMediator _mediator;
        private readonly IDbContext _dbContext;
        private readonly IDateTime _dateTime;
        public MembershipUpgradeTests()
        {
            _mediator = new MockMediator();
            _dbContext = new MockContext().GetDbContext("Membership");
            _dateTime = new MockDateTime(DateTime.Now);
            _product = new MembershipUpgrade(_mediator, _dbContext, _dateTime);
        }

        [TestMethod]
        public void ShouldContainFourRules()
        {
            Assert.AreEqual(4, _product.Rules.Count);
        }

        [TestMethod]
        public void RulesShouldBeCorrectType()
        {
            var firstRule = _product.Rules[0];
            var secondRule = _product.Rules[1];
            var thirdRule = _product.Rules[2];
            var fourthRule = _product.Rules[3];

            Assert.AreEqual(typeof(ActivateMembership), firstRule.GetType());
            Assert.AreEqual(typeof(MembershipEmailOwnerActivation), secondRule.GetType());
            Assert.AreEqual(typeof(UpgradeMembership), thirdRule.GetType());
            Assert.AreEqual(typeof(MembershipEmailOwnerUpgraded), fourthRule.GetType());
        }

        [TestMethod]
        public void ShouldUpdateMembershipInDatabase()
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
            Assert.AreEqual(_dateTime.Now, updatedRecord.Activated);
            Assert.AreEqual("upgraded", updatedRecord.MembershipType);

            var notification = ((MockMediator)_mediator).LatestNotification;
            Assert.AreEqual(typeof(SendUpgradeEmailNotification), notification.GetType());
            Assert.AreEqual("Hans Hansen", ((SendUpgradeEmailNotification)notification).RecipientName);
            Assert.AreEqual("hans@hansen.dk", ((SendUpgradeEmailNotification)notification).RecipientEmail);
        }

    }
}
