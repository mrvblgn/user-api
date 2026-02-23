using MediatR;
using Microsoft.EntityFrameworkCore;
using Senswise.UserService.Infrastructure.Persistence;

namespace Senswise.UserService.Application.Features.Users.Commands.DeleteUser;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
{
    private readonly AppDbContext _context;

    public DeleteUserCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user == null)
        {
            throw new KeyNotFoundException($"ID'si {request.Id} olan kullanıcı bulunamadı.");
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteUserResponse(
            request.Id,
            $"'{user.FirstName} {user.LastName}' kullanıcısı başarıyla silindi."
        );
    }
}
