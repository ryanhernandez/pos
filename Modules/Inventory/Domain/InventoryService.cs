using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Modules.Inventory.Domain
{
    public class InventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Item> CreateItemAsync(string name, decimal price, int quantity, string? description = null)
        {
            // Business rules: name required, non-negative price/quantity
            var item = new Item(name, price, quantity, description);
            await _repository.AddAsync(item);
            await _repository.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<Item>> ListItemsAsync()
        {
            return await _repository.ListAllAsync();
        }

        public async Task<Item?> GetItemAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Item?> UpdateItemAsync(Guid id, string name, decimal price, int quantity, string? description = null)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return null;

            item.Update(name, price, quantity, description);
            await _repository.UpdateAsync(item);
            await _repository.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteItemAsync(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;
            await _repository.DeleteAsync(id);
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
