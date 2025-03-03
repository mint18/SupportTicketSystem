using Microsoft.AspNetCore.Mvc;
using TicketSystem.Domain.Entities;
using TicketSystem.Domain.Repositories;
using TicketSystem.Application.Statuses.Validators;
using TicketSystem.Application.Statuses.Dtos;
using FluentValidation;
using TicketSystem.Application.Statuses.Queries;
using MediatR;
using TicketSystem.Application.Statuses.Commands;

namespace TicketSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusController : ControllerBase
{
    private readonly IStatusRepository _statusRepository;
    private readonly IValidator<CreateStatusDto> _validator;
    private readonly IMediator _mediator;

    public StatusController(IStatusRepository statusRepository, IValidator<CreateStatusDto> validator, IMediator mediator)
    {
        _statusRepository = statusRepository;
        _validator = validator;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateStatus([FromBody] CreateStatusDto createStatusDto)
    {
        var command = new CreateStatusCommand(createStatusDto.StatusName);
        var createdId = await _mediator.Send(command);
        return CreatedAtAction(nameof(CreateStatus), new { id = createdId }, new { id = createdId });
    }


    [HttpGet]
    public async Task<IActionResult> GetStatuses()
    {
        var statuses = await _mediator.Send(new GetAllStatusesQuery());
        return Ok(statuses);
    }

}
