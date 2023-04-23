namespace Application.Pizzas.Queries.GetPizzas;

public class PizzaDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public IEnumerable<string>? Toppings { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? CreatedBy { get; set; }
}
