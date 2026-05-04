using Event_Booking___Ticket_Management.Application.DTOs;
using Event_Booking___Ticket_Management.Application.ServicesContract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Bookings.Querry.Handler
{
    using Event_Booking___Ticket_Management.Application.Common.Pagination;
    using Microsoft.EntityFrameworkCore;

    public class GetAllBookingQueryHandler
        : IRequestHandler<GetAllBookingQuery, PagedResult<BookingResponseDto>>
    {
        private readonly IBookingServices _bookingServices;

        public GetAllBookingQueryHandler(IBookingServices bookingServices)
        {
            _bookingServices = bookingServices;
        }

        public async Task<PagedResult<BookingResponseDto>> Handle(
            GetAllBookingQuery request,
            CancellationToken cancellationToken)
        {
            var query = _bookingServices.GetAllBooking();

            // ---------------------------
            // 🔍 FILTERING
            // ---------------------------
            if (request.EventId.HasValue)
            {
                query = query.Where(b => b.EventId == request.EventId.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                query = query.Where(b => b.status.ToLower() == request.Status.ToLower());
            }

            // ---------------------------
            // 🔽 SORTING
            // ---------------------------
            query = request.SortBy?.ToLower() switch
            {
                "bookingdate" => request.Desc
                    ? query.OrderByDescending(b => b.BookingDate)
                    : query.OrderBy(b => b.BookingDate),

                "seats" => request.Desc
                    ? query.OrderByDescending(b => b.NumberOfSeats)
                    : query.OrderBy(b => b.NumberOfSeats),

                _ => query.OrderBy(b => b.Id)
            };

            // ---------------------------
            // 📊 TOTAL COUNT
            // ---------------------------
            var totalCount = await query.CountAsync(cancellationToken);

            // ---------------------------
            // 📄 PAGINATION + MAPPING
            // ---------------------------
            var data = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(r => new BookingResponseDto
                {
                    BookingId = r.Id,
                    EventId = r.EventId,
                    UserId = r.UserId,
                    NoOfSeats = r.NumberOfSeats,
                    BookingDate = r.BookingDate.ToString("yyyy-MM-dd"),
                    Status = r.status
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<BookingResponseDto>
            {
                Data = data,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
