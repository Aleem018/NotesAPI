using System;

namespace NotesAPI.Models;

public class Note
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Id { get; set; }
    private DateTime _createdAt { get; set; } = DateTime.UtcNow;

    public DateTime CreatedAt
    {
        get => DateTime.SpecifyKind(_createdAt, DateTimeKind.Utc);
        set => _createdAt = value;
    }

}