using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessRulesEngine.Handlers.BusinessRules
{
    public class ActivateMembership : IRule
    {
        private readonly IDbContext _dbContext;
        private readonly IDateTime _dateTime;

        public ActivateMembership(IDbContext dbContext, IDateTime dateTime)
        {
            _dbContext = dbContext;
            _dateTime = dateTime;
        }

        public Task Apply(Payment payment)
        {
            var membership = _dbContext.Memberships.Where(x => x.Email == payment.Customer.Email).FirstOrDefault();
            if (membership != null)
            {
                membership.Active = true;
                membership.Activated = _dateTime.Now;
                _dbContext.Memberships.Update(membership);
                _dbContext.SaveChangesAsync(CancellationToken.None);
            }
            else
            {
                membership = new Data.Membership
                {
                    Created = _dateTime.Now,
                    Activated = _dateTime.Now,
                    Active = true,
                    Email = payment.Customer.Email,
                    Id = Guid.NewGuid(),
                    MembershipType = "normal"
                };
                _dbContext.Memberships.Add(membership);
                _dbContext.SaveChangesAsync(CancellationToken.None);
            }

            return Task.CompletedTask;
        }
    }
}
