using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; } // Forigen key

        public Guid UserId { get; set; } //Forigen key

        public int NumberOfSeats { get; set; }

        public DateTime BookingDate { get; set; }

        public string status { get; set; }

        public Event Event { get; set; }
        public User user { get; set; }
    }
}
