using System.ComponentModel.DataAnnotations;

namespace Notes.WebApi.Contracts
{
    public record LoginUserRequest(
        [Required] [EmailAddress] string Email,
        [Required] string Password);
}
