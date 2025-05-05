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
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request,
            IUserService userService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await userService.Register(request.UserName, request.Password, request.Email); 

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserRequest request,
            IUserService userService)
        {
            var token = await userService.Login(request.Email, request.Password);

            var context = HttpContext;
            context.Response.Cookies.Append("note-cookies", token);

            return Ok();
        }

        //[HttpPost("login")]
        //public IActionResult Login(LoginViewModel viewModel)
        //{
        //    return Ok(viewModel);
        //}
    }
}
