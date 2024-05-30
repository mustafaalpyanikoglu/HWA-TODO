using Application.Features.Assignment.Commands.Create;
using Application.Features.Assignment.Commands.Delete;
using Application.Features.Assignment.Commands.Update;
using Application.Features.Assignment.Queries.GetById;
using Application.Features.Assignment.Queries.GetList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssignmentsController(IMediator mediator) : Controller
{
    private readonly IMediator _mediator = mediator;
    
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetById([FromRoute] GetByIdAssignmentQuery getByIdAssignmentQuery)
    {
        var result = await _mediator.Send(getByIdAssignmentQuery);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var result = await _mediator.Send(new GetListAssigmentQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(CreateAssigmentCommand createAssignmentCommand)
    {
        var result = await _mediator.Send(createAssignmentCommand);
        return Created(uri: "", result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateAssigmentCommand updateAssignmentCommand)
    {
        var result = await _mediator.Send(updateAssignmentCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteAssigmentCommand deleteAssignmentCommand)
    {
        var result = await _mediator.Send(deleteAssignmentCommand);
        return Ok(result);
    }
}