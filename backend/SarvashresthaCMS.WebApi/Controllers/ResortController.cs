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
public class ResortController(IResortRepository resortRepository) : ControllerBase
{
    private readonly IResortRepository _resortRepository = resortRepository;

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<Resort>>>> GetAll()
    {
        var resorts = await _resortRepository.GetAllAsync();
        return Ok(ServiceResponse<IEnumerable<Resort>>.Ok(resorts));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Resort>>> GetById(int id)
    {
        var resort = await _resortRepository.GetByIdAsync(id);
        if (resort == null) 
            return NotFound(ServiceResponse<Resort>.Fail("Resort not found."));
        return Ok(ServiceResponse<Resort>.Ok(resort));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<int>>> Create(Resort resort)
    {
        var id = await _resortRepository.CreateAsync(resort);
        return CreatedAtAction(nameof(GetById), new { id = id }, ServiceResponse<int>.Ok(id, "Resort created successfully."));
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ServiceResponse<bool>>> Update(int id, Resort resort)
    {
        if (id != resort.Id) 
            return BadRequest(ServiceResponse<bool>.Fail("ID mismatch."));
        
        var result = await _resortRepository.UpdateAsync(resort);
        if (!result) 
            return NotFound(ServiceResponse<bool>.Fail("Resort not found or update failed."));
        
        return Ok(ServiceResponse<bool>.Ok(result, "Resort updated successfully."));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<bool>>> Delete(int id)
    {
        var result = await _resortRepository.DeleteAsync(id);
        if (!result) 
            return NotFound(ServiceResponse<bool>.Fail("Resort not found or delete failed."));
        
        return Ok(ServiceResponse<bool>.Ok(result, "Resort deleted successfully."));
    }
}
