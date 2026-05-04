using Event_Booking___Ticket_Management.Application.DTOs;
using Event_Booking___Ticket_Management.Application.ServicesContract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Auth.Command.Handler
{
    public class RegisterCommandHandler:IRequestHandler<RegisterCommand,RegisterResponseDto>
    {
        private readonly IAuthServices _authServices;
        public RegisterCommandHandler(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        public async Task<RegisterResponseDto> Handle(RegisterCommand command, CancellationToken cancellationToken) {
            RegisterDto newRegister = new RegisterDto
            {
                Name = command.Name,
                Email = command.Email,
                Password = command.password
            };
            return await _authServices.Register(newRegister);
        
        }
    }
}
