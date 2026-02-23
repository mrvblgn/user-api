using MediatR;
using Senswise.UserService.Core.Entities;
using Senswise.UserService.Infrastructure.Persistence;

namespace Senswise.UserService.Application.Features.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
    private readonly AppDbContext _context;

    public CreateUserCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Address
        );

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateUserResponse(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Address,
            user.CreatedAt
        );
    }
}
