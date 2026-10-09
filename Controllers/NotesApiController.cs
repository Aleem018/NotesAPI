using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesAPI.Data;
using NotesAPI.DTOs;
using NotesAPI.Models;

namespace NotesAPI.Controllers;

[Authorize] //this locks down every route here
[ApiController]
[Route("api/[controller]")]
public class NotesApiController : ControllerBase
{
    private readonly NotesDbContext _context;

    private int GetUserId()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (int.TryParse(userIdString, out int userId))
        {
            return userId;
        }

        throw new UnauthorizedAccessException("Invalid token payload");
    }

    // DEPENDENCY INJECTION: The server hands the database connection to the controller
    public NotesApiController(NotesDbContext context)
    {
        _context = context;
    }

    // GET: api/notesapi
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllNotes()
    {
        int currentUserId = GetUserId();

        var notes = await _context.Notes
            .Where(note => note.UserId == currentUserId)
            .OrderByDescending(note => note.CreatedAt)
            .ToListAsync();

        return Ok(notes);
    }

    // POST: api/notesapi
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateNote([FromBody] CreateNoteDto entry)
    {
        if (string.IsNullOrWhiteSpace(entry.Title) || string.IsNullOrWhiteSpace(entry.Content))
        {
            return BadRequest("Title and Content cannot be empty.");
        }

        var currentUserId = GetUserId();

        var newNote = new Note
        {
            Title = entry.Title,
            Content = entry.Content,
            UserId = currentUserId
        };

        // EF Core automatically generates the ID. We just Add and Save.
        _context.Notes.Add(newNote);

        // To write the data to notes.db
        await _context.SaveChangesAsync();

        return Created($"/api/notesapi/{newNote.Id}", newNote);
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        int currentUserId = GetUserId();

        var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id && n.UserId == currentUserId);

        if (note == null)
        {
            return NotFound("Note not found.");
        }

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();

        return Ok(new {message = "Note physically deleted from database", noteId = id });
    }
}