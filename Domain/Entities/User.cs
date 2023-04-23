using Domain.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string? Name { get; set; }

    public IEnumerable<Role> Roles { get; set; } = new List<Role>();
}
