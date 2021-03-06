﻿using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityModel;

namespace IdentityProvider.Configurations
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "Your Roles", new[] { "role" }),
                new IdentityResource("website", "Your website", new[] { "website" }),
                new IdentityResource("country", "Your Country", new[] { "country" }),
                new IdentityResource("subscriptionlevel", "Your Subscription Level", new[] { "subscriptionlevel" }),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(
                    "resourcesapi",
                    "Resources API",
                    new List<string>
                    {
                        "role"
                    })
                {
                    ApiSecrets = { new Secret("resourcesapisecret".Sha256()) }
                }
            };
        }


        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // API Client

                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "resourcesapi" }
                },

                // Mvc Client

                new Client
                {
                    ClientId = "taskmvc",
                    ClientName = "MVC Task App",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    RequireConsent = false,

                    AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = 120, // 2 minutes

                    UpdateAccessTokenClaimsOnRefresh = true,
                    AllowOfflineAccess = true,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris           = { "http://localhost:5001/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5001/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "website",
                        "resourcesapi",
                        "subscriptionlevel",
                        "country"
                    },
                },

                // JavaScript Client

                new Client
                {
                    ClientId = "taskjs",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =           { "http://localhost:5002/callback.html" },
                    PostLogoutRedirectUris = { "http://localhost:5002/index.html" },
                    AllowedCorsOrigins =     { "http://localhost:5002" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "website",
                        "resourcesapi"
                    }
                }
            };
        }
    }
}
