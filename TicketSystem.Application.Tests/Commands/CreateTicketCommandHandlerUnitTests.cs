using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TicketSystem.Application.Tickets.Commands;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Repositories;
using Xunit;

namespace TicketSystem.Application.Tests.Tickets.Commands;

public class CreateTicketCommandHandlerTests
{
    private readonly Mock<ITicketRepository> _ticketRepositoryMock;
    private readonly Mock<IStatusRepository> _statusRepositoryMock;
    private readonly CreateTicketCommandHandler _handler;

    public CreateTicketCommandHandlerTests()
    {
        _ticketRepositoryMock = new Mock<ITicketRepository>();
        _statusRepositoryMock = new Mock<IStatusRepository>();
        _handler = new CreateTicketCommandHandler(_ticketRepositoryMock.Object, _statusRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidData_CreatesTicketWithProperValues()
    {
        // arrange
        var command = new CreateTicketCommand("Test Title", "Test Description");
        var openStatus = new Status { Id = 1, StatusName = "Open" };
        var createdTicket = new Ticket { 
            Id = 42,
            Title = command.Title,
            Description = command.Description,
            Status = openStatus
        };

        _statusRepositoryMock
            .Setup(repo => repo.GetByNameAsync("Open"))
            .ReturnsAsync(openStatus);

        _ticketRepositoryMock
            .Setup(repo => repo.AddAsync(It.IsAny<Ticket>()))
            .ReturnsAsync(createdTicket);

        // act
        var result = await _handler.Handle(command, CancellationToken.None);

        // assert
        result.Should().Be(42); 

        _ticketRepositoryMock.Verify(repo => repo.AddAsync(It.Is<Ticket>(t =>
            t.Title == command.Title &&
            t.Description == command.Description &&
            t.Status == openStatus &&
            t.CreatedDate != default
        )), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenOpenStatusDoesNotExist_ThrowsException()
    {
        // arrange
        var command = new CreateTicketCommand("Test Title", "Test Description");

        _statusRepositoryMock
            .Setup(repo => repo.GetByNameAsync("Open"))
            .ReturnsAsync((Status)null);

        // act
        await _handler.Invoking(h => h.Handle(command, CancellationToken.None))
            .Should()
            .ThrowAsync<Exception>()
            .WithMessage("Status 'Open' nie istnieje.");

        // assert
        _ticketRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Ticket>()), Times.Never);
    }
}