using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SarvashresthaCMS.Application.Common;
using SarvashresthaCMS.Application.Interfaces;
using SarvashresthaCMS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SarvashresthaCMS.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController(IRoomRepository roomRepository) : ControllerBase
{
    private readonly IRoomRepository _roomRepository = roomRepository;

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<Room>>>> GetAll()
    {
        var rooms = await _roomRepository.GetAllAsync();
        return Ok(ServiceResponse<IEnumerable<Room>>.Ok(rooms));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Room>>> GetById(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        if (room == null) 
            return NotFound(ServiceResponse<Room>.Fail("Room not found."));
        return Ok(ServiceResponse<Room>.Ok(room));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<int>>> Create(Room room)
    {
        var id = await _roomRepository.CreateAsync(room);
        return CreatedAtAction(nameof(GetById), new { id = id }, ServiceResponse<int>.Ok(id, "Room created successfully."));
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceResponse<bool>>> Update(int id, Room room)
    {
        if (id != room.Id) 
            return BadRequest(ServiceResponse<bool>.Fail("ID mismatch."));
        
        var result = await _roomRepository.UpdateAsync(room);
        if (!result) 
            return NotFound(ServiceResponse<bool>.Fail("Room not found or update failed."));
        
        return Ok(ServiceResponse<bool>.Ok(result, "Room updated successfully."));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<bool>>> Delete(int id)
    {
        var result = await _roomRepository.DeleteAsync(id);
        if (!result) 
            return NotFound(ServiceResponse<bool>.Fail("Room not found or delete failed."));
        
        return Ok(ServiceResponse<bool>.Ok(result, "Room deleted successfully."));
    }
}
