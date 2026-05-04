using Event_Booking___Ticket_Management.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Events.Querry
{
    public class GetEventByIdQuery:IRequest<EventResponseDto>
    {
        public Guid Id { get; set; }
    }
}
