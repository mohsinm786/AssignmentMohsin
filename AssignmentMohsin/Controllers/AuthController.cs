using AssignmentMohsin.Models;
using AssignmentMohsin.Services;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentMohsin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserModel user)
        {
            if (user.username == "admin" && user.password == "password")
            {
                var token = _jwtService.GenerateToken(user.username);
                return Ok(new { Token = token });
            }

            return Unauthorized("Invalid credentials");
        }
    }
}
