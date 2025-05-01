using System.ComponentModel.DataAnnotations;

namespace Notes.WebApi.Contracts
{
    public record LoginUserRequest(
        [Required] string Email,
        [Required] string Password);
}
