using Microsoft.AspNetCore.Mvc;
using Notes.Application.Services;
using Notes.WebApi.Contracts;
using Notes.WebApi.Models;

namespace Notes.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        public static async Task<IResult> Register(RegisterUserRequest request, UserService userService)
        {
            await userService.Register(request.UserName, request.Password, request.Email); 
            return Results.Ok();
        }
        [HttpGet]
        public static async Task<IResult> Login(string returnUrl, 
            LoginUserRequest request, 
            UserService userService, 
            HttpContext context)
        {
            var viewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
            };
            var token = await userService.Login(request.Email, request.Password);

            context.Response.Cookies.Append("note-cookies", token);

            return Results.Ok(viewModel);
        }
        [HttpPost]
        public Task<IResult> Login(LoginViewModel viewModel)
        {
            return Task.FromResult(Results.Ok(viewModel));
        }
    }
}
