using Event_Booking___Ticket_Management.Application.DTOs;
using Event_Booking___Ticket_Management.Application.ServicesContract;
using Event_Booking___Ticket_Management.Domain.RepoContract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Bookings.Command.Handler
{
    public class BookTicketCmdHandler:IRequestHandler<BookTicketCommand,BookingResponseDto>
    {
        private readonly IBookingServices _bookingServices;
        public BookTicketCmdHandler(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        public async Task<BookingResponseDto> Handle(BookTicketCommand request, CancellationToken cancellation)
        {
            BookTicketDto newBooking = new BookTicketDto
            {
                EventId = request.EventId,
                Seats = request.Seats
            };

            var result = await _bookingServices.BookTickt(newBooking);
            return new BookingResponseDto
            {
                BookingId = result.Id,
                EventId = result.EventId,
                UserId = result.UserId,
                NoOfSeats = result.NumberOfSeats,
                BookingDate = result.BookingDate.ToString("yyyy-MM-dd"),
                Status = result.status
            };
        }
    }
}
