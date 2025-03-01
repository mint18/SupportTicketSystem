using TicketSystem.Infrastructure.Extensions;
using TicketSystem.Application.Extensions;
using TicketSystem.API.Middlewares;
using Serilog;
using TicketSystem.Domain.Repositories;
using TicketSystem.Infrastructure.Repositories;
using TicketSystem.Domain.Entities;
using Microsoft.OpenApi.Models;
using TicketSystem.API.Extensions;

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

app.UseHttpsRedirection();

//added apps
app.UseMiddleware<RequestLoggingMiddleware>();

app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<ApplicationUser>();

app.UseAuthorization();

app.MapControllers();

app.Run();
