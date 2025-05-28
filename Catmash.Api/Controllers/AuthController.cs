using Catmash.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Catmash.Api.DTOs;

namespace Catmash.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Pseudo,
                Nom = dto.LastName,
                Prenom = dto.FirstName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            try
            {
                var token = GenerateJwtToken(user);
                return Ok(new
                {
                    token,
                    pseudo = user.UserName,
                    firstName = user.Prenom,
                    lastName = user.Nom
                });
            }
            catch (Exception ex)
            {
                // logguer l'erreur ici
                return StatusCode(500, new { message = "Erreur lors de la génération du token", detail = ex.Message });
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.Pseudo);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return Unauthorized(new { message = "Identifiants incorrects." });


            try
            {
                var token = GenerateJwtToken(user);
                return Ok(new
                {
                    token,
                    pseudo = user.UserName,
                    firstName = user.Prenom,
                    lastName = user.Nom
                });
            }
            catch (Exception ex)
            {
                // logguer l'erreur ici
                return StatusCode(500, new { message = "Erreur lors de la génération du token", detail = ex.Message });
            }

        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName!)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
