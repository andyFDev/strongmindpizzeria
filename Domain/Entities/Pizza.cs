using Domain.Common;

namespace Domain.Entities;

public class Pizza : BaseEntity
{
    public string? Name { get; set; }

    public virtual ICollection<Topping> Toppings { get; set; } = new List<Topping>();
}
