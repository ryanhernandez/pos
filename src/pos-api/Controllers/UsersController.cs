using Microsoft.AspNetCore.Mvc;
using Server.Modules.Users.Domain;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.ListUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _service.GetUserAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Server.Modules.Users.GraphQL.CreateUserInput input)
        {
            var user = await _service.CreateUserAsync(input.Username, input.Email, input.DisplayName);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Server.Modules.Users.GraphQL.UpdateUserInput input)
        {
            var updated = await _service.UpdateUserAsync(input.Id, input.Username, input.Email, input.DisplayName);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteUserAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }
    }
}
