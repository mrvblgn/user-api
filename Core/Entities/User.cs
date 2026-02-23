using Senswise.UserService.Core.Common;

namespace Senswise.UserService.Core.Entities;

public sealed class User : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string? Address { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    // Private parameterless constructor for ORM
    private User() 
    {
        // Required for EF Core but not accessible from outside
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
    }

    // Public constructor enforcing required fields
    public User(string firstName, string lastName, string email, string? address = null)
    {
        ValidateFirstName(firstName);
        ValidateLastName(lastName);
        ValidateEmail(email);

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
        CreatedAt = DateTime.UtcNow;
    }

    // Constructor with explicit Id (for scenarios like deserialization)
    public User(Guid id, string firstName, string lastName, string email, string? address = null)
        : base(id)
    {
        ValidateFirstName(firstName);
        ValidateLastName(lastName);
        ValidateEmail(email);

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string firstName, string lastName, string email, string? address = null)
    {
        ValidateFirstName(firstName);
        ValidateLastName(lastName);
        ValidateEmail(email);

        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Address = address;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateAddress(string? address)
    {
        Address = address;
        UpdatedAt = DateTime.UtcNow;
    }

    public string GetFullName() => $"{FirstName} {LastName}";

    // Guard clauses
    private static void ValidateFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("First name cannot be null or empty.", nameof(firstName));
        }

        if (firstName.Length > 100)
        {
            throw new ArgumentException("First name cannot exceed 100 characters.", nameof(firstName));
        }
    }

    private static void ValidateLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name cannot be null or empty.", nameof(lastName));
        }

        if (lastName.Length > 100)
        {
            throw new ArgumentException("Last name cannot exceed 100 characters.", nameof(lastName));
        }
    }

    private static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));
        }

        if (!email.Contains('@'))
        {
            throw new ArgumentException("Email must be a valid email address.", nameof(email));
        }

        if (email.Length > 255)
        {
            throw new ArgumentException("Email cannot exceed 255 characters.", nameof(email));
        }
    }
}
