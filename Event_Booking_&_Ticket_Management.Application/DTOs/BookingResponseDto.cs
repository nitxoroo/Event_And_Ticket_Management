using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.DTOs
{
    public class BookingResponseDto
    {
      public Guid BookingId { get; set; }
      public Guid EventId { get; set; }
      public Guid UserId { get; set; }
      public int NoOfSeats { get; set; }
      public string BookingDate { get; set; }
      public string Status { get; set; }

    }
}
