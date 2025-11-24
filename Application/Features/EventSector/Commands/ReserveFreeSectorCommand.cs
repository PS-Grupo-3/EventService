using Application.Models.Responses;
using MediatR;

namespace Application.Features.EventSector.Commands;

public class ReserveFreeSectorCommand : IRequest<GenericResponse>
{
    public Guid EventSectorId { get; set; }
}