using MediatR;
using Microsoft.EntityFrameworkCore;
using Senswise.UserService.Application.Features.Users.Queries.GetAllUsers;
using Senswise.UserService.Infrastructure.Persistence;

namespace Senswise.UserService.Application.Features.Users.Queries.GetUserById;

public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly AppDbContext _context;

    public GetUserByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .Where(u => u.Id == request.Id)
            .Select(u => new UserDto(
                u.Id,
                u.FirstName,
                u.LastName,
                u.Email,
                u.Address,
                u.CreatedAt,
                u.UpdatedAt
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {request.Id} not found.");
        }

        return user;
    }
}
