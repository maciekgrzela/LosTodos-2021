using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class DatabaseContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) {}
        
        public DbSet<Tag> Tags {get; set; }
        public DbSet<Todo> Todos {get; set; }
        public DbSet<TodoSet> TodoSets {get; set; }
        public DbSet<TodoSetTags> TodoSetTags {get; set;}

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            DatesBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new())
        {
            DatesBeforeSaving();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void DatesBeforeSaving()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                ((BaseEntity)entry.Entity).LastModified = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).Created = DateTime.Now;
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<TodoSet>()
                .HasMany(p => p.Todos)
                .WithOne(p => p.TodoSet)
                .HasForeignKey(p => p.TodoSetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>()
                .HasMany(p => p.Tags)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AppUser>()
                .HasMany(p => p.TodoSets)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TodoSetTags>()
                .HasKey(tst => new {tst.TagId, tst.TodoSetId});

            builder.Entity<TodoSetTags>()
                .HasOne(p => p.TodoSet)
                .WithMany(p => p.TodoSetTags)
                .HasForeignKey(p => p.TodoSetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TodoSetTags>()
                .HasOne(p => p.Tag)
                .WithMany(p => p.TodoSetTags)
                .HasForeignKey(p => p.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Todo>()
                .HasOne(p => p.TodoSet)
                .WithMany(p => p.Todos)
                .HasForeignKey(p => p.TodoSetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}