using BidFlow.Api.DTOs;
using BidFlow.Api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace BidFlow.Api.Services;

public class SignalRAuctionNotifier(IHubContext<AuctionHub> hubContext) : IAuctionNotifier
{
    public Task NotifyBidPlacedAsync(Guid auctionId, BidPlacedNotification notification)
    {
        var group = AuctionHub.GroupName(auctionId.ToString());
        return hubContext.Clients.Group(group).SendAsync("BidPlaced", notification);
    }
}
