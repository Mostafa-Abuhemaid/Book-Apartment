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
using Mapster;
using MapsterMapper;
using Web.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Web.Application.Hubs;
using Microsoft.AspNetCore.SignalR;
using Web.Application.Helpers;
using Microsoft.AspNetCore.Diagnostics;

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

            #region Jwt Service
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"])),

                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                // ?????? ?? JWT ?? Query String ?? SignalR
                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chatHub"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            #endregion

            #region Mediator Service
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly);
            });
            #endregion

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly);
            builder.Services.AddMapster();
            TypeAdapterConfig.GlobalSettings.Scan(typeof(PropertyMapping).Assembly);

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddSingleton<IUserIdProvider, NameUserIdProvider>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSignalR();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            });

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials()
                          .SetIsOriginAllowed(_ => true);
                });
            });

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "malaz-f18ca-firebase-adminsdk-fbsvc-06b91e997d.json")),
            });

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Global error logging middleware
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

                        if (exceptionHandlerPathFeature?.Error != null)
                        {
                            logger.LogError(exceptionHandlerPathFeature.Error,
                                "Global exception caught at path: {Path}",
                                exceptionHandlerPathFeature.Path);
                        }

                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Internal Server Error");
                    });
                });
            }

            // Swagger
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.UseStaticFiles();

            // SignalR Hub
            app.MapHub<ChatHub>("/chatHub");

            app.Run();
        }
    }
}
