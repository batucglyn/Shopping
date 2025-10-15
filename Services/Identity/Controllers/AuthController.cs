using Identity.DTOs;
using Identity.Mapper;
using Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthController(IConfiguration configuration, ILogger<AuthController> logger, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult>Register(RegisterDto registerDto)
        {
            var user =registerDto.ToEntity();

            var result =await _userManager.CreateAsync(user,registerDto.Password);
            _logger.LogInformation($"User {registerDto.Email} registration attempted");
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(result);


        }
        [HttpPost("login")]
        public async Task<IActionResult>Login(LoginDto loginDto)
        {
           var user= await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized(loginDto.Email);

            var token = GenerateToken(user);
             _logger.LogInformation($"User{user.Email}logged in successfully.");
            return Ok( new {token});
        }

        private string GenerateToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("uid", user.Id)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
                signingCredentials: creds);
            _logger.LogInformation($"JWT Token generated for user {user.Email}");
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
