using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net;
using System.Net.Http.Json;
using TicketSystem.API.Tests;
using TicketSystem.Application.Tickets.Dtos;
using TicketSystem.Domain.Repositories;
using TicketSystem.Domain.Entities;
using Xunit;
namespace TicketSystem.API.Controllers.Tests;
public class TicketsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<ITicketRepository> _ticketRepositoryMock = new();
    public TicketsControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        // arrange
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();
                services.Replace(ServiceDescriptor.Scoped(_ => _ticketRepositoryMock.Object));
            });
        });
    }
    [Fact]
    public async Task GetTickets_ForValidRequest_ReturnsTickets()
    {
        // arrange
        var client = _factory.CreateClient();

        // act
        var response = await client.GetAsync("/api/tickets");

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    [Fact]
    public async Task CreateTicket_ForValidRequest_ReturnsCreatedResult()
    {
        // arrange
        _ticketRepositoryMock
            .Setup(x => x.AddAsync(It.IsAny<Ticket>()))
            .ReturnsAsync(new Ticket { Id = 1 });
        var client = _factory.CreateClient();
        var dto = new CreateTicketDto
        {
            Title = "Test Ticket 2",
            Description = "Test Description 2"
        };

        // act
        var response = await client.PostAsJsonAsync("/api/tickets", dto);

        // assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var location = response.Headers.Location;
        location.Should().NotBeNull();
        _ticketRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Ticket>()), Times.Once);
    }
}