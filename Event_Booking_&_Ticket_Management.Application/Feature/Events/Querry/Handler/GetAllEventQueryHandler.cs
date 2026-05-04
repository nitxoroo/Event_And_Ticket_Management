using Event_Booking___Ticket_Management.Application.Common.Pagination;
using Event_Booking___Ticket_Management.Application.DTOs;
using Event_Booking___Ticket_Management.Application.ServicesContract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Event_Booking___Ticket_Management.Application.Feature.Events.Querry.Handler
{
    public class GetAllEventQueryHandler
    : IRequestHandler<GetAllEventsQuery, PagedResult<EventResponseDto>>
    {
        private readonly IEventServices _eventServices;

        public GetAllEventQueryHandler(IEventServices eventServices)
        {
            _eventServices = eventServices;
        }

        public async Task<PagedResult<EventResponseDto>> Handle(
            GetAllEventsQuery request,
            CancellationToken cancellationToken)
        {
            var query = _eventServices.GetAllEventsQueryable();

            // 🔍 FILTERING
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(e => e.Title.Contains(request.Search));
            }

            if (request.FromDate.HasValue)
            {
                query = query.Where(e => DateTime.Parse(e.Date) >= request.FromDate.Value);
            }

            if (request.ToDate.HasValue)
            {
                query = query.Where(e => DateTime.Parse(e.Date) <= request.ToDate.Value);
            }

            // 🔽 SORTING
            query = request.SortBy?.ToLower() switch
            {
                "title" => request.Desc
                    ? query.OrderByDescending(e => e.Title)
                    : query.OrderBy(e => e.Title),

                "date" => request.Desc
                    ? query.OrderByDescending(e => DateTime.Parse(e.Date))
                    : query.OrderBy(e => DateTime.Parse(e.Date)),

                _ => query.OrderBy(e => e.Id)
            };

            // 📊 COUNT
            var totalCount = await query.CountAsync(cancellationToken);

            // 📄 PAGINATION + MAPPING
            var data = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(e => new EventResponseDto
                {
                    EventId = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Date = e.Date,
                    Location = e.Location,
                    TotalSeats = e.TotalSeats
                })
                .ToListAsync(cancellationToken);

            return new PagedResult<EventResponseDto>
            {
                Data = data,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
