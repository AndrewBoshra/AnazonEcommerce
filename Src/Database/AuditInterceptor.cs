using Anazon.Models;
using Anazon.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Anazon.Database;

public class AuditInterceptor(IServiceProvider serviceProvider) : SaveChangesInterceptor
{

    private CurrentUserService CurrentUserService => serviceProvider.GetRequiredService<CurrentUserService>();
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplyAuditing(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        ApplyAuditing(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private  void ApplyAuditing(DbContext? context)
    {
        if (context == null) return;

        var entries = context.ChangeTracker.Entries<AuditableEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = CurrentUserService.CurrentUserId;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = CurrentUserService.CurrentUserId;
                    break;
            }
        }
    }
}
