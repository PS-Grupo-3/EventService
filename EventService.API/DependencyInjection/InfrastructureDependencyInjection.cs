using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Infrastructure.Commands;
using Infrastructure.Persistence;
using Infrastructure.Queries;
using Microsoft.EntityFrameworkCore;

namespace EventService.API.DependencyInjection
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Commands & Queries
            services.AddScoped<IEventCommand, EventCommand>();
            services.AddScoped<IEventSectorCommand, EventSectorCommand>();
            services.AddScoped<IEventCategoryQuery, EventCategoryQuery>();
            services.AddScoped<IEventQuery, EventQuery>();
            services.AddScoped<IEventSectorQuery, EventSectorQuery>();
            services.AddScoped<IEventStatusQuery, EventStatusQuery>();
            services.AddScoped<ICategoryTypeQuery, CategoryTypeQuery>();

            return services;
        }
    }
}