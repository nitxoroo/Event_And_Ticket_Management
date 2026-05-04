using Event_Booking___Ticket_Management.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Infrastructure.Seed
{
    public static class AdminSeeder
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

        public static async Task SeedAdmin(UserManager<User> userManager)
        {
            var email = "admin@test.com";

            var admin = await userManager.FindByEmailAsync(email);

            if (admin == null)
            {
                admin = new User
                {
                    Id = Guid.NewGuid(),
                    UserName = email,
                    Email = email,
                    Name = "Admin"
                };

                await userManager.CreateAsync(admin, "Admin@123");

                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
