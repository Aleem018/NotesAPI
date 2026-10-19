using System.ComponentModel.DataAnnotations;

namespace NotesAPI.DTOs;
public class ResetPasswordDto
{
    [Required]
    public string Token { get; set; } = string.Empty;
    [Required, StringLength(72, MinimumLength = 8, 
        ErrorMessage = "Password must be between 8 and 72 characters")]
    public string NewPassword { get; set; } = string.Empty;
}