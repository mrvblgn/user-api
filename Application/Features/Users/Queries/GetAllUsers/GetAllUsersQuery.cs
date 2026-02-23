using MediatR;

namespace Senswise.UserService.Application.Features.Users.Queries.GetAllUsers;

public sealed record GetAllUsersQuery : IRequest<List<UserDto>>;
