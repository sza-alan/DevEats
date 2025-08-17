using IdentidadeService.Application.DTOs;
using IdentidadeService.Application.Interfaces;
using IdentidadeService.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentidadeService.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IPasswordHashingService passwordHashingService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _passwordHashingService = passwordHashingService;
            _configuration = configuration;
        }

        public async Task<string?> LoginAsync(LoginUserDto loginUserDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginUserDto.Email);
            if (user == null) return null;

            var isPasswordValid = _passwordHashingService.Verify(loginUserDto.Password, user.PasswordHash);
            if (!isPasswordValid) return null;

            return GenerateJwtToken(user);
        }

        public async Task<bool> RegisterAsync(RegisterUserDto registerUserDto)
        {
            var existingUser = _userRepository.GetByEmailAsync(registerUserDto.Email);
            if (existingUser == null) return false;

            var passwordHash = _passwordHashingService.Hash(registerUserDto.Password);

            var user = new User(registerUserDto.Email, passwordHash, "Customer");

            await _userRepository.AddAsync(user);

            return true;
        }

        private string GenerateJwtToken(User user)
        {
            var secret = _configuration["Jwt:Secret"];
            if (string.IsNullOrEmpty(secret))
                throw new InvalidOperationException("Segredo JWT nao configurado.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
