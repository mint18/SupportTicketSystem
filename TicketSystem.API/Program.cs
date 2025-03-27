using TicketSystem.Infrastructure.Extensions;
using TicketSystem.Application.Extensions;
using TicketSystem.API.Middlewares;
using Serilog;
using TicketSystem.Domain.Repositories;
using TicketSystem.Infrastructure.Repositories;
using TicketSystem.Domain.Entities;
using Microsoft.OpenApi.Models;
using TicketSystem.API.Extensions;
using TicketSystem.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication();
builder.Services.AddSwaggerWithAuth();

//added services
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer();
builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddScoped<ICommentRepository, CommentRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Dodaj przed app.UseHttpsRedirection();
app.UseCors(policy =>
    policy.WithOrigins("http://localhost:5173")
          .AllowAnyMethod()
          .AllowAnyHeader());

app.UseHttpsRedirection();

// seeders

using var scope = app.Services.CreateScope();
var roleSeeder = scope.ServiceProvider.GetRequiredService<IRoleSeeder>();
var statusSeeder = scope.ServiceProvider.GetRequiredService<IStatusSeeder>();
await roleSeeder.SeedRolesAsync();
await statusSeeder.SeedStatusesAsync();

//added apps
app.UseMiddleware<RequestLoggingMiddleware>();

app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<ApplicationUser>();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }