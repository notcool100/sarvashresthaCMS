using SarvashresthaCMS.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SarvashresthaCMS.Application.Interfaces;

public interface IOfferRepository
{
    Task<IEnumerable<Offer>> GetAllAsync();
    Task<Offer?> GetByIdAsync(int id);
    Task<IEnumerable<int>> GetRoomIdsAsync(int offerId);
    Task<int> CreateAsync(Offer offer, IEnumerable<int> roomIds);
    Task<bool> UpdateAsync(Offer offer, IEnumerable<int> roomIds);
    Task<bool> DeleteAsync(int id);
}
