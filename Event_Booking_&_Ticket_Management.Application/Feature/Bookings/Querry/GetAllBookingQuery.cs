using Event_Booking___Ticket_Management.Application.Common.Pagination;
using Event_Booking___Ticket_Management.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Bookings.Querry
{
    public class GetAllBookingQuery : IRequest<PagedResult<BookingResponseDto>>
    {
        // 🔍 Filtering
        public Guid? EventId { get; set; }
        public string? Status { get; set; } // Active / Cancelled

        // 🔽 Sorting
        public string? SortBy { get; set; } = "BookingDate";
        public bool Desc { get; set; } = false;

        // 📄 Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
