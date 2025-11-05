using Application.Features.EventStatus.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventService.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EventStatusController : ControllerBase
{
    private readonly IMediator _mediator;

    public EventStatusController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllEventStatusQuery());
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetEventStatusByIdQuery(id));
        return Ok(result);
    }
}
