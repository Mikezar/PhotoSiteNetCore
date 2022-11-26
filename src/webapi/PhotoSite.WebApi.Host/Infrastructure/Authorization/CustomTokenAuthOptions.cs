using Microsoft.AspNetCore.Authentication;

namespace PhotoSite.WebApi.Infrastructure.Authorization
{
    public class CustomTokenAuthOptions : AuthenticationSchemeOptions
    {
        public const string DefaultSchemeName = "CustomTokenAuthenticationScheme";
        public string TokenHeaderName { get; set; } = "X-CUSTOM-TOKEN";
    }
}