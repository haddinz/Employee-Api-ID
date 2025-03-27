using EmployeeApi.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase {
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public AuthController(JwtTokenGenerator jwt) {
        _jwtTokenGenerator = jwt;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginReq loginReq) {
        if (loginReq.Name == "admin" && loginReq.Email == "password") {
            var token = _jwtTokenGenerator.GenerateToken("1", loginReq.Name);
            return Ok(new { Token = token});
        }

        return Unauthorized(new { Message = "Invalid usernmae or password" });
    }
}