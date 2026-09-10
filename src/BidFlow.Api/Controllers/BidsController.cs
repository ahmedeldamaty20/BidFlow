using BidFlow.Api.DTOs;
using BidFlow.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BidFlow.Api.Controllers;

public class BidsController(IAuctionService auctionService) : BaseController
{
    [HttpPost]
    public async Task<ActionResult<BidResponse>> PlaceBid(Guid auctionId, PlaceBidRequest request)
    {
        var (success, error, bid) = await auctionService.PlaceBidAsync(auctionId, request.BidderName, request.Amount);

        if (!success)
            return BadRequest(new { error });

        var response = new BidResponse(bid!.Id, bid.AuctionId, bid.BidderName, bid.Amount, bid.PlacedAt);

        // Here we're returning the response only to the client that placed the bid.
        // Other participants in the same auction won't receive any notification
        // this is the issue we'll discover and solve in Flow 2.
        return Ok(response);
    }
}
