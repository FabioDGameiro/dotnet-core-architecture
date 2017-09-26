using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project.IdentityProvider.Api.Data
{
    // Classe apenas para testes, deve ser removida em produção

    public static class InMemoryData
    {
        // Retorna os usuários com acesso ao Identity Provider

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "588fdc73-8b30-4b6e-ba0d-705b63d9f425",
                    Username = "Tiago",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Tiago"),
                        new Claim("family_name", "Santos")
                    }
                },
                new TestUser
                {
                    SubjectId = "e70c711f-160d-440f-8392-1bfbf5ce6213",
                    Username = "Iran",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Iran"),
                        new Claim("family_name", "Nunes")
                    }
                }
            };
        }

        // Retorna o escopo de informações do Identity

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        // Retorna os Clients que tem acesso ao Identity Provider

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // 
            };
        }
    }
}
