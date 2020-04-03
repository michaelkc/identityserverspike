// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using SharedConfiguration;

namespace IdSrv
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource(Api1.ResourceId, Api1.ResourceName),
                new ApiResource(Api2.ResourceId, Api2.ResourceName)
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = Mvc.ClientId,
                    ClientName = Mvc.ClientName,

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    ClientSecrets = { new Secret(Mvc.ClientSecret.Sha256()) },

                    RedirectUris = { Mvc.RedirectUri },
                    //FrontChannelLogoutUri = "http://localhost:5003/signout-oidc",
                    //PostLogoutRedirectUris = { "http://localhost:5003/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        "openid",
                        "profile", 
                        Api1.ResourceId
                    },
                    AllowOfflineAccess = true,
                }
            };
    }
}