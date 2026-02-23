using MediatR;

namespace Senswise.UserService.Application.Features.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string? Address
) : IRequest<UpdateUserResponse>;
