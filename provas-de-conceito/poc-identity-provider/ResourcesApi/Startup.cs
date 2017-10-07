using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResourcesApi.Authorization;

namespace ResourcesApi
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
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "https://localhost:44373";
                    options.RequireHttpsMetadata = true;
                    options.ApiName = "resourcesapi";
                    options.ApiSecret = "resourcesapisecret";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "MustBeOwnerOfTask",
                    builder =>
                    {
                        builder.RequireAuthenticatedUser();
                        builder.AddRequirements(new MustBeOwnerOfTask());
                    });
            });

            services.AddSingleton<IAuthorizationHandler, MustBeOwnerOfTaskHandler>();

            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCors("default");
            app.UseMvc();
        }
    }
}
