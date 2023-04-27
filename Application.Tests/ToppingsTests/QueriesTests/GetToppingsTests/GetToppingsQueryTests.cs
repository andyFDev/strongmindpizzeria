using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Toppings.Queries.GetToppings;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Data.Entity.Infrastructure;
using System.Reflection.Metadata;

namespace Application.Tests.ToppingsTests.QueriesTests.GetToppingsTests
{
    [TestClass]
    public class GetToppingsQueryTests
    {

        [TestMethod]
        public async Task GetAllToppings()
        {
            var myProfile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            var mapper = new Mapper(configuration);

            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: "MovieListDatabase")
                .Options;

            using (var context = new Context(options))
            {
                context.Toppings.Add(new Topping { Id = 1, Name = "ToppingTestOne", CreatedBy = 1, CreatedDate = DateTime.Now });
                context.Toppings.Add(new Topping { Id = 2, Name = "ToppingTestTwo", CreatedBy = 1, CreatedDate = DateTime.Now });
                context.Toppings.Add(new Topping { Id = 3, Name = "ToppingTestThree", CreatedBy = 1, CreatedDate = DateTime.Now });
                context.SaveChanges();

                var command = new GetToppingsQuery();
                var handler = new GetToppingsQueryHandler(context, mapper);

                var response = await handler.Handle(command, new CancellationToken());

                Assert.AreEqual(3, response.Count);
                Assert.AreEqual("ToppingTestOne", response[0].Name);
                Assert.AreEqual("ToppingTestTwo", response[1].Name);
                Assert.AreEqual("ToppingTestThree", response[2].Name);
            }
        }
    }
}