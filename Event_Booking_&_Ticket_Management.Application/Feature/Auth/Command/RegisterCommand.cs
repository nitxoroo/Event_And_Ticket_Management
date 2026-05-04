using Event_Booking___Ticket_Management.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Auth.Command
{
    public class RegisterCommand:IRequest<RegisterResponseDto>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
    }
}
