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
            throw new ArgumentException("Ad alanı boş olamaz.", nameof(firstName));
        }

        if (firstName.Length > 100)
        {
            throw new ArgumentException("Ad 100 karakteri geçemez.", nameof(firstName));
        }
    }

    private static void ValidateLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Soyad alanı boş olamaz.", nameof(lastName));
        }

        if (lastName.Length > 100)
        {
            throw new ArgumentException("Soyad 100 karakteri geçemez.", nameof(lastName));
        }
    }

    private static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("E-posta alanı boş olamaz.", nameof(email));
        }

        if (!email.Contains('@'))
        {
            throw new ArgumentException("Geçerli bir e-posta adresi giriniz.", nameof(email));
        }

        if (email.Length > 255)
        {
            throw new ArgumentException("E-posta 255 karakteri geçemez.", nameof(email));
        }
    }
}
