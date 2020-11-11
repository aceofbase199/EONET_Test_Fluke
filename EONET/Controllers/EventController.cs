using EONET.BL.Abstraction;
using EONET.BL.Models;
using EONET.Web.Helpers;
using EONET.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EONET.Web.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        [Route("/events")]
        public async Task<IActionResult> GetEventsAsync([FromQuery] PaginationFilter paginationFilter, [FromBody] EventFilterModel filter)
        {
            if (filter == null)
                return BadRequest();

            var models = await _eventService.GetEventsAsync(filter);

            if (models == null)
                return Json(new { status = "error", message = "Couldn't load events" });

            var pagedResponse = PaginationHelper.ToPagedResponse(models, paginationFilter, models.Count);

            return Ok(pagedResponse);
        }

        [HttpGet]
        [Route("/event/{id}")]
        public async Task<IActionResult> GetEventAsync(string id)
        {
            var model = await _eventService.GetEventAsync(id);

            if (model == null)
                return Json(new { status = "error", message = "Couldn't load events" });

            return Ok(model);
        }
    }
}
