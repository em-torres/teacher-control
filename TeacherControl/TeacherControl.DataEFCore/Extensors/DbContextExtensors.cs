using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using TeacherControl.Core.AuditableModels;
using TeacherControl.Domain.Services;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class DbContextExtensors
    {
        public static ChangeTracker ApplyAuditInformation(this ChangeTracker tracker, IUserService userService)
        {
            string username = userService.GetUsername();

            if (tracker.HasChanges())
            {
                foreach(EntityEntry entry in tracker.Entries())
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.CurrentValues[nameof(IModificationAudit.CreatedBy)] = username;
                        entry.CurrentValues[nameof(IModificationAudit.CreatedDate)] = DateTime.UtcNow;
                    }
                    else if(entry.State == EntityState.Modified)
                    {
                        entry.CurrentValues[nameof(IModificationAudit.UpdatedBy)] = username;
                        entry.CurrentValues[nameof(IModificationAudit.UpdatedBy)] = DateTime.UtcNow;
                    }
                }
            }

            return tracker;
        }
    }
}
