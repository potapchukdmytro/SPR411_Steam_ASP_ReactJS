using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SPR411_SteamClone.BLL.Extensions;
using SPR411_SteamClone.BLL.Settings;
using SPR411_SteamClone.DAL.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SPR411_SteamClone.BLL.Services
{
    public class JwtService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<JwtService> _logger;

        public JwtService(IOptions<JwtSettings> options, ILogger<JwtService> logger)
        {
            _jwtSettings = options.Value;
            _logger = logger;
        }

        public string GetAcessToken(UserEntity user)
        {
            if(!string.IsNullOrEmpty(_jwtSettings.SecretKey))
            {
                _logger.LogInformationWithTimestamp("Jwt secret key is null");
                throw new ArgumentNullException("Jwt secret key is null");
            }

            var claims = new List<Claim>()
            {
                new Claim("userName", user.UserName ?? string.Empty),
                new Claim("email", user.Email ?? string.Empty),
                new Claim("firstName", user.FirstName ?? string.Empty),
                new Claim("lastName", user.LastName ?? string.Empty),
                new Claim("image", user.Image ?? string.Empty)
            };

            var secretKeyBytes = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            var signInKey = new SymmetricSecurityKey(secretKeyBytes);

            var credentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpireHours)
                );

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }
    }
}
