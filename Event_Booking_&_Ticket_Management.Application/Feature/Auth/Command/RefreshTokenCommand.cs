using Event_Booking___Ticket_Management.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Auth.Command
{
    public class RefreshTokenCommand:IRequest<AuthResponseDto>
    {
        public string RefreshToken { get; set; }
    }
}
