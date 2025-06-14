using System.ComponentModel.DataAnnotations;

namespace Notes.WebApi.Contracts
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "Поле UserName не должно быть пустым!")]
        [Unique("UserName", ErrorMessage = "Это имя пользователя уже занято!")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле Password не должно быть пустым!")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле Email не должно быть пустым!")]
        [EmailAddress(ErrorMessage = "Поле Email должно быть электронной почтой!")]
        [Unique("Email", ErrorMessage = "Эта почта уже зарегистрирована!")]
        public string Email { get; set; } = string.Empty;
    }
}