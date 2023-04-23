using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Pizza> Pizzas { get; }

    DbSet<Topping> Toppings { get; }

    DbSet<User> Users { get; }

    DbSet<Role> Roles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
