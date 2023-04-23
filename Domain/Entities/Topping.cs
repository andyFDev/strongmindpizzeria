using Domain.Common;

namespace Domain.Entities;

public class Topping : BaseEntity
{
    public string? Name { get; set; }

    public virtual ICollection<Pizza> Toppings { get; set; } = new List<Pizza>();
}
