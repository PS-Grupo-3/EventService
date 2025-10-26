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

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEventSectorRequest request)
    {
        var result = await _mediator.Send(new UpdateEventSectorCommand(request));
        return Ok(result);
    }

    [HttpPatch("/api/v1/EventSector/{id:guid}/availability")]
    public async Task<IActionResult> UpdateAvailability(Guid id, [FromBody] bool available)
    {
        var response = await _mediator.Send(new UpdateEventSectorAvailabilityCommand(id, available));
        return Ok(response);
    }


    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var request = new DeleteEventSectorRequest { EventSectorId = id };
        var result = await _mediator.Send(new DeleteEventSectorCommand(request));
        return Ok(result);
    }
}
