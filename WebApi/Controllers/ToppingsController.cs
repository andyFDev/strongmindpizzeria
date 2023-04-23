using Application.Toppings.Commands.CreateTopping;
using Application.Toppings.Commands.DeleteTopping;
using Application.Toppings.Commands.UpdateTopping;
using Application.Toppings.Queries.GetToppings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/toppings")]
public class ToppingsController : Controller
{
    readonly ISender _mediator;

    public ToppingsController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<ToppingsVm>> Get()
    {
        return await _mediator.Send(new GetToppingsQuery());
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] CreateToppingCommand command)
    {
        var toppings = await _mediator.Send(new GetToppingsQuery());

        if (toppings.Count > 20)
        {
            return BadRequest();
        }

        return await _mediator.Send(command);
    }

    [HttpPut("{toppingId}")]
    public async Task<ActionResult> Put([FromRoute] int toppingId, [FromBody] UpdateToppingCommand command)
    {
        if (toppingId != command.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{toppingId}")]
    public async Task<ActionResult> Delete([FromRoute] int toppingId)
    {
        await _mediator.Send(new DeleteToppingCommand { Id = toppingId });

        return NoContent();
    }
}
