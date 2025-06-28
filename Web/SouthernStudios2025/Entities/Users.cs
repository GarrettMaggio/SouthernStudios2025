using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SouthernStudios2025.Entities;

public class Users : IdentityUser<int>
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string UserName { get; set; }
    
    public string Password { get; set; }
    
    public string Email { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public List<UserRole> UserRoles { get; set; }
}

public class UserGetDto
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string UserName { get; set; }
    
    public string Password { get; set; }
    
    public string Email { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
}

public class UserCreateDto
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string UserName { get; set; }
    
    public string Password { get; set; }
    
    public string Email { get; set; }
    
    public DateTime DateOfBirth { get; set; }
}

public class UserUpdateDto
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string UserName { get; set; }
    
    public string Password { get; set; }
    
    public string Email { get; set; }
}

public class UserEntityConfiguration : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        builder.Property(x => x.FirstName)
            .IsRequired();
        builder.Property(x => x.LastName)
            .IsRequired();
        builder.Property(x => x.Email)
            .IsRequired();
    }
    
}

