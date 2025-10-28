using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Interceptors
{
    public class AuditDbContextInterceptor:SaveChangesInterceptor
    {
        private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> Behaviors = new()
        {
            { EntityState.Added, AddBehavior },
            { EntityState.Modified, UpdateBehavior }
        };
        private static void AddBehavior(DbContext context, IAuditEntity auditEntity) 
        {
            auditEntity.Created = DateTime.Now;
            context.Entry(auditEntity).Property(x => x.Updated).IsModified = false;

        }
        private static  void UpdateBehavior(DbContext context, IAuditEntity auditEntity)
        {
            auditEntity.Updated = DateTime.Now;
            context.Entry(auditEntity).Property(x => x.Created).IsModified = false;
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
            {
                if (entityEntry.Entity is not IAuditEntity addedEntity) continue;

                Behaviors[entityEntry.State](eventData.Context, addedEntity);
             }
          return base.SavingChangesAsync(eventData, result, cancellationToken);   
        }
    }
}
