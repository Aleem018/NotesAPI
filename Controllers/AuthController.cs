using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using NotesAPI.Models;
using NotesAPI.Data;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly NotesDbContext _context;
    public AuthController(NotesDbContext _context)
    {
        
    }
}