using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Commands;

public class ReleaseFreeSectorCommand : IRequest<GenericResponse>
{
    public Guid EventSectorId { get; set; }
}