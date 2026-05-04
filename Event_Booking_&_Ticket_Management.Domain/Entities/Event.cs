using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Domain.Entities
{
    public class Event
    {
        public Guid Id { get; set;  }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public int TotalSeats { get; set; }

    }
}
