using BidFlow.Api.DTOs;
using BidFlow.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BidFlow.Api.Controllers;

public class AuctionsController(IAuctionService auctionService) : BaseController
{
    [HttpPost]
    public async Task<ActionResult<AuctionResponse>> CreateAuction(CreateAuctionRequest request)
    {
        var auction = await auctionService.CreateAuctionAsync(
            request.Title, 
            request.Description, 
            request.StartingPrice, 
            request.SellerName
        );

        var response = new AuctionResponse(
            auction.Id, 
            auction.Title,
            auction.Description,
            auction.StartingPrice,
            auction.CurrentPrice,
            auction.SellerName,
            auction.CreatedAt
        );

        return CreatedAtAction(nameof(GetAuction), new { id = auction.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionResponse>> GetAuction(Guid id)
    {
        var auction = await auctionService.GetAuctionAsync(id);
        if (auction is null) return NotFound();

        var response = new AuctionResponse(
            auction.Id, 
            auction.Title, 
            auction.Description,
            auction.StartingPrice, 
            auction.CurrentPrice, 
            auction.SellerName, 
            auction.CreatedAt
        );

        return Ok(response);
    }
}
