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
public class GalleryController(IGalleryRepository galleryRepository) : ControllerBase
{
    private readonly IGalleryRepository _galleryRepository = galleryRepository;

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<GalleryItem>>>> GetAll()
    {
        var items = await _galleryRepository.GetAllAsync();
        return Ok(ServiceResponse<IEnumerable<GalleryItem>>.Ok(items));
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<int>>> Create(GalleryItem item)
    {
        var id = await _galleryRepository.CreateAsync(item);
        return Ok(ServiceResponse<int>.Ok(id, "Gallery image added successfully."));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<bool>>> Delete(int id)
    {
        var result = await _galleryRepository.DeleteAsync(id);
        if (!result)
            return NotFound(ServiceResponse<bool>.Fail("Gallery image not found or delete failed."));
        
        return Ok(ServiceResponse<bool>.Ok(result, "Gallery image deleted successfully."));
    }
}
