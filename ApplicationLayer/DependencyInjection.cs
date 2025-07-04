using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using ApplicationLayer.Services.Games;
using ApplicationLayer.Services.Users;

namespace ApplicationLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);

            // FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Game Services
            services.AddScoped<IGameService, GameService>();

            // User Services  
            services.AddScoped<IUserService, UserService>();


            return services;
        }
    }
}