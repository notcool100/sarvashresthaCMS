using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SarvashresthaCMS.Application.Common;
using SarvashresthaCMS.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace SarvashresthaCMS.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UploadController(IFileService fileService) : ControllerBase
{
    private readonly IFileService _fileService = fileService;

    [HttpPost("room")]
    public async Task<ActionResult<ServiceResponse<string>>> UploadRoomImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(ServiceResponse<string>.Fail("No file uploaded."));

        using var stream = file.OpenReadStream();
        var path = await _fileService.SaveFileAsync(stream, file.FileName, "room");
        
        return Ok(ServiceResponse<string>.Ok(path, "Image uploaded successfully."));
    }

    [HttpPost("room/multiple")]
    public async Task<ActionResult<ServiceResponse<List<string>>>> UploadRoomImages(List<IFormFile> files)
    {
        if (files == null || files.Count == 0)
            return BadRequest(ServiceResponse<List<string>>.Fail("No files uploaded."));

        var paths = new List<string>();
        foreach (var file in files)
        {
            using var stream = file.OpenReadStream();
            var path = await _fileService.SaveFileAsync(stream, file.FileName, "room");
            paths.Add(path);
        }
        
        return Ok(ServiceResponse<List<string>>.Ok(paths, "Images uploaded successfully."));
    }

    [HttpPost("offer")]
    public async Task<ActionResult<ServiceResponse<string>>> UploadOfferImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(ServiceResponse<string>.Fail("No file uploaded."));

        using var stream = file.OpenReadStream();
        var path = await _fileService.SaveFileAsync(stream, file.FileName, "offer");
        
        return Ok(ServiceResponse<string>.Ok(path, "Image uploaded successfully."));
    }
}
