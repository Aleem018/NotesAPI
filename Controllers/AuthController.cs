using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NotesAPI.Models;
using NotesAPI.Data;
using NotesAPI.DTOs;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly NotesDbContext _context;
    public AuthController(NotesDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        if (_context.Users.Any(u => u.Email == request.Email))
        {
            return BadRequest("Email is already taken.");
        }

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        var newUser = new User
        {
            Email = request.Email,
            PasswordHash = hashedPassword,
        };

        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();

        return Ok(new { message = "User registered successfully!" });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto request)
    {
        var user = _context.Users.SingleOrDefault(u => u.Email == request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Unauthorized("Invalid email or password");
        }

        var token = CreateToken(user);

        return Ok(new { token = token });
    }

    private string CreateToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyOneAndOnlyAleemAIAKey123324NowIHaveToMakeSureItIsAtLeastSixtyFourCharactersLong"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //Attach the user's Id and EMail to the token payload
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        //Give the token a one day expiry date
        var tokenOptions = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenOptions); 
    }

    
}