using Event_Booking___Ticket_Management.Application.DTOs;
using Event_Booking___Ticket_Management.Application.Services;
using Event_Booking___Ticket_Management.Application.ServicesContract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Auth.Command.Handler
{
    public class RefreshTokenCommandHandler:IRequestHandler<RefreshTokenCommand , AuthResponseDto>
    {
        private readonly IAuthServices _authServices;
        public RefreshTokenCommandHandler(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        public async Task<AuthResponseDto> Handle(RefreshTokenCommand command, CancellationToken cancellationToken) 
        {
            return await _authServices.RefreshToken(command.RefreshToken);
        }
    }
}
