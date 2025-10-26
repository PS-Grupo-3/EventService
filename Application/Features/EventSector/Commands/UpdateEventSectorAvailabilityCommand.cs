using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Commands;
public record UpdateEventSectorAvailabilityCommand(Guid EventSectorId, bool Available) : IRequest<GenericResponse>;

