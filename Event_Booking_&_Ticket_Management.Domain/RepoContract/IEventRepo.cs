using Event_Booking___Ticket_Management.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Domain.RepoContract
{
    public interface IEventRepo
    {
        Task<Event> CreateEventAsync(Event Event);
        IQueryable<Event> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(Guid id);
    }
}
