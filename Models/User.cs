namespace NotesAPI.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    private DateTime _createdAt { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt
    {
        get => DateTime.SpecifyKind(_createdAt, DateTimeKind.Utc);
        set => _createdAt = value;
    }
}