using BasicAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BasicAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Website> Websites { get; set; }

        public override int SaveChanges()
        {
            ModifyEntitiesBeforeSave();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ModifyEntitiesBeforeSave();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ModifyEntitiesBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ModifyEntitiesBeforeSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Website>().HasQueryFilter(w => !w.IsDeleted);
        }

        #region Private Methods
        private void ModifyEntitiesBeforeSave()
        {
            var entries = ChangeTracker.Entries();
            UpdateSoftDeleteStatuses(entries);
        }

        private void UpdateSoftDeleteStatuses(IEnumerable<EntityEntry> entries)
        {
            var softDeletableEntries = entries.Where(x => x.Entity is ISoftDeleteEntity);
            foreach (var entry in softDeletableEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues[nameof(Website.IsDeleted)] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues[nameof(Website.IsDeleted)] = true;
                        break;
                }
            }
        }
        #endregion
    }
}