using Microsoft.AspNetCore.Identity;
using SPR411_SteamClone.BLL.Dtos.Auth;
using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.BLL.Services
{
    public class AuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly JwtService _jwtService;

        public AuthService(UserManager<UserEntity> userManager, JwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<ServiceResponse> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if(user == null)
            {
                return ServiceResponse.Error($"Користувача з іменем '{dto.UserName}' не існує");
            }

            bool passwordResult = await _userManager.CheckPasswordAsync(user, dto.Password);

            if(!passwordResult)
            {
                return ServiceResponse.Error("Невірний пароль");
            }

            // Jwt token
            var token = _jwtService.GetAcessToken(user);

            return ServiceResponse.Success("Успішний вхід", token);
        }
    }
}
