using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Application.ServicesContract
{
    public interface ICurrentUser
    {
        Guid? UserId { get;}
        bool IsInRole(string role);
    }
}
