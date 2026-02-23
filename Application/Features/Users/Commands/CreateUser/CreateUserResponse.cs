namespace Senswise.UserService.Application.Features.Users.Commands.CreateUser;

public sealed record CreateUserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string? Address,
    DateTime CreatedAt
);
