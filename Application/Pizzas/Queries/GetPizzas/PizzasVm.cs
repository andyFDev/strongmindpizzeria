namespace Application.Pizzas.Queries.GetPizzas;

public class PizzasVm : List<PizzaDto>
{
    public PizzasVm(IEnumerable<PizzaDto> collection) : base(collection)
    {
    }
}
