using Event_Booking___Ticket_Management.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Auth.Command
{
    public class LoginCommand:IRequest<AuthResponseDto>
    {
        
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
