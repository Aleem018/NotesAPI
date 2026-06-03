using System;
using Microsoft.AspNetCore.Mvc;
using NotesAPI.Models;

namespace NotesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotesApiController : ControllerBase
{
    private static readonly List<Note> NoteList = new List<Note>();
    private static readonly object _lockObject = new object();

    // My GET method
    [HttpGet]
    public IActionResult GetList()
    {
        return Ok(NoteList);
    }

    // My POST method
    [HttpPost]

    public IActionResult PostList([FromBody] Note entry)
    {
        if (String.IsNullOrWhiteSpace(entry.Title))
        {
            return BadRequest("You must give the note a title");
        }
        if (String.IsNullOrWhiteSpace(entry.Content))
        {
            return BadRequest("You cannot submit an empty note");
        }
        Note newNote = new Note();

        newNote.Title = entry.Title;
        newNote.Content = entry.Content;

        int id = 0;

        foreach (var note in NoteList)
        {
            if (note.Id > id)
            {
                id = note.Id;
            }
        }
        newNote.Id = id + 1;
        
        NoteList.Add(newNote);

        return StatusCode(201, newNote);

    }

    // GET Single note endpoint
    [HttpGet("{id}")]
    public IActionResult GetSingleNote(int id)
    {
        Note? find = NoteList.Find(x => x.Id == id);

        if (find != null)
        {
            return Ok(find);
        } else
        {
            return NotFound();
        }
        
    }

    // PUT - Update Endpoint (to edit a note)
    [HttpPut("{id}")]
    public IActionResult UpdateNote(int id, [FromBody] Note UpdatedEntry)
    {
        Note? find = NoteList.Find(x => x.Id == id);

        if (find == null)
        {
            return NotFound();
        } 

        if (String.IsNullOrWhiteSpace(UpdatedEntry.Content) || String.IsNullOrWhiteSpace(UpdatedEntry.Title))
        {
            return BadRequest("You cannot submit an empty note");
        } 
        
        find.Title = UpdatedEntry.Title;
        find.Content = UpdatedEntry.Content;

        return Ok(find);
        
    }

    // DELETE Endpoint
    [HttpDelete("{id}")]
    public IActionResult DeleteNote(int id)
    {
        Note? find = NoteList.Find(x => x.Id == id);

        if (find == null)
        {
            return NotFound();
        }

        NoteList.Remove(find);
        return Ok("Note deleted successfully.");
        
    }

}