using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SouthernStudios2025.Common;
using SouthernStudios2025.Entities;
using SouthernStudios2025.Models;

namespace SouthernStudios2025.Controllers;

[ApiController]
[Route("/api")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;
    private readonly UserManager<Users> _userManager;
    private readonly SignInManager<Users> _signInManager;

    public AuthenticationController(
        IAuthenticationService authenticationService,
        UserManager<Users> userManager,
        SignInManager<Users> signInManager)
    {
        _authenticationService = authenticationService;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] LoginDto dto)
    {
        var response = new Response();
        
        var user = await _userManager.FindByNameAsync(dto.UserName ?? "");

        if (user == null)
        {
            response.AddError(string.Empty, "Invalid login attempt. Password or Username is incorrect");
            return BadRequest(response);
        }
        
        var result = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

        if (!result.Succeeded)
        {
            response.AddError(string.Empty, "Invalid login attempt. Password or Username is incorrect");
            return BadRequest(response);
        }

        response.Data = result.Succeeded;
        return Ok(response);
    }

    [HttpPost("logout")]

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [Authorize]
    [HttpGet("get-current-user")]

    public IActionResult GetLoggedInUser()
    {
        var response = new Response();
        var user = _authenticationService.GetLoggedInUser();

        if (user == null)
        {
            response.AddError(string.Empty, "User not found");
            return BadRequest(response);
        }

        var userGetDto = new UserGetDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            DateOfBirth = user.DateOfBirth,
        };
        response.Data = userGetDto;
        return Ok(response.Data);
    }
    

}

public class LoginDto
{
    public string UserName { get; set; }
    
    public string Password { get; set; }
}