using BusinessRulesEngine.Data;
using BusinessRulesEngine.Interfaces;
using BusinessRulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessRulesEngine.Handlers.BusinessRules
{
    public class GeneratePackingSlipForShipping : IRule
    {
        private readonly string _department = "";
        private readonly IDbContext _dbContext;

        public string Department => _department;
        public GeneratePackingSlipForShipping(string department, IDbContext dbContext)
        {
            _department = department;
            _dbContext = dbContext;
        }

        public Task Apply(Payment payment)
        {
            for (var i = 0; i < payment.Products.Count; i++)
            {
                var product = payment.Products[i];
                var line = new PackingSlipLine
                {
                    Id = Guid.NewGuid(),
                    Department = _department,
                    Order = i,
                    ProductName = product.Name,
                    ProductType = product.ProductType
                };
                _dbContext.PackingSlipLines.Add(line);
            }
            _dbContext.SaveChangesAsync(CancellationToken.None);

            return Task.CompletedTask;
        }
    }
}
