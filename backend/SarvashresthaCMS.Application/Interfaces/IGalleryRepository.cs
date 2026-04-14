using SarvashresthaCMS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SarvashresthaCMS.Application.Interfaces;

public interface IGalleryRepository
{
    Task<IEnumerable<GalleryItem>> GetAllAsync();
    Task<int> CreateAsync(GalleryItem item);
    Task<bool> DeleteAsync(int id);
}
