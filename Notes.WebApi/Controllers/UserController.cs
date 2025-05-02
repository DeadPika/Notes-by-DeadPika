using Microsoft.AspNetCore.Mvc;
using Notes.Application.Interfaces;
using Notes.Application.Services;
using Notes.WebApi.Contracts;
using Notes.WebApi.Models;

namespace Notes.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) => _userService = userService;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.Register(request.UserName, request.Password, request.Email); 
            return Ok();
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(string returnUrl, 
            LoginUserRequest request,
            HttpContext context)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
            };
            var token = await _userService.Login(request.Email, request.Password);

            context.Response.Cookies.Append("note-cookies", token);

            return Ok(viewModel);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            return Ok(viewModel);
        }
    }
}
