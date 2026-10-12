using System.ComponentModel.DataAnnotations;
namespace NotesAPI.DTOs;
public class RegisterDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Enter a valid email address.")]
        [StringLength(254)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(72, MinimumLength = 8,
            ErrorMessage = "Password must be between 8 and 72 characters")]
        public string Password { get; set; } = string.Empty;
    }