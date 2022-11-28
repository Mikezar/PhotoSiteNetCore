using MediatR;
using PhotoSite.Domain.Admin;

namespace PhotoSite.Application.Authorization;

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginState>
{
    private readonly IUserAuthentication _userAuthentication;

    public LoginCommandHandler(IUserAuthentication userAuthentication)
    {
        _userAuthentication = userAuthentication;
    }

    public Task<LoginState> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var result = _userAuthentication.Login(request.Login, request.Password);
        return Task.FromResult(result);
    }
}

