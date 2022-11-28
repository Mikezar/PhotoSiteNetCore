using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PhotoSite.Domain.Admin;

namespace PhotoSite.WebApi.Infrastructure.Authorization
{
    public class CustomTokenAuthHandler : AuthenticationHandler<CustomTokenAuthOptions>
    {
        private readonly ITokenManager _tokenManager;

        public CustomTokenAuthHandler(
            ITokenManager tokenManager,
            IOptionsMonitor<CustomTokenAuthOptions> options,
            ILoggerFactory logger, 
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock) 
        {
            _tokenManager = tokenManager;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(Options.TokenHeaderName))
                return Task.FromResult(AuthenticateResult.NoResult());

            var token = Request.Headers[Options.TokenHeaderName];

            if (string.IsNullOrEmpty(token))
                return Task.FromResult(AuthenticateResult.NoResult());

            var validationResult = _tokenManager.TryValidate(token);

            if (Request.Path.Value != "/api/ad/login" && !validationResult)
                return Task.FromResult(AuthenticateResult.Fail("Incorrect UserToken"));

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