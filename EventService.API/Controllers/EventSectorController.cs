using Application.Features.EventSector.Commands;
using Application.Features.EventSector.Queries;
using Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventService.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class EventSectorController : BaseController
    {
        private readonly IMediator _mediator;

        public EventSectorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetSectorByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Create([FromBody] CreateEventSectorRequest request)
        {
            // Se eliminó el chequeo manual que causaba el error 403
            var result = await _mediator.Send(new CreateEventSectorCommand(request, CurrentUserId, CurrentUserRole));
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Update([FromBody] UpdateEventSectorRequest request)
        {
            var result = await _mediator.Send(new UpdateEventSectorCommand(request, CurrentUserId, CurrentUserRole));
            return Ok(result);
        }

        [HttpPatch("{id:guid}/availability")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> UpdateAvailability(Guid id, [FromBody] bool available)
        {
            var response = await _mediator.Send(new UpdateEventSectorAvailabilityCommand(id, available, CurrentUserId, CurrentUserRole));
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "SuperAdmin")] // Solo SuperAdmin puede borrar
        public async Task<IActionResult> Delete(Guid id)
        {
            var request = new DeleteEventSectorRequest { EventSectorId = id };
            var result = await _mediator.Send(new DeleteEventSectorCommand(request));
            return Ok(result);
        }
    }
}
