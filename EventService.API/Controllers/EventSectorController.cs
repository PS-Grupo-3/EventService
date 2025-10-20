using Application.Features.EventSector.Commands;
using Application.Features.EventSector.Queries;
using Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EventSectorController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventSectorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("by-event/{eventId:guid}")]
    public async Task<IActionResult> GetByEventId(Guid eventId)
    {
        var result = await _mediator.Send(new GetSectorsByEventIdQuery(eventId));
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetSectorByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEventSectorRequest request)
    {
        var result = await _mediator.Send(new CreateEventSectorCommand(request));
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var request = new DeleteEventSectorRequest { EventSectorId = id };
        var result = await _mediator.Send(new DeleteEventSectorCommand(request));
        return Ok(result);
    }
}
