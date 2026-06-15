using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesAPI.Data;
using NotesAPI.Models;

namespace NotesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesApiController : ControllerBase
{
    private readonly NotesDbContext _context;

    // DEPENDENCY INJECTION: The server hands the database connection to the controller
    public NotesApiController(NotesDbContext context)
    {
        _context = context;
    }

    // GET: api/notesapi
    [HttpGet]
    public async Task<IActionResult> GetAllNotes()
    {
        var notes = await _context.Notes.ToListAsync();
        return Ok(notes);
    }

    // POST: api/notesapi
    [HttpPost]
    public async Task<IActionResult> CreateNote([FromBody] Note entry)
    {
        if (string.IsNullOrWhiteSpace(entry.Title) || string.IsNullOrWhiteSpace(entry.Content))
        {
            return BadRequest("Title and Content cannot be empty.");
        }

        // EF Core automatically generates the ID. We just Add and Save.
        _context.Notes.Add(entry);

        // To write the data to notes.db
        await _context.SaveChangesAsync();

        return Created($"/api/notes/{entry.Id}", entry);
    }

    // DELETE
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var note = await _context.Notes.FindAsync(id);

        if (note == null)
        {
            return NotFound("Note not found.");
        }

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();

        return Ok("Note physically deleted from database");
    }
}