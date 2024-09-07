using Backend.Entities.Common;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Backend.Database.Interceptors;

public class AuditInterceptor(IDateTimeService dateTime) : SaveChangesInterceptor
{
    private readonly IDateTimeService _date = dateTime;

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result
    )
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context == null)
            return;
        // base entity
        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = _date.Now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = _date.Now;
            }
        }
    }
}
