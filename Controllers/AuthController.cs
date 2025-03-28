using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using EmployeeApi.DTO.Request;
using EmployeeApi.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase {
    private readonly JwtTokenGenerator _jwtTokenGenerator;
    private readonly EmployeeRepository _repository;
    private readonly IConfiguration _configuration;

    public AuthController(JwtTokenGenerator jwt, EmployeeRepository repository, IConfiguration configuration) {
        _jwtTokenGenerator = jwt;
        _repository = repository;
        _configuration = configuration;
    }

    // [HttpPost("login")]
    // [AllowAnonymous]
    // public IActionResult Login([FromBody] LoginReq loginReq) {
    //     if (loginReq.Name == "admin" && loginReq.Email == "password") {
    //         var token = _jwtTokenGenerator.GenerateToken("1", loginReq.Name);
    //         return Ok(new { Token = token});
    //     }

    //     return Unauthorized(new { Message = "Invalid usernmae or password" });
    // }

    // [HttpPost("login")]
    // [AllowAnonymous]
    // public IActionResult Login([FromBody] LoginReq loginReq) {
    //     var employeeExist = _repository.GetByEmailAsync(loginReq.Email);
    //     if (employeeExist != null) {
    //         var claims = new[] {
    //             new Claim(JwtRegisteredClaimNames.Sub, _configuration.);
    //         }
    //     }
        
    // }
}