using IdentityProvider.Configurations;
using IdentityProvider.Database.Context;
using IdentityProvider.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IdentityProvider
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
            // Registrando o contexto dos usuários Identity Server

            var connectionString = Configuration["connectionStrings:defaultConnectionString"];
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));

            // Registrando os repositórios

            services.AddScoped<IUserRepository, UserRepository>();

            // Registrando o IsentityServer

            services.AddIdentityServer()
                .AddSigningCredential(IdentityServerBuilderExtensionsCrypto.CreateRsaSecurityKey())
                .AddTestUsers(TestUsers.Users)
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryClients(Config.GetClients());

            services.AddMvc();
        }

        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            UserContext userContext)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Garantindo que o contexto executara o Migrations
            // e o Seed será executado
            userContext.Database.Migrate();
            userContext.EnsureSeedDataForContext();

            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}