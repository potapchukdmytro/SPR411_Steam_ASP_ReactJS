using Microsoft.AspNetCore.Mvc;
using SPR411_SteamClone.BLL.Services;

namespace SPR411_SteamClone.API.Extensions
{
    public static class ControllerBaseExtesions
    {
        public static IActionResult GetResult(this ControllerBase controller, ServiceResponse response)
        {
            return response.IsSuccess 
                ? controller.Ok(response) 
                : controller.BadRequest(response);
        }
    }
}
