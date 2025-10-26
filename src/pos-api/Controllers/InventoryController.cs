using Microsoft.AspNetCore.Mvc;
using Server.Modules.Inventory.Domain;
using Server.Modules.Inventory.GraphQL;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _service;

        public InventoryController(InventoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.ListItemsAsync();
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var item = await _service.GetItemAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddItemInput input)
        {
            var item = await _service.CreateItemAsync(input.Name, input.Price, input.Quantity, input.Description);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateItemInput input)
        {
            var updated = await _service.UpdateItemAsync(input.Id, input.Name, input.Price, input.Quantity, input.Description);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteItemAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
