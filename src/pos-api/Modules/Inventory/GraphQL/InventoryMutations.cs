using Server.Modules.Inventory.Domain;

namespace Server.Modules.Inventory.GraphQL
{
    public class AddItemInput
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateItemInput
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class InventoryMutations
    {
        private readonly InventoryService _service;

        public InventoryMutations(InventoryService service)
        {
            _service = service;
        }

        public async Task<Item> AddItem(AddItemInput input)
        {
            return await _service.CreateItemAsync(input.Name, input.Price, input.Quantity, input.Description);
        }

        public async Task<Item?> UpdateItem(UpdateItemInput input)
        {
            return await _service.UpdateItemAsync(input.Id, input.Name, input.Price, input.Quantity, input.Description);
        }

        public async Task<bool> DeleteItem(Guid id)
        {
            return await _service.DeleteItemAsync(id);
        }
    }
}
