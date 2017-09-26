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
using Project.IdentityProvider.Api.Data;

namespace Project.IdentityProvider.Api
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
            // Configurando Identity Server

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddTestUsers(InMemoryData.GetUsers())
                .AddInMemoryIdentityResources(InMemoryData.GetIdentityResources())
                .AddInMemoryClients(InMemoryData.GetClients());

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Configurando Identity Server

            app.UseIdentityServer();

            app.UseMvc();
        }
    }
}
