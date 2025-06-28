using System.Security.Claims;
using IdentityModel;
using SouthernStudios2025.Data;
using SouthernStudios2025.Entities;

namespace SouthernStudios2025.Models;

public interface IAuthenticationService
{
    Users GetLoggedInUser();
}

public class AuthenticationService :  IAuthenticationService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticationService(
        DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public Users GetLoggedInUser()
    {
        if (!IsUserLoggedIn())
        {
            return null;
        }

        var id = RequestingUser.FindFirstValue(JwtClaimTypes.Subject).SafeParseInt();
        
        return id == null
            ? null 
            : _context.Users.SingleOrDefault(x => x.Id == id);
    }

    private ClaimsPrincipal RequestingUser
    {
        get
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var identity = httpContext?.User.Identity;

            if (identity == null)
            {
                return null;
            }

            return !identity.IsAuthenticated
                ? null
                : httpContext.User;
        }
        
    }

    private bool IsUserLoggedIn()
    {
        return _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    }
}

