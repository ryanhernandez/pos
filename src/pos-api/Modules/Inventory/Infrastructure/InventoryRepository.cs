using Microsoft.EntityFrameworkCore;
using Server.Shared.Infrastructure;
using Server.Modules.Inventory.Domain;

namespace Server.Modules.Inventory.Infrastructure
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AppDbContext _db;

        public InventoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Item item)
        {
            await _db.Items.AddAsync(item);
        }

        public async Task DeleteAsync(Guid id)
        {
            var item = await _db.Items.FindAsync(id);
            if (item != null)
                _db.Items.Remove(item);
        }

        public async Task<Item?> GetByIdAsync(Guid id)
        {
            return await _db.Items.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Item>> ListAllAsync()
        {
            return await _db.Items.AsNoTracking().ToListAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _db.SaveChangesAsync();
        }

        public Task UpdateAsync(Item item)
        {
            _db.Items.Update(item);
            return Task.CompletedTask;
        }
    }
}
