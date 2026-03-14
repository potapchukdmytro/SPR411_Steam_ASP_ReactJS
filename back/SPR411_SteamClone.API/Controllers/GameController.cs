using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.DAL;

namespace SPR411_SteamClone.API.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GameController(AppDbContext context)
        {
            _context = context;
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
        public IActionResult Post()
        {
            return Ok("Це POST метод");
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
