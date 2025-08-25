using Microsoft.AspNetCore.Mvc;
using StockTraceSystem.Application.Feature.OperationClaims.Queries.GetList;

namespace StockTraceSystem.Presentation.Controllers
{
    public class OperationClaimsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            GetListOperationClaimQuery getListOperationClaimQuery = new GetListOperationClaimQuery() { };
            var users = await Mediator.Send(getListOperationClaimQuery);
            return Json(users);
        }
    }
}