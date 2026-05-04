using Event_Booking___Ticket_Management.Application.DTOs;
using Event_Booking___Ticket_Management.Application.ServicesContract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Bookings.Querry.Handler
{
    public class GetMyBookingQueryHandler:IRequestHandler<GetMyBookingQuery,List<BookingResponseDto>>
    {
        private readonly IBookingServices _bookingServices;
        public GetMyBookingQueryHandler(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        public async Task<List<BookingResponseDto>> Handle(GetMyBookingQuery request,CancellationToken cancellationToken)
        {
            var result = await _bookingServices.GetMyBooking();
            return result.Select(r=> new BookingResponseDto
            {
                BookingId=r.Id,
                EventId=r.EventId,
                UserId=r.UserId,
                NoOfSeats=r.NumberOfSeats,
                BookingDate=r.BookingDate.ToString("yyyy-MM-dd"),
                Status=r.status
            }).ToList();
        }
    }
}
