namespace Senswise.UserService.Application.Features.Users.Commands.DeleteUser;

public sealed record DeleteUserResponse(
    Guid Id,
    string Message
);
