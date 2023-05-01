using Microsoft.AspNetCore.Mvc;
using EventHandleApi.Models;
using EventHandleApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace EventHandleApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService) =>
            _eventService = eventService;

        [HttpGet("All")]
        public async Task<List<Event>> Get() =>
            await _eventService.GetAsync();

        [HttpPost("Add")]
        public async Task<IActionResult> Post(Event newEvent)
        {
            await _eventService.CreateAsync(newEvent);

            return CreatedAtAction(nameof(Get), new { id = newEvent.Id }, newEvent);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Event>> Get(string id)
        {
            var evnt = await _eventService.GetAsync(id);

            if (evnt is null)
            {
                return NotFound();
            }

            return Ok(evnt);
        }

        [HttpPut("Update/{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Event updatedEvent)
        {
            var evnt = await _eventService.GetAsync(id);

            if (evnt is null)
            {
                return NotFound();
            }

            updatedEvent.Id = evnt.Id;

            await _eventService.UpdateAsync(id, updatedEvent);

            return Ok(updatedEvent);
        }

        [HttpDelete("Delete/{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var evnt = await _eventService.GetAsync(id);

            if (evnt is null)
            {
                return NotFound();
            }

            await _eventService.RemoveAsync(id);

            return NoContent();
        }
    }
}
