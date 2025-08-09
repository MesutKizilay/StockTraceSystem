using Core.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using StockTraceSystem.Application.Feature.Auth.Commands.Login;

namespace StockTraceSystem.Presentation.Controllers
{
    public class AuthController : BaseController
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto };
            LoggedResponse result = await Mediator.Send(loginCommand);

            return Json(result);
        }
    }
}