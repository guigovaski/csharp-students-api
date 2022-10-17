using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudentsApi.Dtos;
using StudentsApi.Services.Interfaces;
using StudentsApi.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentsApi.Services;

public class AuthenticateService : IAuthenticateService
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private static IConfiguration _configuration;

    public AuthenticateService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<bool> Authenticate(string email, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
        return result.Succeeded;
    }

    public async Task<bool> Register(string email, string password)
    {
        var user = new IdentityUser
        {
            UserName = email,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);
        
        return result.Succeeded;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public static TokenDto GenerateToken(LoginViewModel login)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, login.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var expires = DateTime.UtcNow.AddHours(8);

        JwtSecurityToken token = new(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expires,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        );

        return new TokenDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expires,
        };
    }
}
