using Identity.DTOs;
using Identity.Mapper;
using Identity.Models;
using Identity.Services;
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
        private readonly EmailService _emailService;
        public AuthController(IConfiguration configuration, ILogger<AuthController> logger, UserManager<ApplicationUser> userManager, EmailService emailService)
        {
            _configuration = configuration;
            _logger = logger;
            _userManager = userManager;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<IActionResult>Register(RegisterDto registerDto)
        {
            var user = registerDto.ToEntity();
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            _logger.LogInformation($"User {registerDto.Email} registration attempted");

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { userId = user.Id, token = token }, Request.Scheme);

            await _emailService.SendEmailAsync(user.Email, "Email Doğrulama",
                $"<h3>Merhaba {user.FirstName},</h3><p>Email adresinizi doğrulamak için <a href='{confirmationLink}'>buraya tıklayın</a>.</p>");

            return Ok("Kayıt başarılı! Lütfen e-postanızı doğrulayın.");


        }
        [HttpPost("login")]
        public async Task<IActionResult>Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return Unauthorized("Email veya şifre yanlış.");

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return BadRequest("E-posta adresinizi doğrulamadan giriş yapamazsınız.");

            var token = GenerateToken(user);
            _logger.LogInformation($"User {user.Email} logged in successfully.");
            return Ok(new { token });
        }
        [HttpGet("confirmemail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest("Kullanıcı bulunamadı.");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
                return BadRequest("E-posta doğrulama başarısız.");

            return Ok("E-posta doğrulandı, artık giriş yapabilirsiniz!");
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
