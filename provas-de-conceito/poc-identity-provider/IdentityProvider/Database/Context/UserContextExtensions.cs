using System.Collections.Generic;
using System.Linq;
using IdentityModel;
using IdentityServer4;

namespace IdentityProvider.Database.Context
{
    public static class UserContextExtensions
    {
        public static void EnsureSeedDataForContext(this UserContext context)
        {
            // Add users if there aren't any users yet
            if (context.Users.Any())
            {
                return;
            }

            // init users
            var users = new List<User>
            {
                new User
                {
                    SubjectId = "b07761f1-2a3e-42d8-9051-707ce2c98dbc",
                    Username = "tiago",
                    Password = "tiago",
                    IsActive = true,

                    Claims =
                    {
                        new UserClaim(JwtClaimTypes.Name, "Tiago Santos"),
                        new UserClaim(JwtClaimTypes.GivenName, "Tiago"),
                        new UserClaim(JwtClaimTypes.FamilyName, "Santos"),
                        new UserClaim(JwtClaimTypes.Email, "tiago@email.com"),
                        new UserClaim(JwtClaimTypes.EmailVerified, "true"),
                        new UserClaim(JwtClaimTypes.WebSite, "http://tiago.com"),
                        new UserClaim(JwtClaimTypes.Address, "Antonieta de Morais, 289"),
                        new UserClaim("country", "Brasil"),
                        new UserClaim("subscriptionlevel", "FreeUser"),
                        new UserClaim("role", "FreeUser")
                    }
                },

                new User
                {
                    SubjectId = "aab263d7-b2ac-43d5-bbc2-fc812ce658d3",
                    Username = "mercia",
                    Password = "mercia",
                    IsActive = true,

                    Claims =
                    {
                        new UserClaim(JwtClaimTypes.Name, "Mercia Mello"),
                        new UserClaim(JwtClaimTypes.GivenName, "Mercia"),
                        new UserClaim(JwtClaimTypes.FamilyName, "Mello"),
                        new UserClaim(JwtClaimTypes.Email, "mercia@email.com"),
                        new UserClaim(JwtClaimTypes.EmailVerified, "true"),
                        new UserClaim(JwtClaimTypes.WebSite, "http://mercia.com"),
                        new UserClaim(JwtClaimTypes.Address, "Rua Maresias, 49"),
                        new UserClaim("country", "Brasil"),
                        new UserClaim("subscriptionlevel", "Subscriber"),
                        new UserClaim("role", "Subscriber")
                    }
                },

                new User
                {
                    SubjectId = "56639436-0465-471f-ba19-54259a399cd5",
                    Username = "iran",
                    Password = "iran",
                    IsActive = true,

                    Claims =
                    {
                        new UserClaim(JwtClaimTypes.Name, "Iran Nunes"),
                        new UserClaim(JwtClaimTypes.GivenName, "Iran"),
                        new UserClaim(JwtClaimTypes.FamilyName, "Nunes"),
                        new UserClaim(JwtClaimTypes.Email, "iran@email.com"),
                        new UserClaim(JwtClaimTypes.EmailVerified, "true"),
                        new UserClaim(JwtClaimTypes.WebSite, "http://iran.com"),
                        new UserClaim(JwtClaimTypes.Address, "Av Carrão, 8549"),
                        new UserClaim("country", "Japao"),
                        new UserClaim("role", "Subscriber")
                    }
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}
