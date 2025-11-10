using System.Security.Claims;
using Application.Features.Event.Commands;
using Application.Features.Event.Queries;
using Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventService.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EventController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromQuery] int? categoryId, [FromQuery] int? statusId, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        var events = await _mediator.Send(
            new GetFilteredEventsQuery(categoryId, statusId, from, to)
        );

        return Ok(events);
    }
   
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var evt = await _mediator.Send(new GetEventByIdQuery(id));
        return evt is null ? NotFound() : Ok(evt);
    }

    [HttpGet("{id:guid}/sectors")]
    [AllowAnonymous]
    public async Task<IActionResult> GetSectors(Guid id)
    {
        var response = await _mediator.Send(new GetEventSectorsQuery(id));
        return Ok(response);
    }


    [HttpPost]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> Create([FromBody] CreateEventRequest request)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

        var command = new CreateEventCommand(request, userId, userRole);
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = result.EventId }, result);
    }


    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateEventRequest request)
    {
        var result = await _mediator.Send(new UpdateEventCommand(request));
        return Ok(result);
    }

    [HttpPatch("{id:guid}/status/{statusId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(Guid id, int statusId)
    {
        var response = await _mediator.Send(new UpdateEventStatusCommand(id, statusId));
        return Ok(response);
    }


    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var request = new DeleteEventRequest { EventId = id };
        var result = await _mediator.Send(new DeleteEventCommand(request));
        return Ok(result);
    }
}
