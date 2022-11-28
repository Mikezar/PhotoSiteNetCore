using MediatR;
using PhotoSite.Domain.Admin;

namespace PhotoSite.Application.Authorization;

internal sealed class LogoutCommandHandler : AsyncRequestHandler<LogoutCommand>
{
    protected override Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        TokenManager.Check(request.TokenValue);

        return Task.CompletedTask;
    }
}

