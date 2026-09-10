using BidFlow.Api.DTOs;
using BidFlow.Domain.Entities;
using BidFlow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BidFlow.Api.Services;

public class AuctionService(BidFlowDbContext db, IAuctionNotifier notifier) : IAuctionService
{
    public async Task<Auction> CreateAuctionAsync(string title, string description, decimal startingPrice, string sellerName)
    {
        var auction = new Auction
        {
            Title = title,
            Description = description,
            StartingPrice = startingPrice,
            CurrentPrice = startingPrice,
            SellerName = sellerName
        };

        db.Auctions.Add(auction);
        await db.SaveChangesAsync();
        return auction;
    }

    public async Task<Auction?> GetAuctionAsync(Guid auctionId)
    {
        return await db.Auctions
            .Include(a => a.Bids.OrderByDescending(b => b.PlacedAt))
            .FirstOrDefaultAsync(a => a.Id == auctionId);
    }

    public async Task<(bool Success, string? Error, Bid? Bid)> PlaceBidAsync(Guid auctionId, string bidderName, decimal amount)
    {
        var auction = await db.Auctions.FirstOrDefaultAsync(a => a.Id == auctionId);
        if (auction is null)
            return (false, "Auction not found", null);

        // Validate bid amount
        if (amount <= auction.CurrentPrice)
            return (false, $"Bid must be greater than current price ({auction.CurrentPrice})", null);

        var bid = new Bid
        {
            AuctionId = auctionId,
            BidderName = bidderName,
            Amount = amount
        };

        auction.CurrentPrice = amount;

        db.Bids.Add(bid);
        await db.SaveChangesAsync();

        var notification = new BidPlacedNotification(bid.Id, bid.AuctionId, bid.BidderName, bid.Amount, bid.PlacedAt);
        await notifier.NotifyBidPlacedAsync(auctionId, notification);

        return (true, null, bid);
    }
}
