using CommonConstants;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Idp
{
    internal static class Config
    {
        internal static IEnumerable<ApiResource> GetApis()
        {
            return new ApiResource[]
            {
                new ApiResource("api1", "My API")
            };
        }

        internal static IEnumerable<Client> GetClients()
        {
            return new Client[]
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    
                    // scopes that client has access to
                    AllowedScopes = { "openid", "api1" },
                    
                    RedirectUris = { "https://localhost:44346/signin-oidc" },
                    // AlwaysIncludeUserClaimsInIdToken = true,
                    // AllowOfflineAccess = true,

                    // logout:
                    // BackChannelLogoutSessionRequired = true,
                    // BackChannelLogoutUri = "",
                    // FrontChannelLogoutSessionRequired = true,
                    // FrontChannelLogoutUri = "",
                    // PostLogoutRedirectUris = { "" },

                }
            };
        }

        internal static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
        }

        internal static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password"
                }
            };
        }
    }
}
