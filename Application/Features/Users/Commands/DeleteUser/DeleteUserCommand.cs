using MediatR;

namespace Senswise.UserService.Application.Features.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(Guid Id) : IRequest<Unit>;
