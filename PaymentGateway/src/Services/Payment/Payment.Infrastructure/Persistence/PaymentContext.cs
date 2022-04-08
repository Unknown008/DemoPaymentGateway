using Microsoft.EntityFrameworkCore;
using PaymentNs.Domain.Common;
using PaymentNs.Domain.Entities;

namespace PaymentNs.Infrastructure.Persistence
{
    public class PaymentContext : DbContext
    {
        public PaymentContext(DbContextOptions<PaymentContext> options) : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.Now;
                        entry.Entity.CreatedBy = "test";
                        entry.Entity.UpdatedOn = DateTime.Now;
                        entry.Entity.UpdatedBy = "test";
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedOn = DateTime.Now;
                        entry.Entity.UpdatedBy = "test";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}