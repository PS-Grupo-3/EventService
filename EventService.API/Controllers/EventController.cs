using Application.Features.Event.Commands;
using Application.Features.Event.Queries;
using Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

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
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllEventsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetEventByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEventRequest request)
    {
        var result = await _mediator.Send(new CreateEventCommand(request));
        return CreatedAtAction(nameof(GetById), new { id = result.EventId }, result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEventRequest request)
    {
        var result = await _mediator.Send(new UpdateEventCommand(request));
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var request = new DeleteEventRequest { EventId = id };
        var result = await _mediator.Send(new DeleteEventCommand(request));
        return Ok(result);
    }
}
