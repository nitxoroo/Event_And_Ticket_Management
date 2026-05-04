using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.DTOs
{
    public class EventResponseDto
    {
        public Guid EventId {  get; set; }
        public String Title { get; set; }
        public string Description {  get; set; }
        public string Date {  get; set; }
        public string Location { get; set; }
        public int TotalSeats {  get; set; }
    }
}
