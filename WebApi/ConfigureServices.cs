using Application.Common.Interfaces;
using System.Text.Json.Serialization;
using WebApi.Filters;
using WebApi.Services;

namespace WebApi;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();
        services.AddControllers().AddJsonOptions(opt =>
        {
            opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        services.AddControllersWithViews(options => options.Filters.Add<ApiExceptionFilterAttribute>());
        services.AddCors(opt =>
        {
            opt.AddPolicy(
                name: "AllowSpecificOrigins",
                policy =>
                {
                    policy
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
        });

        return services;
    }
}
