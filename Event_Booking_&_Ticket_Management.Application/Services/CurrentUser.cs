using Event_Booking___Ticket_Management.Application.ServicesContract;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Event_Booking___Ticket_Management.Application.Services
{
    public class CurrentUser:ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;

                if (user == null || !user.Identity!.IsAuthenticated)
                    return null;

                var claim = user.FindFirst(ClaimTypes.NameIdentifier);

                if (claim == null)
                    return null;

                return Guid.Parse(claim.Value);
            }
        }
        public bool IsInRole(string role)
        {
            return _httpContextAccessor.HttpContext?.User?.IsInRole(role) ?? false;
        }
    }
}
