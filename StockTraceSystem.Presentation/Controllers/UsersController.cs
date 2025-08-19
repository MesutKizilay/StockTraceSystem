using Core.Application.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockTraceSystem.Application.Feature.Users.Commands.Create;
using StockTraceSystem.Application.Feature.Users.Commands.Delete;
using StockTraceSystem.Application.Feature.Users.Commands.Update;
using StockTraceSystem.Application.Feature.Users.Queries.GetList;
using System.Security.Claims;

namespace StockTraceSystem.Presentation.Controllers
{
    public class UsersController : BaseController
    {
        public IActionResult Users()
        {
            ViewBag.Claim = User.FindFirst(ClaimTypes.Role)?.Value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetList(PageRequest pageRequest)
        {
            GetListUserQuery getListUserQuery = new GetListUserQuery() { PageRequest = pageRequest };
            var users = await Mediator.Send(getListUserQuery);
            return Json(users);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateUserCommand updateUserCommand)
        {
            await Mediator.Send(updateUserCommand);
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand createUserCommand)
        {
            await Mediator.Send(createUserCommand);
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            DeleteUserCommand deleteUserCommand = new DeleteUserCommand() { Id = id };

            await Mediator.Send(deleteUserCommand);
            return Json(true);
        }
    }
}