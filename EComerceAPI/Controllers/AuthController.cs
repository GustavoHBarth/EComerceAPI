using EComerceAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EComerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // Método de Registro de Usuário
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.NomeCompleto))
            {
                return BadRequest("Nome Completo é obrigatório.");
            }

            var user = new User
            {
                UserName = request.Username,
                Email = request.Email,
                NomeCompleto = request.NomeCompleto  // Atribuindo o NomeCompleto da requisição
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                // Aqui você pode gerar o token JWT ou retornar uma resposta de sucesso
                return Ok(new { Message = "Usuário criado com sucesso." });
            }

            return BadRequest(result.Errors);
        }


        // Método de Login de Usuário
        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthRequestLogin request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);  // Busca o usuário pelo email
            if (user == null || !(await _userManager.CheckPasswordAsync(user, request.Password)))
                return Unauthorized("Usuário ou senha inválidos");

            // Gerar o Token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(2),  // Expiração do Token (2 horas)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { token = tokenString });
        }

    }
}
