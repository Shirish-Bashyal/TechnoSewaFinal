using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Base;
using Domain.Interfaces.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.DbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IHttpContextAccessor httpContextAccessor
        )
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override int SaveChanges()
        {
            UpdateAuditableEntities();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default
        )
        {
            UpdateAuditableEntities();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditableEntities()
        {
            //taking out the userId from header
            string? userId = _httpContextAccessor
                .HttpContext?.User
                ?.FindFirstValue(ClaimTypes.NameIdentifier);

            var entries = ChangeTracker //finding entities that is of following interfaces and is either added or modified
                .Entries()
                .Where(e =>
                    (
                        e.Entity is IDateAudited
                        || e.Entity is IHasCreationDate
                        || e.Entity is IFulAudited
                    ) && (e.State == EntityState.Added || e.State == EntityState.Modified)
                );

            foreach (var entry in entries)
            {
                if (entry.Entity is IDateAudited datedEntity)
                {
                    datedEntity.ModifiedDate = DateTime.UtcNow;
                }

                if (
                    entry.Entity is IHasCreationDate hasCreationDate
                    && entry.State == EntityState.Added
                )
                {
                    hasCreationDate.AddedDate = DateTime.UtcNow;
                }

                if (entry.Entity is IFulAudited fullAudited)
                {
                    if (entry.State == EntityState.Added)
                    {
                        fullAudited.AddedBy = userId;
                        fullAudited.AddedDate = DateTime.UtcNow;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        fullAudited.ModifiedBy = userId;
                        fullAudited.ModifiedDate = DateTime.UtcNow;
                    }
                }
            }
        }
    }
}
