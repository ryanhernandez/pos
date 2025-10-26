using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Server.Modules.Inventory.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Server.UnitTests
{
    [TestClass]
    public class InventoryServiceTests
    {
        class InMemoryInventoryRepository : IInventoryRepository
        {
            private readonly List<Item> _items = new();

            public Task AddAsync(Item item)
            {
                _items.Add(item);
                return Task.CompletedTask;
            }

            public Task DeleteAsync(System.Guid id)
            {
                _items.RemoveAll(i => i.Id == id);
                return Task.CompletedTask;
            }

            public Task<Item?> GetByIdAsync(System.Guid id)
            {
                return Task.FromResult(_items.Find(i => i.Id == id));
            }

            public Task<IEnumerable<Item>> ListAllAsync()
            {
                return Task.FromResult<IEnumerable<Item>>(_items);
            }

            public Task<int> SaveChangesAsync() => Task.FromResult(0);

            public Task UpdateAsync(Item item)
            {
                var idx = _items.FindIndex(i => i.Id == item.Id);
                if (idx >= 0) _items[idx] = item;
                return Task.CompletedTask;
            }
        }

        [TestMethod]
        public async Task CreateAndListItem()
        {
            var repo = new InMemoryInventoryRepository();
            var svc = new InventoryService(repo);

            var item = await svc.CreateItemAsync("Test", 1.23m, 5, "desc");
            var list = await svc.ListItemsAsync();

            Assert.IsTrue(list.Any(i => i.Id == item.Id && i.Name == "Test"));
        }
    }
}
