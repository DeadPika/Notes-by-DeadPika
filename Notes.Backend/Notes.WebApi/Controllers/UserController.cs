using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Interfaces;
using Notes.Application.Services;
using Notes.WebApi.Contracts;
using Notes.WebApi.Models;

namespace Notes.WebApi.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UserController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request, IUserService userService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList()
                });
            }

            try
            {
                await userService.Register(request.UserName, request.Password, request.Email);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Ошибка регистрации", Details = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserRequest request, IUserService userService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var token = await userService.Login(request.Email, request.Password);
                if (string.IsNullOrEmpty(token))
                {
                    return BadRequest(new { message = "Токен не сгенерирован" });
                }
                //var context = HttpContext;
                HttpContext.Response.Cookies.Append("note-cookies", token, new CookieOptions
                {
                    HttpOnly = false, // Убираем HttpOnly для теста (или оставьте true, если безопасно)
                    Path = "/",
                    SameSite = SameSiteMode.None, // Разрешаем кросс-доменные запросы
                    Secure = true, // Требуется для SameSite=None, если используется HTTPS
                    Expires = DateTimeOffset.UtcNow.AddDays(7) // Устанавливаем срок действия (опционально)
                });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ошибка входа: {ex}" });
            }
        }
    }
}
