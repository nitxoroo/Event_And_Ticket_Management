using Event_Booking___Ticket_Management.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Events.Command
{
    public class CreateEventCommand:IRequest<EventResponseDto>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Date {  get; set; }
        public string Location { get; set; }
        public int TotalSeats { get; set;  } 
    }
}
