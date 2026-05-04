using Event_Booking___Ticket_Management.Application.DTOs;
using Event_Booking___Ticket_Management.Application.ServicesContract;
using Event_Booking___Ticket_Management.Domain.Entities;
using Event_Booking___Ticket_Management.Domain.RepoContract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Services
{
    public class EventServices:IEventServices
    {
        private readonly IEventRepo _eventRepo;
        public EventServices(IEventRepo eventRepo) { _eventRepo = eventRepo; }

        public async Task<Event> CreateEvent(CreateEventDto dto)
        {
            Event newEvent = new Event
            {
                Id = Guid.NewGuid(),
                Title = dto.Title,
                Description = dto.Description,
                Date = dto.Date,
                Location = dto.Location,
                TotalSeats = dto.TotalSeats,
            };
           return await _eventRepo.CreateEventAsync(newEvent);
            
        }
        public IQueryable<Event> GetAllEventsQueryable()
        {
            var result = _eventRepo.GetAllEventsAsync();
            if (result == null) throw new Exception("There is No Event");

            return result;
        }

        public async Task<Event> GetEventById(Guid id)
        {
            var result = await _eventRepo.GetEventByIdAsync(id);
            if (result == null) throw new Exception("Cannot find Event of this Id");

            return result;
        }
    }
}
