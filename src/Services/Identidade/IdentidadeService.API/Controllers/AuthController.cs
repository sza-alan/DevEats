using IdentidadeService.Application.DTOs;
using IdentidadeService.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace IdentidadeService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) => _authService = authService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                var result = await _authService.RegisterAsync(registerUserDto);

                if (result)
                    return Ok("Usuário registrado com sucesso.");

                return BadRequest("O e-mail informado já está em uso.");
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
            {
                return BadRequest("O e-mail informado já está em uso (detectado pelo banco de dados).");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var token = await _authService.LoginAsync(loginUserDto);

            if (string.IsNullOrEmpty(token))
                return Unauthorized("E-mail ou senha inválidos.");

            return Ok(new { Token = token });
        }
    }
}
