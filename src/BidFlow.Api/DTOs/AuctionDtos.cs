namespace BidFlow.Api.DTOs;

public record CreateAuctionRequest(string Title, string Description, decimal StartingPrice, string SellerName);

public record AuctionResponse(
    Guid Id,
    string Title,
    string Description,
    decimal StartingPrice,
    decimal CurrentPrice,
    string SellerName,
    DateTime CreatedAt
);
