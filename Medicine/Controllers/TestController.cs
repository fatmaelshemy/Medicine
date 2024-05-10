using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Medicine.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test() // GET: api/Test
        {
            string userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(userid);
        }
    }
}