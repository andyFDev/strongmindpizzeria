using AutoMapper;
using Domain.Entities;

namespace Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Pizza, Pizzas.Queries.GetPizzas.PizzaDto>()
            .ForMember(dest => dest.Toppings, opt => opt.MapFrom(src => src.Toppings.Select(t => t.Name).ToList()));

        CreateMap<Topping, Toppings.Queries.GetToppings.ToppingDto>();

        CreateMap<User, Users.Queries.GetUser.UserVm>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(t => t.Name).ToList()));
    }
}
