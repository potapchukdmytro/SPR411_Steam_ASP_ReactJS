using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SPR411_SteamClone.BLL.Dtos.Auth;
using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.BLL.Services
{
    public class AuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly ILogger<AuthService> _logger;
        private readonly JwtService _jwtService;
        private readonly IMapper _mapper;

        public AuthService(UserManager<UserEntity> userManager, JwtService jwtService, ILogger<AuthService> logger, IMapper mapper)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ServiceResponse> RegisterAsync(RegisterDto dto)
        {
            if(await UsernameIsExistAsync(dto.UserName))
            {
                return ServiceResponse.Error($"Користувач з іменем '{dto.UserName}' вже існує");
            }

            if(await EmailIsExistAsync(dto.Email))
            {
                return ServiceResponse.Error($"Email '{dto.Email}' вже використовується");
            }

            var user = _mapper.Map<UserEntity>(dto);

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return ServiceResponse.Error($"Помилка реєстрації. {result.Errors.First().Description}");
            }

            await _userManager.AddToRoleAsync(user, "user");

            return ServiceResponse.Success("Успішна реєстрація");
        }

        public async Task<ServiceResponse> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.UserName);

            if(user == null)
            {
                _logger.LogWarning("[{Date}] - Login attempt failed for username: {UserName}", DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"), dto.UserName);
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

        private async Task<bool> UsernameIsExistAsync(string userName)
        {
            return await _userManager.Users
                .AnyAsync(u => u.NormalizedUserName == userName.ToUpper());
        }

        private async Task<bool> EmailIsExistAsync(string email)
        {
            return await _userManager.Users
                .AnyAsync(u => u.NormalizedEmail == email.ToUpper());
        }
    }
}
