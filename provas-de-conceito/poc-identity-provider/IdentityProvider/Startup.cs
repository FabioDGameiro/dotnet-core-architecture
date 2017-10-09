using System;
using System.Security.Cryptography.X509Certificates;
using IdentityProvider.Configurations;
using IdentityProvider.Database.Context;
using IdentityProvider.Repositories;
using IdentityServer4;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.OData.Query.SemanticAst;
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
                
                // desenv config
                
                //.AddSigningCredential(IdentityServerBuilderExtensionsCrypto.CreateRsaSecurityKey())
                //.AddTestUsers(TestUsers.Users)
                .AddUserStore()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryClients(Config.GetClients())
                
                // production config
                .AddSigningCredential(LoadCertificateFromStore());

            // Registando o Provider do Facebook e 2FA

            services.AddAuthentication()
                .AddCookie("idsrv.2FA")
                .AddFacebook("Facebook", "Facebook", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.AppId = "1682895991744536";
                    options.AppSecret = "0fad88f633e751f99c5016a5cd059390";
                });

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
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }

        public X509Certificate2 LoadCertificateFromStore()
        {
            const string thumbPrint = "34FE0A35D4757DC8979FC8211A6352A034F73346";

            using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly);
                var certCollection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbPrint, true);
                if (certCollection.Count == 0)
                {
                    throw new Exception("The specified certificate wasn't found.");
                }
                return certCollection[0];
            }
        }
    }
}