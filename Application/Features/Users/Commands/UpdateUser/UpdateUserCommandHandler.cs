using MediatR;
using Microsoft.EntityFrameworkCore;
using Senswise.UserService.Infrastructure.Persistence;

namespace Senswise.UserService.Application.Features.Users.Commands.UpdateUser;

public sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
{
    private readonly AppDbContext _context;

    public UpdateUserCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"ID'si {request.Id} olan kullanıcı bulunamadı.");
        }

        user.Update(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Address
        );

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateUserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Address,
            user.UpdatedAt ?? DateTime.UtcNow
        );
    }
}
