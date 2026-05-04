using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.DTOs
{
    public class BookTicketDto
    {
        public Guid EventId { get; set; }
       
        public int Seats { get; set; }
        
    }
}
