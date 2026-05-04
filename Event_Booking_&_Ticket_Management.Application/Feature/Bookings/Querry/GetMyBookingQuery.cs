using Event_Booking___Ticket_Management.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Bookings.Querry
{
    public class GetMyBookingQuery:IRequest<List<BookingResponseDto>>
    {
    }
}
