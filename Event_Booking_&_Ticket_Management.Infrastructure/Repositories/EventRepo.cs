using Event_Booking___Ticket_Management.Domain.Entities;
using Event_Booking___Ticket_Management.Domain.RepoContract;
using Event_Booking___Ticket_Management.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Infrastructure.Repositories
{
    public class EventRepo:IEventRepo
    {
        private readonly ApplicationDbContext _context;
        public EventRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Event> CreateEventAsync(Event Event)
        {
            await _context.Events.AddAsync(Event);
            await _context.SaveChangesAsync();

            return Event;
        }

        public IQueryable<Event> GetAllEventsAsync()
        {
            return _context.Events.AsQueryable();
        }

        public async Task<Event> GetEventByIdAsync(Guid id)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
