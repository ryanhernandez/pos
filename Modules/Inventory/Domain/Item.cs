using Server.Shared.Kernel;
using System.ComponentModel.DataAnnotations;

namespace Server.Modules.Inventory.Domain
{
    public class Item : EntityBase
    {
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }

        // EF Core requires parameterless constructor
        private Item() { }

        public Item(string name, decimal price, int quantity, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required", nameof(name));
            if (price < 0) throw new ArgumentOutOfRangeException(nameof(price));
            if (quantity < 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            Name = name;
            Price = price;
            Quantity = quantity;
            Description = description;
        }

        public void Update(string name, decimal price, int quantity, string? description = null)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required", nameof(name));
            if (price < 0) throw new ArgumentOutOfRangeException(nameof(price));
            if (quantity < 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            Name = name;
            Price = price;
            Quantity = quantity;
            Description = description;
        }
    }
}
