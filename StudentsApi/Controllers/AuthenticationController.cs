using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentsApi.Dtos;
using StudentsApi.Services.Interfaces;
using StudentsApi.ViewModels;
using StudentsApi.Services;
using System.Diagnostics;

namespace StudentsApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IAuthenticateService _authenticationService;

    public AuthenticationController(IConfiguration configuration, IAuthenticateService authenticationService)
    {
        _configuration = configuration;
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<TokenDto>> Register(RegisterViewModel register)
    {
        var result = await _authenticationService.Register(register.Email, register.Password);

        if (result)
        {
            var login = new LoginViewModel
            {
                Email = register.Email,
                Password = register.Password
            };

            var token = AuthenticateService.GenerateToken(login);
            return Ok(token);
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> Login(LoginViewModel login)
    {
        var result = await _authenticationService.Authenticate(login.Email, login.Password);
        
        if (result)
        {
            var token = AuthenticateService.GenerateToken(login);
            return Ok(token);
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
