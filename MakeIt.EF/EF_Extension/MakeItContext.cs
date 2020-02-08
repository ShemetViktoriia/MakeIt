using MakeIt.EF.Common;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;

namespace MakeIt.EF
{
    public partial class MakeItContext
    {
        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
              .Where(x => x.Entity is IAuditable
                  && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                IAuditable entity = entry.Entity as IAuditable;
                if (entity != null)
                {
                    //TODO later for Web
                    string identityName = new WindowsPrincipal(WindowsIdentity.GetCurrent()).Identity.Name;
                    DateTime now = DateTime.UtcNow;

                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreatedDate = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedDate = now;
                }
            }
            return base.SaveChanges();
        }
    }
}
