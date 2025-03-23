using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketSystem.Application.Tickets.Commands;
using TicketSystem.Application.Tickets.Dtos;
using TicketSystem.Application.Tickets.Queries;
using TicketSystem.Domain.Constants;

namespace TicketSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TicketsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto createTicketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateTicketCommand(createTicketDto.Title, createTicketDto.Description);
            var createdId = await mediator.Send(command);
            return CreatedAtAction(nameof(CreateTicket), new { id = createdId }, new { id = createdId });
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var query = new GetTicketsQuery();
            var tickets = await mediator.Send(query);
            return Ok(tickets);
        }

        [HttpGet("{ticketId}")]
        public async Task<IActionResult> GetTicket(int ticketId)
        {
            try
            {
                var query = new GetTicketByIdQuery(ticketId);
                var ticket = await mediator.Send(query);
                return Ok(ticket);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{ticketId}/status")]
        [Authorize(Roles = $"{Roles.Manager},{Roles.Admin}")]
        public async Task<IActionResult> UpdateTicketStatus(int ticketId, [FromBody] int statusId)
        {
            var command = new UpdateTicketStatusCommand(ticketId, statusId);
            try
            {
                await mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{ticketId}")]
        public async Task<IActionResult> PatchTicket(int ticketId, [FromBody] UpdateTicketDto updateTicketDto)
        {
            var command = new UpdateTicketCommand(ticketId, updateTicketDto.Title, updateTicketDto.Description);
            try
            {
                await mediator.Send(command);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{ticketId}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> DeleteTicket(int ticketId)
        {
            try
            {
                await mediator.Send(new DeleteTicketCommand(ticketId));
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
