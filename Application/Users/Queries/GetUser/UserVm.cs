namespace Application.Users.Queries.GetUser;

public class UserVm
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public IEnumerable<string>? Roles { get; set; }
}
