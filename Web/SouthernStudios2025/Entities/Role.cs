using Microsoft.AspNetCore.Identity;

namespace SouthernStudios2025.Entities;

public class Role :  IdentityRole<int>
{
    public List<UserRole> Users { get; set; }
}