using Event_Booking___Ticket_Management.Application.DTOs;
using Event_Booking___Ticket_Management.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.ServicesContract
{
    public interface IBookingServices
    {

        Task<Booking> BookTickt( BookTicketDto dto);
        Task<List<Booking>> GetMyBooking();
        IQueryable<Booking> GetAllBooking();
        Task<Booking> CancelBooking(Guid id);
        

   
    }
}
