using Event_Booking___Ticket_Management.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Bookings.Command
{
    public class BookTicketCommand:IRequest<BookingResponseDto>
    {
        public Guid EventId { get; set; }
        public int Seats {  get; set; }
    }
}
