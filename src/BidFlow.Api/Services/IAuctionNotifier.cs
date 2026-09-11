using BidFlow.Api.DTOs;

namespace BidFlow.Api.Services;

public interface IAuctionNotifier
{
    Task NotifyBidPlacedAsync(Guid auctionId, BidPlacedNotification notification);
}
