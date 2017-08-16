using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Domain.Empresas.Repository;
using Infra.Data.Repositories;
using AutoMapper;

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
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
        }
    }
}
