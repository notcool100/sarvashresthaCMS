using Microsoft.Extensions.Hosting;
using SarvashresthaCMS.Application.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SarvashresthaCMS.Infrastructure.Services;

public class FileService(IHostEnvironment env) : IFileService
{
    private readonly string _uploadFolder = Path.Combine(env.ContentRootPath, "uploads");

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string subFolder)
    {
        if (fileStream == null) return null;

        var targetFolder = Path.Combine(_uploadFolder, subFolder);
        if (!Directory.Exists(targetFolder)) Directory.CreateDirectory(targetFolder);

        var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        var filePath = Path.Combine(targetFolder, newFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await fileStream.CopyToAsync(stream);
        }

        // Return the relative URL path
        return $"/uploads/{subFolder}/{newFileName}";
    }

    public void DeleteFile(string relativePath)
    {
        if (string.IsNullOrEmpty(relativePath)) return;
        
        var relativePart = relativePath.Replace("/uploads/", "");
        var physicalPath = Path.Combine(_uploadFolder, relativePart.Replace("/", Path.DirectorySeparatorChar.ToString()));
        if (File.Exists(physicalPath)) File.Delete(physicalPath);
    }
}
