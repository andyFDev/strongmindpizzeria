namespace Application.Toppings.Queries.GetToppings;

public class ToppingDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime? CreatedDate { get; set; }
    public int? CreatedBy { get; set; }
}
