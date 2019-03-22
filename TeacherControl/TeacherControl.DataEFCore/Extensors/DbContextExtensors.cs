﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using TeacherControl.Core.AuditableModels;
using TeacherControl.Core.Enums;
using TeacherControl.Domain.Services;

namespace TeacherControl.DataEFCore.Extensors
{
    public static class DbContextExtensors
    {
        public static ChangeTracker ApplyAuditInformation(this ChangeTracker tracker, IUserService userService)
        {
            if (tracker.HasChanges())
            {
                string username = userService.GetUsername();
                foreach (EntityEntry entry in tracker.Entries())
                {
                    if (entry.Entity is IModificationAudit)
                    {
                        if (entry.State == EntityState.Added)
                        {
                            entry.CurrentValues[nameof(IModificationAudit.CreatedBy)] = username;
                            entry.CurrentValues[nameof(IModificationAudit.CreatedDate)] = DateTime.UtcNow;
                        }
                        else if (entry.State == EntityState.Modified)
                        {
                            entry.CurrentValues[nameof(IModificationAudit.UpdatedBy)] = username;
                            entry.CurrentValues[nameof(IModificationAudit.UpdatedDate)] = DateTime.UtcNow;
                        }
                    }

                    //if (entry.Entity is IStatusAudit)
                    //{
                    //    if (entry.State == EntityState.Added)
                    //    {
                    //        entry.CurrentValues[nameof(IStatusAudit.StatusId)] = (int)Status.Active;
                    //    }
                    //}
                }
            }

            return tracker;
        }
    }
}
