using MediatR;

namespace PhotoSite.Application.Authorization;

public record LogoutCommand(string TokenValue) : IRequest;
