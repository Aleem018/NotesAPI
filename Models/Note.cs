using System;

namespace NotesAPI.Models;

public class Note
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}