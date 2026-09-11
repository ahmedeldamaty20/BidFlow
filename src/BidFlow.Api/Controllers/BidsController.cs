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

        return Ok(response);
    }
}
