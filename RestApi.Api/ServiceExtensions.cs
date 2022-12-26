using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestApi.Core;
using RestApi.Core.Interfaces.Repositories;
using RestApi.Core.Services.Person;
using RestApi.Core.Services.Quote;
using RestApi.Infrastructure.Data;
using RestApi.Infrastructure.Data.Repositories;

namespace RestApi.Api
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds services to IoC container.
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IQuoteService, QuoteService>()
                .AddScoped<IPersonService, PersonService>();
        }

        /// <summary>
        /// Adds AutoMapper
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        /// <returns></returns>
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<MappingProfile>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        /// <summary>
        /// Adds and configures dbContext and repositories
        /// </summary>
        /// <param name="services">The services collection or IoC container.</param>
        /// <param name="configuration">Gets or sets the application configuration, where key value pair settings are stored.</param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlite().AddDbContext<ApplicationDbContext>(
                 options => options.UseSqlite(configuration.GetConnectionString("DefaultDbConnection")), ServiceLifetime.Scoped, ServiceLifetime.Singleton);

            services.AddAutoMapper();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IQuoteRepository, QuoteRepository>();


            return services;
        }

    }
}
