using Event_Booking___Ticket_Management.Application.Feature.Events.Command;
using Event_Booking___Ticket_Management.Application.Feature.Events.Querry;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Booking___Ticket_Management.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IMediator _mediator;
        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles ="Admin")]
        [HttpPost("Add")]
        public async Task<IActionResult> CreateEvent(CreateEventCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllEvents()
        {
            var result = await _mediator.Send(new GetAllEventsQuery());
            return Ok(result);
        }

        [HttpGet("EventById")]
        public async Task<IActionResult> GetEventById(GetEventByIdQuery query)
        {
            var result =  await _mediator.Send(query);
            return Ok(result);
        }

        

    }
}
