using System.ComponentModel.DataAnnotations;

namespace Notes.WebApi.Contracts
{
    public record RegisterUserRequest(
        [Required(ErrorMessage = "Username is required")][StringLength(50, ErrorMessage = "Username cannot exceed 50 characters")] 
    string UserName,
        [Required(ErrorMessage = "Password is required")][StringLength(100, ErrorMessage = "Password cannot exceed 100 characters")] 
    string Password,
        [Required(ErrorMessage = "Email is required")][EmailAddress(ErrorMessage = "Invalid email address")] 
    string Email);
}