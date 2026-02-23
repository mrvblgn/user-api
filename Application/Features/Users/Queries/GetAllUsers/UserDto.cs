namespace Senswise.UserService.Application.Features.Users.Queries.GetAllUsers;

public sealed record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string? Address,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);
