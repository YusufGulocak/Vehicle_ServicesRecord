using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace App.Repositories.Interceptors
{
    public class AuditDbContextInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
            {
                if (entityEntry.Entity is not IAuditEntity auditEntity) continue;

                if (entityEntry.State == EntityState.Added)
                {
                    auditEntity.Created = DateTime.Now;
                    entityEntry.Property(nameof(IAuditEntity.Updated)).IsModified = false;
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    auditEntity.Updated = DateTime.Now;
                    entityEntry.Property(nameof(IAuditEntity.Created)).IsModified = false;
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
