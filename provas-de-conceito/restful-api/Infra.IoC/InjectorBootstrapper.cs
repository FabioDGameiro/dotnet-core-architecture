using AutoMapper;
using Domain.Usuarios.Repository;
using Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infra.IoC
{
    public class InjectorBootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // AutoMapper
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(m => new Mapper(m.GetRequiredService<IConfigurationProvider>(), m.GetService));

            // Repositories
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        }
    }
}