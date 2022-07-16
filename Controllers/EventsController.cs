using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Authorization;
using WebApi.Models.Event;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : BaseController
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAsync()
        {
            var cities = await _eventService.getAll();
            return Ok(cities);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetAsync(int id)
        {
            var cities = await _eventService.getById(id);
            return Ok(cities);
        }

        [HttpPost]
        public async Task<ActionResult<Event>> addAsync([FromBody] EventDto eventDto)
        {
            Event result = null;
            if (ModelState.IsValid)
            {
                result = await _eventService.addAsync(eventDto, Account);
            }
            return Ok(result);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Event>> updateAsync(int id, [FromBody] EventDto eventDto)
        {
            Event result = null;
            if (ModelState.IsValid)
            {
                result = await _eventService.updateAsync(id, eventDto, Account);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> deleteAsync(int id)
        {
            Event result = await _eventService.deleteAsync(id, Account);
            return Ok(result);
        }
    }
}
