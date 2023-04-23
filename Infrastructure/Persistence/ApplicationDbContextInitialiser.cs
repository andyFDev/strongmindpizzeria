using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            //  Seed data, if there is none
            if (!_context.Pizzas.Any() || !_context.Toppings.Any())
            {
                var pineAppleTopping = new Topping
                {
                    Name = "Pineapple"
                };

                _context.Toppings.Add(pineAppleTopping);

                _context.Toppings.Add(new Topping
                {
                    Name = "Pepperoni"
                });

                _context.Toppings.Add(new Topping
                {
                    Name = "Tomatoes"
                });

                _context.Pizzas.Add(new Pizza
                {
                    Name = "Hawaiian",
                    Toppings = new[] { pineAppleTopping }
                });

                var toppingManagerRole = new Role
                {
                    Name = "ToppingManager"
                };

                var toppingUserRole = new Role
                {
                    Name = "ToppingUser"
                };

                var pizzaUserRole = new Role
                {
                    Name = "PizzaUser"
                };

                _context.Roles.Add(toppingManagerRole);

                _context.Roles.Add(toppingUserRole);

                _context.Roles.Add(pizzaUserRole);

                _context.Users.Add(new User
                {
                    Name = "Owner",
                    Roles = new[] { toppingManagerRole, toppingUserRole, pizzaUserRole }
                });

                _context.Users.Add(new User
                {
                    Name = "Chef1",
                    Roles = new[] { toppingUserRole, pizzaUserRole }
                });

                await _context.SaveChangesAsync();
            }
        }
        catch
        {
            throw;
        }
    }
}
