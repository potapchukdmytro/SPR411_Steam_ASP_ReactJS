using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SPR411_SteamClone.API.Extensions;
using SPR411_SteamClone.BLL.Dtos.Genre;
using SPR411_SteamClone.BLL.Services;
using SPR411_SteamClone.DAL.Repositories;
using System.Xml.Linq;

namespace SPR411_SteamClone.API.Controllers
{
    [ApiController]
    [Route("api/genre")]
    public class GenreController : ControllerBase
    {
        private readonly GenreService _genreService;

        public GenreController(GenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _genreService.GetAllAsync();
            return this.GetResult(response);
        }

        [HttpGet("by-id/{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] int id)
        {
            var response = await _genreService.GetByIdAsync(id);
            return this.GetResult(response);
        }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetAsync([FromRoute] string name)
        {
            var response = await _genreService.GetByNameAsync(name);
            return this.GetResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateGenreDto dto)
        {
            var response = await _genreService.CreateAsync(dto);
            return this.GetResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> CreateAsync(UpdateGenreDto dto)
        {
            var response = await _genreService.UpdateAsync(dto);
            return this.GetResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CreateAsync(int id)
        {
            var response = await _genreService.DeleteAsync(id);
            return this.GetResult(response);
        }
    }
}
