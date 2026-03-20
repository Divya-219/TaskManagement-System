using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Abstractions.Persistence;
using taskManagement.Domain.Entities;
using TaskManagement.Application.Services;
using TaskManagement.Application;
using TaskManagement.Application.Dtos;

namespace TaskManagement.API;

[ApiController]
[Route("Api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;


    }
    [HttpPost("register")]

    public async Task<ActionResult> Register([FromBody] RegisterUserRequest request)


    {
        if(!Enum.TryParse<UserRole>(request.Role,true,out var roleEnum))
        {
            return BadRequest("Invalid role. Allowed values: User, Admin");
        }
        //var user = await _userService.RegisterUserAsync(request.Name, request.Email, request.Password, request.Role);
        //return Ok(user);

        var user = await _userService.RegisterUserAsync(
       request.Name,
       request.Email,
       request.Password,
       roleEnum // Pass enum, not string
   );

        return Ok(user);
    }
    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var token = await _userService.loginasync(request.Email, request.Password);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



}
