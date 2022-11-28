using System;
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
        public CustomTokenAuthHandler(IOptionsMonitor<CustomTokenAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(Options.TokenHeaderName))
                return Task.FromResult(AuthenticateResult.NoResult());

            var token = Request.Headers[Options.TokenHeaderName];

            if (string.IsNullOrEmpty(token))
                return Task.FromResult(AuthenticateResult.NoResult());

            var result = false;
            try
            {
                TokenManager.Check(token);
                result = true;
            }
            catch(Exception e)
            {
                Logger.LogError(e, e.Message);
            }

            if (Request.Path.Value != "/api/ad/login" && !result)
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