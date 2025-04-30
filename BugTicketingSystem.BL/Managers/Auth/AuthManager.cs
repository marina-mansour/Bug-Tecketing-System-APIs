
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BugTicketingSystem.BL.Helpers;
using BugTicketingSystem.DAL;
using BugTrackingSystem.BL;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BugTicketingSystem.BL
{
    public class AuthManager : IAuthManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthManager(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(UserLoginDTO dto)
        {
            var user = await _userRepository.GetUserByEmailAsync(dto.Email);
            if (user == null || !(PasswordHelper.VerifyPassword(dto.Password, user.Password)))
                return null;

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

            //Console.WriteLine($"😎🙌❤Role claim value------------->>>: {user.Role.ToString()}");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
