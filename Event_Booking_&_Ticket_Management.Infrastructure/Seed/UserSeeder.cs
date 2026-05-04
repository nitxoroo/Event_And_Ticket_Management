using Event_Booking___Ticket_Management.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Infrastructure.Seed
{
    public class UserSeeder
    {
        public static async Task SeedRoles(RoleManager<IdentityRole<Guid>> roleManager)
        {
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>
                    {
                        Name = role
                    });
                }
            }
        }

        public static async Task SeedUser(UserManager<User> userManager)
        {
            var email = "JohnSnow@gmail.com";

            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = email,
                    Email = email,
                    Name = "John Snow"
                };

                await userManager.CreateAsync(user, "JohnSnow@123");

                await userManager.AddToRoleAsync(user, "User");
            }
        }
    }
}


