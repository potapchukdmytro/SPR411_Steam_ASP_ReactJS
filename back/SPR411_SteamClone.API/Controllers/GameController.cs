using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.API.Extensions;
using SPR411_SteamClone.BLL.Dtos.Game;
using SPR411_SteamClone.BLL.Services;
using SPR411_SteamClone.DAL;

namespace SPR411_SteamClone.API.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly GameService _gameService;

        public GameController(AppDbContext context, GameService gameService)
        {
            _context = context;
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var games = await _context.Games
                .AsNoTracking()
                .ToListAsync();
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var game = await _context.Games
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if(game != null)
            {
                return Ok(game);
            }
            else
            {
                return BadRequest($"Гра з id '{id}' не знайдена");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateGameDto dto)
        {
            var response = await _gameService.CreateAsync(dto);
            return this.GetResult(response);
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok("Це DELETE метод");
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok("Це PUT метод");
        }

        [HttpPatch]
        public IActionResult Patch()
        {
            return Ok("Це PATCH метод");
        }
    }
}
