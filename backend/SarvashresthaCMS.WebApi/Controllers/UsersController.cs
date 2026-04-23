using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SarvashresthaCMS.Application.Common;
using SarvashresthaCMS.Application.DTOs.Booking;
using SarvashresthaCMS.Application.Interfaces;
using SarvashresthaCMS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SarvashresthaCMS.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController(IUserRepository userRepository) : ControllerBase
{
    private readonly IUserRepository _userRepository = userRepository;


//   [HttpGet("{userId}")]
//      public async Task <ActionResult<ServiceResponse<IEnumerable<Users>>>> GetbyuserId()
//     {
//         var bookings = await _bookingRepository.GetbyuserId();
//         return Ok(ServiceResponse<IEnumerable<Booking>>.Ok(bookings));
//     }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<User>>>> GetAll()
    {
        var Users = await _userRepository.GetAllUsers();
        return Ok(ServiceResponse<IEnumerable<User>>.Ok(Users));
    }

//     [HttpGet("{id}")]
//     public async Task<ActionResult<ServiceResponse<Booking>>> GetById(int id)
//     {
//         var booking = await _bookingRepository.GetByIdAsync(id);
//         if (booking == null)
//             return NotFound(ServiceResponse<Booking>.Fail("Booking not found."));
//         return Ok(ServiceResponse<Booking>.Ok(booking));
//     }

//     [HttpPost]
//     public async Task<ActionResult<ServiceResponse<int>>> Create([FromBody] CreateBookingRequest request)
//     {
//         var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
//                            ?? User.FindFirst("sub")?.Value;

//         if (string.IsNullOrEmpty(userIdString))
//         {
//             return Unauthorized(ServiceResponse<int>.Fail("User is not logged in or invalid token."));
//         }

//         if (!int.TryParse(userIdString, out var userId))
//         {
//             return BadRequest(ServiceResponse<int>.Fail("Invalid user identity."));
//         }

//         var booking = new Booking
//         {
//             UserId = userId,
//             RoomId = request.RoomId,
//             OfferId = request.OfferId,
//             GuestName = request.GuestName,
//             guestEmail = request.GuestEmail,
//             CheckIn = request.CheckIn,
//             CheckOut = request.CheckOut,
//             TotalPrice = request.TotalPrice,
//             DiscountAmount = request.DiscountAmount,
//             FinalPrice = request.FinalPrice,
//             NumberOfPeople = request.NumberOfPeople,
//             Status = "Pending"
//         };

//         var id = await _bookingRepository.CreateAsync(booking);

//         return CreatedAtAction(nameof(GetById), new { id = id },
//             ServiceResponse<int>.Ok(id, "Booking created successfully."));
//     }
//     [HttpPatch("{id}/status")]
//     public async Task<ActionResult<ServiceResponse<bool>>> UpdateStatus(int id, [FromBody] UpdateBookingStatusRequest request)
//     {
//         var result = await _bookingRepository.UpdateStatusAsync(id, request.Status);
//         if (!result)
//             return NotFound(ServiceResponse<bool>.Fail("Booking not found or update failed."));
//         return Ok(ServiceResponse<bool>.Ok(result, "Booking status updated successfully."));
//     }

//     [HttpDelete("{id}")]
//     public async Task<ActionResult<ServiceResponse<bool>>> Delete(int id)
//     {
//         var result = await _bookingRepository.DeleteAsync(id);
//         if (!result)
//             return NotFound(ServiceResponse<bool>.Fail("Booking not found or delete failed."));
//         return Ok(ServiceResponse<bool>.Ok(result, "Booking removed successfully."));
//     }
  
// }
}
