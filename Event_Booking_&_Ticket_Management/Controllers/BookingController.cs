using Event_Booking___Ticket_Management.Application.Feature.Bookings.Command;
using Event_Booking___Ticket_Management.Application.Feature.Bookings.Querry;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Event_Booking___Ticket_Management.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {

        private IMediator  _mediator;

        public BookingController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        [HttpPost("Booking")]
        public async Task<IActionResult> BookTicket(BookTicketCommand command)
        {
           var result= await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("bookings")]
        public async Task<IActionResult> GetAllBookings([FromQuery] GetAllBookingQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetMyBooking")]
        public async Task<IActionResult> GetMyBookings()
        {
            var result = await _mediator.Send(new GetMyBookingQuery());

            return Ok(result);
        }

        [HttpDelete("Cancel")]
        public async Task<IActionResult> CancelBooking(CancelBookingCommand command) 
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
