namespace Application.Toppings.Queries.GetToppings;

public class ToppingsVm : List<ToppingDto>
{
    public ToppingsVm(IEnumerable<ToppingDto> collection) : base(collection)
    {
    }
}
