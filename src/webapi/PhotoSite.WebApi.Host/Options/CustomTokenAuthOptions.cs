using Microsoft.AspNetCore.Authentication;

namespace PhotoSite.WebApi.Options
{
    /// <summary>
    /// Token auth options
    /// </summary>
    public class CustomTokenAuthOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// Default scheme name
        /// </summary>
        public const string DefaultSchemeName = "CustomTokenAuthenticationScheme";

        /// <summary>
        /// Token header name
        /// </summary>
        public string TokenHeaderName { get; set; } = "X-CUSTOM-TOKEN";
    }
}