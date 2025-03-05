using Xunit;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.API.Controllers;
using TicketSystem.Application.Tickets.Commands;
using TicketSystem.Application.Tickets.Dtos;
using FluentAssertions;

namespace TicketSystem.API.Tests.Controllers;

public class TicketControllerUnitTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly TicketsController _controller;

    public TicketControllerUnitTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new TicketsController(_mediatorMock.Object);
    }

    [Fact]
    public async Task CreateTicket_ValidInput_ShouldCallMediatorAndReturnCreatedAtActionResult()
    {
        // arrange
        var ticketDto = new CreateTicketDto
        {
            Title = "Test Ticket",
            Description = "Test Description"
        };

        var createdId = 1; 
        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateTicketCommand>(), default))
            .ReturnsAsync(createdId);

        // act
        var result = await _controller.CreateTicket(ticketDto);

        // assert
        _mediatorMock.Verify(
            m => m.Send(
                It.Is<CreateTicketCommand>(
                    cmd => cmd.Title == ticketDto.Title &&
                           cmd.Description == ticketDto.Description
                ),
                default
            ),
            Times.Once
        );

        result.Should().BeOfType<CreatedAtActionResult>();
        var createdAtActionResult = result as CreatedAtActionResult;
        createdAtActionResult?.Value.Should().BeEquivalentTo(new { id = createdId });
        createdAtActionResult?.ActionName.Should().Be(nameof(_controller.CreateTicket));
    }

    [Fact]
    public async Task CreateTicket_InvalidInput_ShouldReturnBadRequestWithoutCallingMediator()
    {
        // arrange
        var invalidDto = new CreateTicketDto
        {
            Title = "No description"
        };

        _controller.ModelState.AddModelError("Description", "Description is required.");

        // act
        var result = await _controller.CreateTicket(invalidDto);

        // assert
        result.Should().BeOfType<BadRequestObjectResult>();
        _mediatorMock.Verify(m => m.Send(It.IsAny<CreateTicketCommand>(), default), Times.Never);
    }
}