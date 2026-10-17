using System.ComponentModel.DataAnnotations;

namespace NotesAPI.DTOs;
public class UpdateNoteDto
{
    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;
    [Required, StringLength(10000)]
    public string Content { get; set; } = string.Empty;
}