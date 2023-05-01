using EventHandleApi.DTO;
using EventHandleApi.Models;
using EventHandleApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppUserHandleApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AppUserController : Controller
    {
        private readonly AppUserService _appUserService;

        public AppUserController(AppUserService appUserService) =>
            _appUserService = appUserService;

        [HttpGet("All")]
        public async Task<List<AppUser>> Get() =>
            await _appUserService.GetAsync();

        [HttpPost("Add")]
        public async Task<IActionResult> Post(AppUser newAppUser)
        {
            await _appUserService.CreateAsync(newAppUser);

            return CreatedAtAction(nameof(Get), new { id = newAppUser.Id }, newAppUser);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<AppUser>> Get(string id)
        {
            var user = await _appUserService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("Update/{id:length(24)}")]
        public async Task<IActionResult> Update(string id, AppUser updatedAppUser)
        {
            var user = await _appUserService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            updatedAppUser.Id = user.Id;

            await _appUserService.UpdateAsync(id, updatedAppUser);

            return Ok(updatedAppUser);
        }

        [HttpDelete("Delete/{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _appUserService.GetAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            await _appUserService.RemoveAsync(id);

            return NoContent();
        }

        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO details)
        {
            if(details is null || details.email is null || details.password is null)
            {
                return BadRequest("Insuffisent parameter is passed.");
            }
            var token = await _appUserService.Authenticate(details.email, details.password);

            if (token is null)
            {
                return Unauthorized();
            }

            var user = await _appUserService.FindByEmail(details.email);

            return Ok(new { token, user});
        }
    }
}
