using Event_Booking___Ticket_Management.Domain.Entities;
using Event_Booking___Ticket_Management.Domain.RepoContract;
using Event_Booking___Ticket_Management.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Infrastructure.Repositories
{
    public class BookingRepo:IBookingRepo
    {
        private readonly ApplicationDbContext _context;
        public BookingRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> BookTicketAsync(Booking booking)
        {
           

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task<List<Booking>> GetMyBookingAsync(Guid userId)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public  IQueryable<Booking> GetAllBookingAsync()
        {
            return _context.Bookings.AsQueryable();
        }

        public async Task<Booking> CancelBookingAsync(Guid BookingId) {
            var booking = await _context.Bookings.FindAsync(BookingId);
            if (booking == null) return null;

            booking.status = "Cancelled";
            await _context.SaveChangesAsync();
            return booking;

        }

        public async Task<Booking> FindById(Guid id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(b=>b.Id == id);
        }

    }
}
