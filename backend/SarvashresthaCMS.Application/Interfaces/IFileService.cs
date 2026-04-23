using System.IO;
using System.Threading.Tasks;

namespace SarvashresthaCMS.Application.Interfaces;

public interface IFileService
{
    /// <summary>
    /// Saves a file stream to the specified subfolder and returns the relative path.
    /// </summary>
    Task<string> SaveFileAsync(Stream fileStream, string fileName, string subFolder, string[] allowedExtensions);
    
    /// <summary>
    /// Deletes a file from the physical storage if it exists.
    /// </summary>
    void DeleteFile(string relativePath);
}
