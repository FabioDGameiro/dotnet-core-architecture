using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using AutoMapper;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Infra.IoC;
using Microsoft.AspNetCore.Http;

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

            services.ConfigureApplicationCookie(configure => {
                configure.Cookie.Domain = "bunda";
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
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpect error happened. Try again later.");
                    });
                });
            }

            usuarioContext.EnsureSeedDataForContext();
            app.UseMvc();
        }
    }
}
