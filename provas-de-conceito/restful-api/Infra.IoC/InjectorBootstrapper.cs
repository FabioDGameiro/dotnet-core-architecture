#region Using

using AutoMapper;
using Domain.Base;
using Domain.Usuarios.Repository;
using Infra.CrossCutting.Reports.UsuariosReports.Repository;
using Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

#endregion

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

            // Services
            services.AddTransient<ITypeHelperService, TypeHelperService>();

            // Reports
            services.AddScoped<IUsuarioReportService, UsuarioReportService>();
        }
    }
}