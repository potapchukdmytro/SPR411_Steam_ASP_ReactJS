using Microsoft.AspNetCore.Mvc;
using SPR411_SteamClone.API.Extensions;
using SPR411_SteamClone.BLL.Dtos.Developer;
using SPR411_SteamClone.BLL.Services;

namespace SPR411_SteamClone.API.Controllers
{
    [ApiController]
    [Route("api/developer")]
    public class DeveloperController : ControllerBase
    {
        private readonly DeveloperService _developerService;

        public DeveloperController(DeveloperService developerService)
        {
            _developerService = developerService;
        }

        // api/developer
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var response = await _developerService.GetAllAsync();
            return this.GetResult(response);
        }

        // api/developer/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int id)
        {
            var response = await _developerService.GetByIdAsync(id);
            return this.GetResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateDeveloperDto dto)
        {
            var response = await _developerService.CreateAsync(dto);
            return this.GetResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateDeveloperDto dto)
        {
            var response = await _developerService.UpdateAsync(dto);
            return this.GetResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
            var response = await _developerService.DeleteAsync(id);
            return this.GetResult(response);
        }
    }
}
