
using FluentValidation;
using MediatR.NotificationPublishers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Web.Application.DTOs.EmailDTO;
using Web.Application.Interfaces;
using Web.Application.Mapping;
using Web.Domain.Entites;
using Web.Domain.Repositories;
using Web.Infrastructure.Data;
using Web.Infrastructure.Repositories;
using Web.Infrastructure.Service;

namespace Web.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            // Add services to the container.

            builder.Services.AddControllers();
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<AppUser, IdentityRole>()
             .AddRoles<IdentityRole>()
             .AddEntityFrameworkStores<AppDbContext>()
             .AddDefaultTokenProviders();

            builder.Services.Configure<EmailDto>(configuration.GetSection("MailSettings"));
			
			#region Mediator Service
			builder.Services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
				cfg.NotificationPublisher = new TaskWhenAllPublisher();
			});
			#endregion
            builder.Services.AddHttpContextAccessor();
			builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddMemoryCache();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
