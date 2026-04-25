using Microsoft.AspNetCore.Mvc;
using SPR411_SteamClone.API.Extensions;
using SPR411_SteamClone.BLL.Dtos.Auth;
using SPR411_SteamClone.BLL.Services;

namespace SPR411_SteamClone.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            //_logger.LogInformation($"[{DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss")}] - User login\n\tusername: {dto.UserName}\n\tpassword: {dto.Password}");

            var response = await _authService.LoginAsync(dto);
            return this.GetResult(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterDto dto)
        {
            var response = await _authService.RegisterAsync(dto);
            return this.GetResult(response);
        }
    }
}
