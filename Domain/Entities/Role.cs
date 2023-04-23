using Domain.Common;

namespace Domain.Entities;

public class Role : BaseEntity
{
    public string? Name { get; set; }

    public IEnumerable<User> Roles { get; set; } = new List<User>();
}
