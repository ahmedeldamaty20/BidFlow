namespace BidFlow.Domain.Entities;

public class Auction
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal StartingPrice { get; set; }
    public decimal CurrentPrice { get; set; }

    // Temporary: Seller name as free text. Will be changed to SellerId linked with JWT in Flow 5
    public string SellerName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Bid> Bids { get; set; } = new();
}
