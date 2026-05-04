using Event_Booking___Ticket_Management.Application.DTOs;
using Event_Booking___Ticket_Management.Application.ServicesContract;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Bookings.Command.Handler
{
    public class CancelBookingCmdHandler:IRequestHandler<CancelBookingCommand,BookingResponseDto>
    {
        private readonly IBookingServices _bookingServices;
        public CancelBookingCmdHandler(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        public async Task<BookingResponseDto> Handle(CancelBookingCommand request,CancellationToken cancellationToken)
        {
            var result = await _bookingServices.CancelBooking(request.Id);

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
