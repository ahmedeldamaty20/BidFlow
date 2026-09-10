using BidFlow.Domain.Entities;

namespace BidFlow.Api.Services;

public interface IAuctionService
{
    Task<Auction> CreateAuctionAsync(string title, string description, decimal startingPrice, string sellerName);

    Task<Auction?> GetAuctionAsync(Guid auctionId);

    Task<(bool Success, string? Error, Bid? Bid)> PlaceBidAsync(Guid auctionId, string bidderName, decimal amount);
}
