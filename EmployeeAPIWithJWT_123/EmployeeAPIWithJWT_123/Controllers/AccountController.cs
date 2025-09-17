using EmployeeAPIWithJWT_123.Models.ViewModels;
using EmployeeAPIWithJWT_123.ServiceContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeAPIWithJWT_123.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginVM loginVM)
        {
            var user=await _userService.Authenticate(loginVM);
            if (user == null) return BadRequest("wrong user/pwd");
            return Ok(user);
        }
    }
}
