using Core.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockTraceSystem.Application.Feature.Auth.Commands.Login;

namespace StockTraceSystem.Presentation.Controllers
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            LoginCommand loginCommand = new() { UserForLoginDto = userForLoginDto };
            LoggedResponse result = await Mediator.Send(loginCommand);

            Response.Cookies.Append("AccessToken", result.AccessToken!.Token, new CookieOptions 
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict, // veya Strict
                //Expires = DateTimeOffset.UtcNow.AddMinutes(10000000),
                Expires = result.AccessToken.Expiration,
                IsEssential = true,
            });

            return Json(result);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete("AccessToken");
            return RedirectToAction("Login", "Auth");
        }
    }
}