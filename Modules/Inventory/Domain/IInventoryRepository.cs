using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Modules.Inventory.Domain
{
    public interface IInventoryRepository
    {
        Task<Item?> GetByIdAsync(Guid id);
        Task<IEnumerable<Item>> ListAllAsync();
        Task AddAsync(Item item);
        Task UpdateAsync(Item item);
        Task DeleteAsync(Guid id);
        Task<int> SaveChangesAsync();
    }
}
