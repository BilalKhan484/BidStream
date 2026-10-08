using Microsoft.EntityFrameworkCore;
using BidStream.Domain.Entities;
using BidStream.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Identity.Client;
using System.Threading;
using System.Threading.Tasks;

namespace BidStream.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Item> Items => Set<Item>();
        public DbSet<Bid> Bids => Set<Bid>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Configure decimal precision for Item entity
            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(i => i.CurrentPrice).HasColumnType("decimal(18,2)");
                entity.Property(i => i.StartingPrice).HasColumnType("decimal(18,2)");

                // Indexes for performance
                entity.HasIndex(i => i.Status);
                entity.HasIndex(i => i.EndTime);
            });


            modelBuilder.Entity<Bid>(entity =>
            {
                entity.Property(b => b.Amount).HasColumnType("decimal(18,2)");

                // Configure the relationship between Bid and Item
                entity.HasOne(b => b.Item)
                  .WithMany(i => i.Bids)
                  .HasForeignKey(b => b.ItemId)
                  .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(b => b.ItemId);
            });
        }

        // Override saveChanges to automatically set CreatedAt and LastModifiedAt timestamps
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedAt = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

