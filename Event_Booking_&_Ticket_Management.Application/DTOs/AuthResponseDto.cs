using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.DTOs
{
    public class AuthResponseDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string JwtToken { get; set; }
        public DateTime JwtTokenExpirationTime { get; set; }
        public string RefreshToken { get; set; }
        public string RefreshTokenExpirationTime { get; set; }

    }
}
