using Application.Features.Event.Commands;
using Application.Features.Event.Queries;
using Application.Features.EventSeat;
using Application.Features.EventSector.Commands;
using Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventService.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class EventController : BaseController
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] int? categoryId, [FromQuery] int? statusId, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] string? name)
        {
            var query = new GetFilteredEventsQuery(categoryId, statusId, from, to, name);
            var events = await _mediator.Send(query);
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
            var command = new CreateEventCommand(request, CurrentUserId, CurrentUserRole);
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = result.EventId }, result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Update([FromBody] UpdateEventRequest request)
        {
            var result = await _mediator.Send(new UpdateEventCommand(request, CurrentUserId, CurrentUserRole));
            return Ok(result);
        }

        [HttpPatch("{id:guid}/status/{statusId:int}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> UpdateStatus(Guid id, int statusId)
        {
            var response = await _mediator.Send(new UpdateEventStatusCommand(id, statusId, CurrentUserId, CurrentUserRole));
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new DeleteEventRequest { EventId = id };
            var result = await _mediator.Send(new DeleteEventCommand(request, CurrentUserId, CurrentUserRole));
            return Ok(result);
        }
        
        // Adapter / Prototype
        [HttpGet("{id:guid}/full")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFull(Guid id)
        {
            var result = await _mediator.Send(new GetEventFullSnapshotQuery(id));
            return Ok(result);
        }

        [HttpGet("{id:guid}/metrics")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMetrics(Guid id)
        {
            var result = await _mediator.Send(new GetEventMetricsQuery(id));
            return Ok(result);
        }

        [HttpGet("{eventId:guid}/sectors/{sectorId:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSector(Guid eventId, Guid sectorId)
        {
            var result = await _mediator.Send(new GetEventSectorByIdQuery(sectorId));
            return Ok(result);
        }

        [HttpPatch("{eventId:guid}/seats/{seatId:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateSeat(Guid eventId, Guid seatId, [FromBody] UpdateSeatStatusRequest req)
        {
            var result = await _mediator.Send(new UpdateSeatStatusCommand(seatId, req));
            return Ok(result);
        }
        
        [HttpPatch("{id}/reserve")]
        public async Task<IActionResult> ReserveFree(Guid id)
        {
            var cmd = new ReserveFreeSectorCommand { EventSectorId = id };
            var result = await _mediator.Send(cmd);

            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }

        [HttpPatch("{id}/release")]
        public async Task<IActionResult> ReleaseFree(Guid id)
        {
            var cmd = new ReleaseFreeSectorCommand { EventSectorId = id };
            var result = await _mediator.Send(cmd);

            if (!result.Success) return BadRequest(result);
            return Ok(result);
        }


    }
}
