using Event_Booking___Ticket_Management.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Domain.RepoContract
{
    public interface IBookingRepo
    {
        Task<Booking> BookTicketAsync(Booking booking);
        Task<List<Booking>> GetMyBookingAsync(Guid userId);
        IQueryable<Booking> GetAllBookingAsync();

        Task<Booking> CancelBookingAsync(Guid BookingId);
        Task<Booking> FindById(Guid id);

        
    }
}
