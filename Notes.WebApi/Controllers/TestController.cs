using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Notes.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : Controller
    {
        [HttpGet]
        [Authorize("AdminPolicy")]
        public IActionResult Test() => Ok();
    }
}
