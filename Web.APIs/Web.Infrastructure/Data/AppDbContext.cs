using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Domain.Entites;

namespace Web.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {


        }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyReview> PropertyReviews { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Property>()
            .HasMany(p => p.Images)
            .WithOne(i => i.Property)
            .HasForeignKey(i => i.PropertyId);
            ////////////////
            builder.Entity<Favorite>()
        .HasKey(f => new { f.UserId, f.PropertyId });
            ////////////////
            builder.Entity<Favorite>()
          .HasOne(f => f.User)
          .WithMany(f=>f.Favorites)
          .HasForeignKey(f => f.UserId)
           .OnDelete(DeleteBehavior.Restrict); 
            //////////////
            builder.Entity<Favorite>()
                .HasOne(f => f.Property)
                .WithMany()
                .HasForeignKey(f => f.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);
            ///////////////////////
            builder.Entity<PropertyReview>()
     .HasOne(r => r.Property)
     .WithMany(p => p.PropertyReviews) 
     .HasForeignKey(r => r.PropertyId)
     .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PropertyReview>()
                .HasOne(r => r.User)
                .WithMany(u => u.PropertyReviews)
                .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Restrict);
            ////////////////////

            builder.Entity<Appointment>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Appointment>()
                .HasOne(a => a.Property)
                .WithMany()
                .HasForeignKey(a => a.PropertyId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ChatMessage>()
                .HasIndex(c => new { c.SenderUserId, c.ReceiverUserId, c.CreatedAt });

            builder.Entity<ChatMessage>()
             .HasIndex(c => new { c.ReceiverUserId, c.SenderUserId, c.CreatedAt });
            

        }
    }
}
