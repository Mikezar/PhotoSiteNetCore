using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PhotoSite.ApiService.Helpers;
using PhotoSite.WebApi.Options;

namespace PhotoSite.WebApi.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomTokenAuthHandler : AuthenticationHandler<CustomTokenAuthOptions>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <param name="encoder"></param>
        /// <param name="clock"></param>
        public CustomTokenAuthHandler(IOptionsMonitor<CustomTokenAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock) { }

        /// <summary>
        /// Handle authenticate async
        /// </summary>
        /// <returns>Authenticate result</returns>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(Options.TokenHeaderName))
                return Task.FromResult(AuthenticateResult.NoResult());
            //return Task.FromResult(AuthenticateResult.Fail($"Missing Header For Token: {Options.TokenHeaderName}"));

            var token = Request.Headers[Options.TokenHeaderName];

            if (string.IsNullOrEmpty(token))
                return Task.FromResult(AuthenticateResult.NoResult());

            if (Request.Path.Value != "/api/ad/login" && !AdminHelper.CheckToken(token))
                return Task.FromResult(AuthenticateResult.Fail("incorrect UserToken"));

            var username = "Admin";
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Name, username),
            };
            var id = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(id);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}