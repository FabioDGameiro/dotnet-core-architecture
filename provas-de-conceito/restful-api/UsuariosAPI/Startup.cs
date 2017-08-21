using AutoMapper;
using Infra.Data.Context;
using Infra.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UsuariosAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAutoMapper();

            services.AddDbContext<UsuariosContext>(o => o.UseSqlServer(Configuration["connectionStrings:defaultConnectionString"]));
            InjectorBootstrapper.RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UsuariosContext usuarioContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    // Garante que em ambiente de produção, toda vez que a aplicação der erro,
                    // irá exibir o status 500 com a mensagem "An unexpect error happened. Try again later."
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpect error happened. Try again later.");
                    });
                });
            }

            // Reseta o seed do banco de dados a cada vez que a aplicação é iniciada
            usuarioContext.EnsureSeedDataForContext();

            app.UseMvc();
        }
    }
}