using BusinessRulesEngine.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessRulesEngine.Interfaces
{
    public interface IDbContext : IDisposable
    {
        DbSet<Membership> Memberships { get; set; }
        DbSet<PackingSlipLine> PackingSlipLines { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);

    }
}
