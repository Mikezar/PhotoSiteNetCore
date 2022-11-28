using MediatR;
using PhotoSite.Domain.Admin;

namespace PhotoSite.Application.Authorization;

internal sealed class LogoutCommandHandler : AsyncRequestHandler<LogoutCommand>
{
    private readonly IUserAuthentication _userAuthentication;

    public LogoutCommandHandler(IUserAuthentication userAuthentication)
    {
        _userAuthentication = userAuthentication;
    }

    protected override Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        _userAuthentication.Logout(request.TokenValue);
        return Task.CompletedTask;
    }
}

