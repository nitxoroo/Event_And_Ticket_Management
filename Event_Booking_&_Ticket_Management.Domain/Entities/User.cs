using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Domain.Entities
{
    public class User:IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }



        public List<Event> events { get; set; }
        public List<Booking> bookings { get; set; }
    }
}
