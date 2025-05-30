using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Security.Claims;
using Notes.Infrastructure.Authentication;

namespace Notes.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public abstract class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

        //internal Guid UserId => User.Identity!.IsAuthenticated
        //    ? Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
        //    : Guid.Empty;
        internal Guid UserId
        {
            get
            {
                if (!User.Identity!.IsAuthenticated)
                {
                    Console.WriteLine("User is not authenticated");
                    return Guid.Empty;
                }

                var userIdClaim = User.FindFirst(CustomClaims.UserId)?.Value; // Используем кастомный claim
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    Console.WriteLine("UserId not found in token");
                    return Guid.Empty;
                }

                var userId = Guid.Parse(userIdClaim);
                Console.WriteLine($"Extracted UserId: {userId}");
                return userId;
            }
        }
    }
}
