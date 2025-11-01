using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using HabbitIt.Application.DataBase;
using HabbitIt.Domain.Models.User;
using HabbitIt.Dto.Auth;
using HabbitIt.Dto.Login;
using HabbitIt.Dto.RegisterDto;
using HabbitIt.Services.Interfaces.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens; 

public class AuthService : IAuthService
{
    private readonly Db _context;
    private readonly IConfiguration _config;

    public AuthService(Db context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        var existing = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existing != null)
            throw new Exception("Email уже занят!");
        
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            HashedPassword = hashedPassword
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        string token = GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Name = user.Name,
            Email = user.Email
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null)
            throw new Exception("Неверный email или пароль");
        
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.HashedPassword))
            throw new Exception("Неверный email или пароль");
        
        string token = GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Name = user.Name,
            Email = user.Email
        };
    }

    private string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name)
        };
        
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
        );
        
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}