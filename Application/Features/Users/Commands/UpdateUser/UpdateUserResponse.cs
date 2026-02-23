namespace Senswise.UserService.Application.Features.Users.Commands.UpdateUser;

public sealed record UpdateUserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string? Address,
    DateTime UpdatedAt
);
