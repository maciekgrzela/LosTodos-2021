using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Tag> Tags {get; set; }
        public DbSet<Task> Tasks {get; set; }
        public DbSet<TaskSet> TaskSets {get; set; }
        public DbSet<TaskSetTags> TaskSetTags {get; set;}
        
        public DatabaseContext([NotNullAttribute] DbContextOptions<DatabaseContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.Entity<TaskSet>()
                .HasMany(p => p.Tasks)
                .WithOne(p => p.TaskSet)
                .HasForeignKey(p => p.TaskSetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<TaskSetTags>()
                .HasKey(tst => new {tst.TagId, tst.TaskSetId});

            builder.Entity<TaskSetTags>()
                .HasOne(p => p.TaskSet)
                .WithMany(p => p.TaskSetTags)
                .HasForeignKey(p => p.TaskSetId);

            builder.Entity<TaskSetTags>()
                .HasOne(p => p.Tag)
                .WithMany(p => p.TaskSetTags)
                .HasForeignKey(p => p.TagId);

            builder.Entity<Task>()
                .HasOne(p => p.TaskSet)
                .WithMany(p => p.Tasks)
                .HasForeignKey(p => p.TaskSetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}