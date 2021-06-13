using BusinessRulesEngine.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessRulesEngine.Data
{
    public class BusinessRulesEngineDbContext : DbContext, IDbContext
    {
        public BusinessRulesEngineDbContext(DbContextOptions<BusinessRulesEngineDbContext> options) : base(options)
        {
        }

        public DbSet<Membership> Memberships { get; set; }
        public DbSet<PackingSlipLine> PackingSlipLines { get; set; }
    }
}
