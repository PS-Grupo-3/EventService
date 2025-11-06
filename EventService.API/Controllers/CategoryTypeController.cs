using Application.Features.EventCategories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventService.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoryTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryTypeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllCategoryTypesQuery());
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetCategoryTypeByIdQuery(id));
        return Ok(result);
    }
}
