using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.DAL.Repositories;

namespace SPR411_SteamClone.API.Controllers
{
    [ApiController]
    [Route("api/genre")]
    public class GenreController : ControllerBase
    {
        private readonly GenreRepository _genreRepository;

        public GenreController(GenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var genres = await _genreRepository.Genres.ToListAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            return Ok(genre);
        }
    }
}
