using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SarvashresthaCMS.Application.Common;
using SarvashresthaCMS.Application.Interfaces;
using SarvashresthaCMS.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SarvashresthaCMS.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class OfferController(IOfferRepository offerRepository) : ControllerBase
{
    private readonly IOfferRepository _offerRepository = offerRepository;

    public record OfferRequest(
        string Title,
        string Description,
        string DiscountType,
        decimal DiscountValue,
        System.DateTime StartDate,
        System.DateTime EndDate,
        bool IsActive,
        bool AppliesToAllRooms,
        List<int>? RoomIds
    );

    public record OfferResponse(
        int Id,
        string Title,
        string Description,
        string DiscountType,
        decimal DiscountValue,
        System.DateTime StartDate,
        System.DateTime EndDate,
        bool IsActive,
        bool AppliesToAllRooms,
        List<int> RoomIds
    );

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<OfferResponse>>>> GetAll()
    {
        var offers = await _offerRepository.GetAllAsync();
        var result = new List<OfferResponse>();
        foreach (var offer in offers)
        {
            var roomIds = (await _offerRepository.GetRoomIdsAsync(offer.Id)).ToList();
            result.Add(new OfferResponse(
                offer.Id,
                offer.Title,
                offer.Description,
                offer.DiscountType,
                offer.DiscountValue,
                offer.StartDate,
                offer.EndDate,
                offer.IsActive,
                offer.AppliesToAllRooms,
                roomIds
            ));
        }
        return Ok(ServiceResponse<IEnumerable<OfferResponse>>.Ok(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<OfferResponse>>> GetById(int id)
    {
        var offer = await _offerRepository.GetByIdAsync(id);
        if (offer == null)
            return NotFound(ServiceResponse<OfferResponse>.Fail("Offer not found."));

        var roomIds = (await _offerRepository.GetRoomIdsAsync(offer.Id)).ToList();
        var response = new OfferResponse(
            offer.Id,
            offer.Title,
            offer.Description,
            offer.DiscountType,
            offer.DiscountValue,
            offer.StartDate,
            offer.EndDate,
            offer.IsActive,
            offer.AppliesToAllRooms,
            roomIds
        );
        return Ok(ServiceResponse<OfferResponse>.Ok(response));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<int>>> Create([FromBody] OfferRequest request)
    {
        var offer = new Offer
        {
            Title = request.Title,
            Description = request.Description,
            DiscountType = request.DiscountType,
            DiscountValue = request.DiscountValue,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsActive = request.IsActive,
            AppliesToAllRooms = request.AppliesToAllRooms
        };

        var id = await _offerRepository.CreateAsync(offer, request.RoomIds ?? new List<int>());
        return CreatedAtAction(nameof(GetById), new { id }, ServiceResponse<int>.Ok(id, "Offer created successfully."));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceResponse<bool>>> Update(int id, [FromBody] OfferRequest request)
    {
        var offer = new Offer
        {
            Id = id,
            Title = request.Title,
            Description = request.Description,
            DiscountType = request.DiscountType,
            DiscountValue = request.DiscountValue,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsActive = request.IsActive,
            AppliesToAllRooms = request.AppliesToAllRooms
        };

        var result = await _offerRepository.UpdateAsync(offer, request.RoomIds ?? new List<int>());
        if (!result)
            return NotFound(ServiceResponse<bool>.Fail("Offer not found or update failed."));

        return Ok(ServiceResponse<bool>.Ok(result, "Offer updated successfully."));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<bool>>> Delete(int id)
    {
        var result = await _offerRepository.DeleteAsync(id);
        if (!result)
            return NotFound(ServiceResponse<bool>.Fail("Offer not found or delete failed."));

        return Ok(ServiceResponse<bool>.Ok(result, "Offer deleted successfully."));
    }
}
