using MediatR;

namespace Senswise.UserService.Application.Features.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string? Address
) : IRequest<CreateUserResponse>;
