using Event_Booking___Ticket_Management.Application.DTOs;
using Event_Booking___Ticket_Management.Application.ServicesContract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Events.Command.Handler
{
    public  class CreateEventCmdHandler:IRequestHandler<CreateEventCommand,EventResponseDto>
    {
        private readonly IEventServices _evenServices;
        public CreateEventCmdHandler(IEventServices evenServices)
        {
            _evenServices = evenServices;
        }

        public async Task<EventResponseDto> Handle(CreateEventCommand request,CancellationToken cancellationToken)
        {
            CreateEventDto newEvent = new CreateEventDto
            {
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                Location = request.Location,
                TotalSeats = request.TotalSeats,
            };
            
            var result = await _evenServices.CreateEvent(newEvent);
            return new EventResponseDto
            {
                EventId = result.Id,
                Title = request.Title,
                Description = request.Description,
                Date = request.Date,
                Location = request.Location,
                TotalSeats = request.TotalSeats,
            };
        }
    }
}
