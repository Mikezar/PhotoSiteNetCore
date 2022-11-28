using MediatR;
using PhotoSite.Domain.Admin;

namespace PhotoSite.Application.Authorization;

public record LoginCommand(string Login, string Password) : IRequest<LoginState>;
