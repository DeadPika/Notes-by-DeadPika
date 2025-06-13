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

                // Устанавливаем куки с правильными атрибутами
                Response.Cookies.Append("note-cookies", token, new CookieOptions
                {
                    HttpOnly = false, // Делаем доступными для JavaScript
                    SameSite = SameSiteMode.None, // Для кросс-доменных запросов
                    Secure = true, // Требуется для SameSite=None
                    Expires = DateTimeOffset.UtcNow.AddDays(7),
                    Path = "/",
                    Domain = "https://notes-deadpikas-projects.vercel.app"
                });

                return Ok(new { message = "Успешный вход" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Ошибка входа: {ex.Message}" });
            }
        }
    }
}
