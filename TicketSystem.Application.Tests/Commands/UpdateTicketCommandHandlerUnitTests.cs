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

public class UpdateTicketStatusCommandHandlerUnitTests
{
    private readonly Mock<ITicketRepository> _ticketRepositoryMock;
    private readonly Mock<IStatusRepository> _statusRepositoryMock;
    private readonly UpdateTicketStatusCommandHandler _handler;

    public UpdateTicketStatusCommandHandlerUnitTests()
    {
        _ticketRepositoryMock = new Mock<ITicketRepository>();
        _statusRepositoryMock = new Mock<IStatusRepository>();
        _handler = new UpdateTicketStatusCommandHandler(_ticketRepositoryMock.Object, _statusRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WhenTicketAndStatusExist_UpdatesTicketStatus()
    {
        // arrange
        var ticketId = 1;
        var statusId = 2;
        var command = new UpdateTicketStatusCommand(ticketId, statusId);

        var existingTicket = new Ticket { Id = ticketId, Title = "Test Ticket" };
        var newStatus = new Status { Id = statusId, StatusName = "In Progress" };

        _ticketRepositoryMock.Setup(r => r.GetByIdAsync(ticketId)).ReturnsAsync(existingTicket);
        _statusRepositoryMock.Setup(r => r.GetByIdAsync(statusId)).ReturnsAsync(newStatus);

        // act
        await _handler.Handle(command, CancellationToken.None);

        // assert
        existingTicket.Status.Should().Be(newStatus);
        _ticketRepositoryMock.Verify(r => r.UpdateAsync(existingTicket), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenTicketDoesNotExist_ThrowsKeyNotFoundException()
    {
        // arrange
        var ticketId = 999;
        var statusId = 2;
        var command = new UpdateTicketStatusCommand(ticketId, statusId);

        _ticketRepositoryMock.Setup(r => r.GetByIdAsync(ticketId)).ReturnsAsync((Ticket)null);

        // act assert
        await _handler.Invoking(h => h.Handle(command, CancellationToken.None))
            .Should()
            .ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Ticket with ID {ticketId} not found.");

        _ticketRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Ticket>()), Times.Never);
    }

    [Fact]
    public async Task Handle_WhenStatusDoesNotExist_ThrowsKeyNotFoundException()
    {
        // arrange
        var ticketId = 1;
        var statusId = 999;
        var command = new UpdateTicketStatusCommand(ticketId, statusId);

        var existingTicket = new Ticket { Id = ticketId, Title = "Test Ticket" };

        _ticketRepositoryMock.Setup(r => r.GetByIdAsync(ticketId)).ReturnsAsync(existingTicket);
        _statusRepositoryMock.Setup(r => r.GetByIdAsync(statusId)).ReturnsAsync((Status)null);

        // act assert
        await _handler.Invoking(h => h.Handle(command, CancellationToken.None))
            .Should()
            .ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Status with ID {statusId} not found.");

        _ticketRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Ticket>()), Times.Never);
    }
}