using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Tests
{
    internal class Context : DbContext, IApplicationDbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }

        public DbSet<Topping> Toppings { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public Context(DbContextOptions options) : base(options) { }
    }
}
