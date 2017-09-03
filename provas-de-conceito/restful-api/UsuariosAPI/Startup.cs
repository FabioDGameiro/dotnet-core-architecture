using AutoMapper;
using Infra.Data.Context;
using Infra.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

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
            services.AddMvc(options =>
            {
                // Configura a aplicação para retornar um Status Code 406 - NOT ACCEPTABLE
                // para outros formatos de respostas diferentes dos aceitados.
                // Obs.: por padrão o único aceitável é (application/json)
                options.ReturnHttpNotAcceptable = true;
            })
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            // Adicionando suporte ao AutoMapper;
            services.AddAutoMapper();

            // Adicionando suporte ao Framework de Cache
            services.AddHttpCacheHeaders(
                expirationOptions =>
                {
                    expirationOptions.MaxAge = 600;
                },
                validationOptions =>
                {
                    validationOptions.AddMustRevalidate = true;
                });

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

            // Utilizando o middleware para supoerte a cache
            app.UseHttpCacheHeaders();

            app.UseMvc();
        }
    }
}