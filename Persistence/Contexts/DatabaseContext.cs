using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class DatabaseContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public DbSet<Tag> Tags {get; set; }
        public DbSet<Task> Tasks {get; set; }
        public DbSet<TaskSet> TaskSets {get; set; }
        public DbSet<TaskSetTags> TaskSetTags {get; set;}
        
        public DatabaseContext([NotNullAttribute] DbContextOptions<DatabaseContext> options) : base(options) {}

        public async override System.Threading.Tasks.Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is ModifiedAndCreatedEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((ModifiedAndCreatedEntity)entityEntry.Entity).LastModified = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((ModifiedAndCreatedEntity)entityEntry.Entity).Created = DateTime.Now;
                }
            }

            return (await base.SaveChangesAsync(true, cancellationToken));
        }


        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<TaskSet>()
                .HasMany(p => p.Tasks)
                .WithOne(p => p.TaskSet)
                .HasForeignKey(p => p.TaskSetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AppUser>()
                .HasMany(p => p.Tags)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AppUser>()
                .HasMany(p => p.TaskSets)
                .WithOne(p => p.Owner)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TaskSetTags>()
                .HasKey(tst => new {tst.TagId, tst.TaskSetId});

            builder.Entity<TaskSetTags>()
                .HasOne(p => p.TaskSet)
                .WithMany(p => p.TaskSetTags)
                .HasForeignKey(p => p.TaskSetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TaskSetTags>()
                .HasOne(p => p.Tag)
                .WithMany(p => p.TaskSetTags)
                .HasForeignKey(p => p.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Task>()
                .HasOne(p => p.TaskSet)
                .WithMany(p => p.Tasks)
                .HasForeignKey(p => p.TaskSetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}