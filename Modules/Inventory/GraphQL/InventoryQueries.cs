using Server.Modules.Inventory.Domain;

namespace Server.Modules.Inventory.GraphQL
{
    public class InventoryQueries
    {
        private readonly InventoryService _service;

        public InventoryQueries(InventoryService service)
        {
            _service = service;
        }

        public Task<IEnumerable<Item>> GetItems() => _service.ListItemsAsync();

        public Task<Item?> GetItem(Guid id) => _service.GetItemAsync(id);
    }
}
