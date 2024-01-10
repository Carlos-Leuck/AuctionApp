using Duende.IdentityServer.Models;

namespace IdentityService;

public static class Config
{
    private const int _oneMonthInSeconds = 3600 * 24 * 30;

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new("auctionApp","Auction app full access"),
        };

    //Just for development configuration and postman testing.
    public static IEnumerable<Client> Clients =>
        new Client[]
        {
           new() {
            ClientId = "postman",
            ClientName = "Postman",
            AllowedScopes = {"openid", "profile", "auctionApp"},
            RedirectUris = {"https://www.getpostman.com/oauth2/callback"},
            ClientSecrets = new[] {new Secret("NotASecret".Sha256())},
            AllowedGrantTypes = {GrantType.ResourceOwnerPassword}
           },
           new()
           {
            ClientId = "nextApp",
            ClientName = "nextApp",
            ClientSecrets = {new Secret("secret".Sha256())},
            AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
            RequirePkce = false,
            RedirectUris = {"http://localhost:3000/api/auth/callback/id-server"},
            AllowOfflineAccess = true,
            AllowedScopes = {"openid", "profile", "auctionApp"},
            AccessTokenLifetime = _oneMonthInSeconds,
            AlwaysIncludeUserClaimsInIdToken = true
           }
        };
}
