using Microsoft.AspNetCore.SignalR;

namespace BidFlow.Api.Hubs;
public class AuctionHub : Hub
{
    public async Task JoinAuctionGroup(string auctionId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, GroupName(auctionId));
    }

    public async Task LeaveAuctionGroup(string auctionId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupName(auctionId));
    }

    public static string GroupName(string auctionId) => $"auction-{auctionId}";
}
