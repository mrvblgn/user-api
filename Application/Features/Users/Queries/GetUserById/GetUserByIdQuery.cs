using MediatR;
using Senswise.UserService.Application.Features.Users.Queries.GetAllUsers;

namespace Senswise.UserService.Application.Features.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;
