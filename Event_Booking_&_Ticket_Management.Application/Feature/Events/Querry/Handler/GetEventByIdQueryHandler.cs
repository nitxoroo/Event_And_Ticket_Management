using Event_Booking___Ticket_Management.Application.DTOs;
using Event_Booking___Ticket_Management.Application.ServicesContract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Events.Querry.Handler
{
    public class GetEventByIdQueryHandler:IRequestHandler<GetEventByIdQuery,EventResponseDto>
    {
        private readonly IEventServices _eventServices;
        public GetEventByIdQueryHandler(IEventServices eventServices)
        {
            _eventServices = eventServices;
        }

        public async Task<EventResponseDto> Handle(GetEventByIdQuery request,CancellationToken cancellationToken)
        {
            var result = await _eventServices.GetEventById(request.Id);

            return new EventResponseDto
            {
                EventId = result.Id,
                Title = result.Title,
                Description = result.Description,
                Date = result.Date,
                Location = result.Location,
                TotalSeats = result.TotalSeats
            };
        }
    }
}
