using Event_Booking___Ticket_Management.Application.DTOs;
using Event_Booking___Ticket_Management.Application.ServicesContract;
using Event_Booking___Ticket_Management.Domain.Entities;
using Event_Booking___Ticket_Management.Domain.RepoContract;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Services
{
    public class BookingSevices:IBookingServices
    {
        private readonly IBookingRepo _bookingRepo;
        private readonly ICurrentUser _currentUser;
        private readonly IEventRepo _eventRepo;
        public BookingSevices(IBookingRepo bookingRepo,ICurrentUser currentUser,
            IEventRepo eventRepo
            ) {
            _bookingRepo = bookingRepo;
            _currentUser = currentUser;
            _eventRepo = eventRepo;
        }

        public async Task<Booking> BookTickt(BookTicketDto dto)
        {
            var eventItem = await _eventRepo.GetEventByIdAsync(dto.EventId);
            if (eventItem == null) throw new Exception("Event not found");

            
            if (DateTime.Parse(eventItem.Date )< DateTime.Now) throw new Exception("Event passed");
            if (eventItem.TotalSeats < dto.Seats) throw new Exception("Not enough seats");

          
            eventItem.TotalSeats -= dto.Seats;

      
            Booking newBooking = new Booking
            {
                Id = Guid.NewGuid(),
                EventId = dto.EventId,
                UserId = GetCurrentUserId(),
                NumberOfSeats = dto.Seats,
                BookingDate = DateTime.Now,
                status = "Active"
            };

            return await _bookingRepo.BookTicketAsync(newBooking);
        }

        public async Task<List<Booking>> GetMyBooking()
        {

            var userId =GetCurrentUserId();
            
            var result = await _bookingRepo.GetMyBookingAsync(userId);
            if (result == null) throw new Exception("There are no Bookings Currently");

            return result;
        }
        public IQueryable<Booking> GetAllBooking()
        {
            var result = _bookingRepo.GetAllBookingAsync();
            if (result == null) throw new Exception("There are No Bookings");

            return result;
        }

        public async Task<Booking> CancelBooking(Guid id)
        {
            var booking = await _bookingRepo.FindById(id);
            if (booking == null) throw new Exception("Booking not found");

            if (booking.status == "Cancelled")
            {
                throw new InvalidOperationException("This booking is already cancelled.");
            }

            var userId = GetCurrentUserId();

            
            var isAdmin = _currentUser.IsInRole("Admin");

            
            if (!isAdmin && booking.UserId != userId)
            {
                throw new Exception("Cannot delete bookings that are not yours.");
            }

            var eventItem = await _eventRepo.GetEventByIdAsync(booking.EventId);
            eventItem.TotalSeats += booking.NumberOfSeats;

            return await _bookingRepo.CancelBookingAsync(id);
        }



        private Guid GetCurrentUserId()
        {
            if (_currentUser.UserId == null)
                throw new UnauthorizedAccessException("User not authenticated");

            return _currentUser.UserId.Value;
        }

        
    }
}



