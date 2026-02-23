using MediatR;
using Microsoft.EntityFrameworkCore;
using Senswise.UserService.Infrastructure.Persistence;

namespace Senswise.UserService.Application.Features.Users.Queries.GetAllUsers;

public sealed class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly AppDbContext _context;

    public GetAllUsersQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .AsNoTracking()
            .Select(u => new UserDto(
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email,
                u.Address,
                u.CreatedAt,
                u.UpdatedAt
            ))
            .ToListAsync(cancellationToken);

        return users;
    }
}
