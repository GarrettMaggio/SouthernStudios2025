using System.Reflection;
using SouthernStudios2025.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SouthernStudios2025.Entities;

// DataContext is the code representation of the database
namespace SouthernStudios2025.Data;

public sealed class DataContext : IdentityDbContext<Users, Role, int>
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).GetTypeInfo().Assembly);
    }
}