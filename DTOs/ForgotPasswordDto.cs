using System.ComponentModel.DataAnnotations;

namespace NotesAPI.DTOs;
public class ForgotPasswordDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
}