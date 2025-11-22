using Application.Features.EventSeat;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventService.API.Controllers;

[ApiController]
[Route("api/v1/event-seats")]
public class EventSeatController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventSeatController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{seatId}")]
    public async Task<IActionResult> GetSeat(Guid seatId)
    {
        var result = await _mediator.Send(new GetEventSeatQuery(seatId));
        return Ok(result);
    }
}
