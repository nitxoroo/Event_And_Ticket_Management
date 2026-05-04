using Event_Booking___Ticket_Management.Application.DTOs;
using Event_Booking___Ticket_Management.Application.ServicesContract;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.Feature.Auth.Command.Handler
{
    public class LoginCommandHandler:IRequestHandler<LoginCommand,AuthResponseDto>
    {
        private readonly IAuthServices _authServices;
        public LoginCommandHandler(IAuthServices authServices)
        {
            _authServices = authServices;
        }

        public async Task<AuthResponseDto> Handle(LoginCommand request,CancellationToken cancellationToken)
        {
            LoginDto newlogin = new LoginDto
            {
                Email = request.Email,
                Password = request.Password
            };
            var result = await _authServices.Login(newlogin);
            return result;
        }
    }
}
