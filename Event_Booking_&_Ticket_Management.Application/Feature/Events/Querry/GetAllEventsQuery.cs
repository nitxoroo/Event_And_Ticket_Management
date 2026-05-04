using Event_Booking___Ticket_Management.Application.Common.Pagination;
using Event_Booking___Ticket_Management.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Events.Querry
{
    public class GetAllEventsQuery : IRequest<PagedResult<EventResponseDto>>
    {
        // 🔍 Filtering
        public string? Search { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        // 🔽 Sorting
        public string? SortBy { get; set; } = "Date";
        public bool Desc { get; set; } = false;

        // 📄 Pagination
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
