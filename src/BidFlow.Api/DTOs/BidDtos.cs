namespace BidFlow.Api.DTOs;

public record PlaceBidRequest(string BidderName, decimal Amount);

public record BidResponse(Guid Id, Guid AuctionId, string BidderName, decimal Amount, DateTime PlacedAt);

public record BidPlacedNotification(Guid BidId, Guid AuctionId, string BidderName, decimal Amount, DateTime PlacedAt);
