using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SouthernStudios2025.Common;
using SouthernStudios2025.Data;
using SouthernStudios2025.Entities;

namespace SouthernStudios2025.Controllers;

[ApiController]
[Route("api/users")]

public class UsersController : ControllerBase
{
    private readonly DataContext _context;
    private readonly UserManager<Users> _userManager;

    public UsersController(DataContext context, UserManager<Users> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var response = new Response();

        response.Data = _context
            .Users
            .Select(x => new UserGetDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName,
                Email = x.Email,
                DateOfBirth = x.DateOfBirth,
            }).ToList();
            return Ok(response);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetbyId([FromRoute] int id)

    {
        var response = new Response();
        var userToReturn = _context.Users.Select(x => new UserGetDto
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            UserName = x.UserName,
            Email = x.Email,
            DateOfBirth = x.DateOfBirth,
        }).ToList();

        if (userToReturn == null)
        {
            response.AddError("id", "User not found");
            return NotFound(response);
        }
        
        response.Data = userToReturn;
        return Ok(response);
    }

    [HttpPost]
    public IActionResult Create(
        [FromBody] UserCreateDto userCreateDto)
    {
        var response = new Response();

        if (string.IsNullOrEmpty(userCreateDto.FirstName))
        {
            response.AddError("FirstName", "First name is required");
        }

        if (string.IsNullOrEmpty(userCreateDto.LastName))
        {
            response.AddError("LastName", "Last name is required");
        }

        if (string.IsNullOrEmpty(userCreateDto.UserName))
        {
            response.AddError("UserName", "UserName is required");
        }

        if (string.IsNullOrEmpty(userCreateDto.Password))
        {
            response.AddError("Password", "Password is required");
        }

        if (string.IsNullOrEmpty(userCreateDto.Email))
        {
            response.AddError("Email", "Email is required");
        }

        if (response.HasErrors)
        {
            return BadRequest(response);
        }

        var userToCreate = new Users
        {
            FirstName = userCreateDto.FirstName,
            LastName = userCreateDto.LastName,
            UserName = userCreateDto.UserName,
            Email = userCreateDto.Email,
            DateOfBirth = userCreateDto.DateOfBirth,
        };
        
        _userManager.CreateAsync(userToCreate, userCreateDto.Password).Wait();
        _userManager.AddToRoleAsync(userToCreate, "Admin").Wait();
        _context.SaveChanges();

        var userGetDto = new UserGetDto
        {
            Id = userToCreate.Id,
            FirstName = userToCreate.FirstName,
            LastName = userToCreate.LastName,
            UserName = userToCreate.UserName,
            Email = userToCreate.Email,
            DateOfBirth = userToCreate.DateOfBirth
        };

        response.Data = userGetDto;
        return Created("", response);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] UserUpdateDto userUpdateDto)
    {
        var response = new Response();

        if (userUpdateDto == null)
        {
            response.AddError("id", "Problem occured. Cannot edit user");
            return NotFound(response);
        }

        var userToUpdate = _context.Users.SingleOrDefault(x => x.Id == id);

        if (userToUpdate == null)
        {
            response.AddError("id", "User not found");
            return NotFound(response);
        }

        if (string.IsNullOrEmpty(userUpdateDto.FirstName))
        {
            response.AddError("FirstName", "FirstName is required ");
            return NotFound(response);
        }

        if (string.IsNullOrEmpty(userUpdateDto.LastName))
        {
            response.AddError("LastName", "LastName is required ");
        }

        if (string.IsNullOrEmpty(userUpdateDto.UserName))
        {
            response.AddError("UserName", "UserName is required ");
            return NotFound(response);
        }

        if (string.IsNullOrEmpty(userUpdateDto.Password))
        {
            response.AddError("Password", "Password is required ");
            return NotFound(response);
        }

        if (string.IsNullOrEmpty(userUpdateDto.Email))
        {
            response.AddError("Email", "Email is required ");
            return NotFound(response);
        }

        if (response.HasErrors)
        {
            return BadRequest(response);
        }
        
        userToUpdate.FirstName = userUpdateDto.FirstName;
        userToUpdate.LastName = userUpdateDto.LastName;
        userToUpdate.UserName = userUpdateDto.UserName;
        userToUpdate.Password = userUpdateDto.Password;
        userToUpdate.Email = userUpdateDto.Email;
        
        _context.SaveChanges();

        var userGetDto = new UserGetDto
        {
            Id = userToUpdate.Id,
            FirstName = userToUpdate.FirstName,
            LastName = userToUpdate.LastName,
            UserName = userToUpdate.UserName,
            Password = userToUpdate.Password,
            Email = userToUpdate.Email,
        };
        
        response.Data = userGetDto;
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var response = new Response();
        
        var userToDelete = _context.Users.FirstOrDefault(x => x.Id == id);

        if (userToDelete == null)
        {
            response.AddError("id", "User not found");
            return NotFound(response);
        }
        
        _context.Users.Remove(userToDelete);
        _context.SaveChanges();
        
        return Ok(response);

    }
    
}