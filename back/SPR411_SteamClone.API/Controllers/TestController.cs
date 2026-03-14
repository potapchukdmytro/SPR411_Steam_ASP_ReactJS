using Microsoft.AspNetCore.Mvc;
using SPR411_SteamClone.DAL.Entities;

namespace SPR411_SteamClone.API.Controllers
{
    public class Pagination
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
    }

    public class Register
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }

    public class User
    {
        public string UserName { get; set; }
        public IFormFile Image { get; set; }
    }

    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        [HttpGet("{value}")]
        public IActionResult RouteValue([FromRoute]string value)
        {
            return Ok(value);
        }

        [HttpGet("query")]
        public IActionResult QueryParams([FromQuery]Pagination pagination)
        {
            return Ok(pagination);
        }

        [HttpPost("body-json")]
        public IActionResult BodyJson([FromBody] Register dto)
        {
            return Ok(dto);
        }

        [HttpPost("body-formdata")]
        public IActionResult BodyFormdata([FromForm] User dto)
        {
            return Ok(dto);
        }
    }
}
