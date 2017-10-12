// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Test;

namespace IdentityProvider.Configurations
{
    public class TestUsers
    {
        public static List<TestUser> Users = new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "b07761f1-2a3e-42d8-9051-707ce2c98dbc",
                Username = "tiago",
                Password = "tiago", 

                Claims = 
                {
                    new Claim(JwtClaimTypes.Name, "Tiago Santos"),
                    new Claim(JwtClaimTypes.GivenName, "Tiago"),
                    new Claim(JwtClaimTypes.FamilyName, "Santos"),
                    new Claim(JwtClaimTypes.Email, "tiago@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://tiago.com"),
                    new Claim(JwtClaimTypes.Address, "Antonieta de Morais, 289"),
                    new Claim("country", "Brasil"),
                    new Claim("subscriptionlevel", "FreeUser"),
                    new Claim("role", "FreeUser")
                }
            },

            new TestUser
            {
                SubjectId = "aab263d7-b2ac-43d5-bbc2-fc812ce658d3",
                Username = "mercia",
                Password = "mercia", 

                Claims = 
                {
                    new Claim(JwtClaimTypes.Name, "Mercia Mello"),
                    new Claim(JwtClaimTypes.GivenName, "Mercia"),
                    new Claim(JwtClaimTypes.FamilyName, "Mello"),
                    new Claim(JwtClaimTypes.Email, "mercia@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://mercia.com"),
                    new Claim(JwtClaimTypes.Address, "Rua Maresias, 49"),
                    new Claim("country", "Brasil"),
                    new Claim("subscriptionlevel", "Subscriber"),
                    new Claim("role", "Subscriber")
                }
            },

            new TestUser
            {
                SubjectId = "56639436-0465-471f-ba19-54259a399cd5",
                Username = "iran",
                Password = "iran",

                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Iran Nunes"),
                    new Claim(JwtClaimTypes.GivenName, "Iran"),
                    new Claim(JwtClaimTypes.FamilyName, "Nunes"),
                    new Claim(JwtClaimTypes.Email, "iran@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://iran.com"),
                    new Claim(JwtClaimTypes.Address, "Av Carrão, 8549"),
                    new Claim("country", "Japao"),
                    new Claim("role", "Subscriber")
                }
            }
        };
    }
}