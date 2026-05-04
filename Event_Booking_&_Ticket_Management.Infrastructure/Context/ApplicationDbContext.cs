using Event_Booking___Ticket_Management.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event_Booking___Ticket_Management.Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.user)
                .WithMany(u => u.bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);

           
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Event)
                .WithMany()
                .HasForeignKey(b => b.EventId)
                .OnDelete(DeleteBehavior.Cascade);

          
            modelBuilder.Entity<User>()
                .HasMany(u => u.events)
                .WithMany()
                .UsingEntity<Booking>(
                    j => j.HasOne(b => b.Event).WithMany().HasForeignKey(b => b.EventId),
                    j => j.HasOne(b => b.user).WithMany(u => u.bookings).HasForeignKey(b => b.UserId)
                );
        }
    }
}
