using Microsoft.AspNetCore.Mvc;
using SarvashresthaCMS.Application.Interfaces;
using SarvashresthaCMS.Domain.Entities;

namespace SarvashresthaCMS.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResortController(IResortRepository resortRepository) : ControllerBase
{
    private readonly IResortRepository _resortRepository = resortRepository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Resort>>> GetAll()
    {
        var resorts = await _resortRepository.GetAllAsync();
        return Ok(resorts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Resort>> GetById(int id)
    {
        var resort = await _resortRepository.GetByIdAsync(id);
        if (resort == null) return NotFound();
        return Ok(resort);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create(Resort resort)
    {
        var id = await _resortRepository.CreateAsync(resort);
        return CreatedAtAction(nameof(GetById), new { id = id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Resort resort)
    {
        if (id != resort.Id) return BadRequest();
        var result = await _resortRepository.UpdateAsync(resort);
        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _resortRepository.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}
