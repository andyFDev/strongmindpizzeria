using Application.Pizzas.Commands.CreatePizza;
using Application.Pizzas.Commands.DeletePizza;
using Application.Pizzas.Commands.UpdatePizza;
using Application.Pizzas.Queries.GetPizzas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/pizzas")]
public class PizzasController : Controller
{
    readonly ISender _mediator;

    public PizzasController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PizzasVm>> Get()
    {
        return await _mediator.Send(new GetPizzasQuery());
    }

    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] CreatePizzaCommand command)
    {
        var pizzas = await _mediator.Send(new GetPizzasQuery());

        if (pizzas.Count > 20)
        {
            return BadRequest();
        }

        return await _mediator.Send(command);
    }

    [HttpPut("{pizzaId}")]
    public async Task<ActionResult> Put([FromRoute] int pizzaId, [FromBody] UpdatePizzaCommand command)
    {
        if (pizzaId != command.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{pizzaId}")]
    public async Task<ActionResult> Delete([FromRoute] int pizzaId)
    {
        await _mediator.Send(new DeletePizzaCommand { Id = pizzaId });

        return NoContent();
    }
}
