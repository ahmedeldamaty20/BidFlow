using BidFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BidFlow.Infrastructure.Data;

public class BidFlowDbContext(DbContextOptions<BidFlowDbContext> options) : DbContext(options)
{
    public DbSet<Auction> Auctions => Set<Auction>();
    public DbSet<Bid> Bids => Set<Bid>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auction>(entity =>
        {
            entity.Property(a => a.Title).IsRequired().HasMaxLength(200);
            entity.Property(a => a.StartingPrice).HasColumnType("numeric(12,2)");
            entity.Property(a => a.CurrentPrice).HasColumnType("numeric(12,2)");
        });

        modelBuilder.Entity<Bid>(entity =>
        {
            entity.Property(b => b.Amount).HasColumnType("numeric(12,2)");

            entity.HasOne(b => b.Auction)
                .WithMany(a => a.Bids)
                .HasForeignKey(b => b.AuctionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
