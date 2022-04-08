using BankSimulator.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSimulator.DBContext
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

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
