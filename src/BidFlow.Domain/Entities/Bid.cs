namespace BidFlow.Domain.Entities;

public class Bid
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AuctionId { get; set; }
    public Auction? Auction { get; set; }

    // Temporary: Bidder name as free text. Will be changed to BidderId linked with JWT in Flow 5
    public string BidderName { get; set; } = string.Empty;

    public decimal Amount { get; set; }
    public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
}