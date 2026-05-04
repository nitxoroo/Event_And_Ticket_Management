using Event_Booking___Ticket_Management.Application.DTOs;
using Event_Booking___Ticket_Management.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.ServicesContract
{
    public interface IEventServices
    {
        Task<Event> CreateEvent(CreateEventDto dto);
        IQueryable<Event> GetAllEventsQueryable();
        Task<Event> GetEventById(Guid id);
    }
}
