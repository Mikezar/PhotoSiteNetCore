using MediatR;
using Microsoft.Extensions.Options;
using PhotoSite.Domain.Admin;

namespace PhotoSite.Application.Authorization;

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginState>
{
    private readonly LoginOptions _loginOptions;

    public LoginCommandHandler(IOptionsSnapshot<LoginOptions> loginOptions)
    {
        _loginOptions = loginOptions.Value;
    }

    public Task<LoginState> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var loginAttempt = new LoginAttempt(request.Login, request.Password);
        var result = loginAttempt.Login(_loginOptions);
        return Task.FromResult(result);
    }
}

