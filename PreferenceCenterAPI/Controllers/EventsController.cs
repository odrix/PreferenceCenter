using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PreferenceCenterAPI.Domain;

namespace PreferenceCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public IActionResult Post(EventsPostRequest newEvents)
        {
            try
            {
                _eventService.AddEvents(newEvents.User.Id, newEvents.Consents);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
