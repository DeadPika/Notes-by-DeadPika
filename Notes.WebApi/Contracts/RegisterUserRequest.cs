using System.ComponentModel.DataAnnotations;

namespace Notes.WebApi.Contracts
{
    public record RegisterUserRequest(
        [Required] string UserName,
        [Required] string Password,
        [Required] string Email);
}
