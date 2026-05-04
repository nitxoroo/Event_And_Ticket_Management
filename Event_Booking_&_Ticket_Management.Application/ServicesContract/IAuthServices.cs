using Event_Booking___Ticket_Management.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.ServicesContract
{
    public interface IAuthServices
    {
        Task<RegisterResponseDto> Register(RegisterDto dto);
        Task<AuthResponseDto> Login(LoginDto login);

        Task<AuthResponseDto> RefreshToken(string RefershToken);
    }
}
