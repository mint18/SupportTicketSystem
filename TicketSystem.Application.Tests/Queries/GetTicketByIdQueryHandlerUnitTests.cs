using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TicketSystem.Application.Tickets.Dtos;
using TicketSystem.Application.Tickets.Queries;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Repositories;
using Xunit;

namespace TicketSystem.Application.Tests.Tickets.Queries;

public class GetTicketByIdQueryHandlerUnitTests
{
    private readonly Mock<ITicketRepository> _ticketRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly GetTicketByIdQueryHandler _handler;

    public GetTicketByIdQueryHandlerUnitTests()
    {
        _ticketRepositoryMock = new Mock<ITicketRepository>();
        _mapperMock = new Mock<IMapper>();
        _handler = new GetTicketByIdQueryHandler(_ticketRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_WhenTicketExists_ReturnsTicketDto()
    {
        // Arrange
        var ticketId = 42;
        var query = new GetTicketByIdQuery(ticketId);

        var ticket = new Ticket
        {
            Id = ticketId,
            Title = "Test Ticket",
            Description = "Test Description",
            Status = new Status { StatusName = "Open" }
        };

        var expectedDto = new TicketResponseDto
        {
            Id = ticketId,
            Title = "Test Ticket",
            Description = "Test Description",
            StatusName = "Open"
        };

        _ticketRepositoryMock.Setup(r => r.GetByIdAsync(ticketId)).ReturnsAsync(ticket);
        _mapperMock.Setup(m => m.Map<TicketResponseDto>(ticket)).Returns(expectedDto);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedDto);
    }

    [Fact]
    public async Task Handle_WhenTicketDoesNotExist_ThrowsKeyNotFoundException()
    {
        // Arrange
        var ticketId = 999;
        var query = new GetTicketByIdQuery(ticketId);

        _ticketRepositoryMock.Setup(r => r.GetByIdAsync(ticketId)).ReturnsAsync((Ticket)null);

        // Act & Assert
        await _handler.Invoking(h => h.Handle(query, CancellationToken.None))
            .Should()
            .ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Ticket with ID {ticketId} not found.");
    }
}