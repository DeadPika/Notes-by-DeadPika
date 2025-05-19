using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Domain.Enums;
using Notes.Infrastructure;

namespace Notes.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : Controller
    {
        [HttpGet]
        //[Authorize(policy => policy.AddRequirements(new PermissionRequirement([Permission.Read])))]
        public IActionResult Test() => Ok();
    }
}
