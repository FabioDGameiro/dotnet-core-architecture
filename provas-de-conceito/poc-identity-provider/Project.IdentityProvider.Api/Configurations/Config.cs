using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Project.IdentityProvider.Api.Configurations
{
    // Classe apenas para testes, deve ser removida em produção

    public static class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "588fdc73-8b30-4b6e-ba0d-705b63d9f425",
                    Username = "tiago",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "Tiago"),
                        new Claim(JwtClaimTypes.FamilyName, "Santos")
                    }
                },
                new TestUser
                {
                    SubjectId = "e70c711f-160d-440f-8392-1bfbf5ce6213",
                    Username = "iran",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim(JwtClaimTypes.GivenName, "Iran"),
                        new Claim(JwtClaimTypes.FamilyName, "Nunes")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // 
            };
        }
    }
}
