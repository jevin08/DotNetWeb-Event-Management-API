using EventHandleApi.Models;
using EventHandleApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserHandleApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService) =>
            _userService = userService;

        [HttpGet("All")]
        public async Task<List<User>> Get() =>
            await _userService.GetAsync();

        [HttpPost("Add")]
        public async Task<IActionResult> Post(User newUser)
        {
            await _userService.CreateAsync(newUser);

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var evnt = await _userService.GetAsync(id);

            if (evnt is null)
            {
                return NotFound();
            }

            return Ok(evnt);
        }

        [HttpPut("Update/{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody]User updatedUser)
        {
            var user = await _userService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            updatedUser.Id = user.Id;
            await _userService.UpdateAsync(id, updatedUser);
            return Ok(updatedUser);
        }

        [HttpDelete("Delete/{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var evnt = await _userService.GetAsync(id);

            if (evnt is null)
            {
                return NotFound();
            }

            await _userService.RemoveAsync(id);

            return NoContent();
        }
    }
}
