#region Using

using System.Collections.Generic;
using AspNetCoreRateLimit;
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
using Newtonsoft.Json;

#endregion

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
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            // Adicionando suporte ao AutoMapper;
            services.AddAutoMapper();

            // Adicionando suporte ao Framework de Cache
            services.AddHttpCacheHeaders(
                expirationOptions => { expirationOptions.MaxAge = 60; },
                validationOptions => { validationOptions.AddMustRevalidate = true; });

            // Adicionando suporte a Rate Limiting and Throttling
            services.AddMemoryCache();

            services.Configure<IpRateLimitOptions>(options =>
            {
                options.GeneralRules = new List<RateLimitRule>
                {
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        Limit = 1000, // para testes colocar 10 e realizar mais que 10 requests em menos de 5 minutos
                        Period = "5m"
                    },
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        Limit = 200, // para testes colocar 2 e realizar mais que 2 requests em menos de 10 segundos
                        Period = "10s"
                    }
                };
            });

            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();

            // Adicionando o contexto e os services do negócio

            services.AddDbContext<UsuariosContext>(o =>
                o.UseSqlServer(Configuration["connectionStrings:defaultConnectionString"]));
            InjectorBootstrapper.RegisterServices(services);
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            UsuariosContext usuarioContext)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
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

            // Reseta o seed do banco de dados a cada vez que a aplicação é iniciada
            usuarioContext.Database.Migrate();
            usuarioContext.EnsureSeedDataForContext();

            // Utilizando o middleware para aplicar o suporte a Rate Limiting and Throttling

            app.UseIpRateLimiting();

            // Utilizando o middleware para supoerte a cache
            app.UseHttpCacheHeaders();

            app.UseMvc();
        }
    }
}