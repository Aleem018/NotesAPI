using System.ComponentModel.DataAnnotations;
namespace NotesAPI.DTOs;

public class CreateNoteDto
{
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;
    [StringLength(10000)]
    public string Content { get; set; } = string.Empty;
}
