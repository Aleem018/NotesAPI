using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
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

    
}