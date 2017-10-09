using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using IdentityProvider.Configurations;
using IdentityProvider.Database.Context;
using IdentityProvider.Repositories;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
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

            var identityUsersConnectionString = Configuration["connectionStrings:identityUsersConnectionString"];
            services.AddDbContext<UserContext>(options => options.UseSqlServer(identityUsersConnectionString));

            // Registrando os repositórios

            services.AddScoped<IUserRepository, UserRepository>();

            // Registrando o IsentityServer

            var identityServerConnectionString = Configuration["connectionStrings:identityServerConnectionString"];
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentityServer()

                // desenv config

                //.AddSigningCredential(IdentityServerBuilderExtensionsCrypto.CreateRsaSecurityKey())
                //.AddTestUsers(TestUsers.Users)
                //.AddInMemoryApiResources(Config.GetApiResources())
                //.AddInMemoryIdentityResources(Config.GetIdentityResources())
                //.AddInMemoryClients(Config.GetClients())

                // production config
                .AddUserStore()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder =>
                        builder.UseSqlServer(identityServerConnectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
                })
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
            UserContext userContext,
            ConfigurationDbContext configurationDbContext)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Garantindo que o contexto dos usuarios executara o Migrations
            // e o Seed será executado
            userContext.Database.Migrate();
            userContext.EnsureSeedDataForContext();

            // Garantindo que o contexto de configurações executara o Migrations
            // e o Seed será executado
            configurationDbContext.Database.Migrate();
            configurationDbContext.EnsureSeedDataForContext();

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