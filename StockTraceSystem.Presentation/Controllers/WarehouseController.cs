using Microsoft.AspNetCore.Mvc;

namespace StockTraceSystem.Presentation.Controllers
{
    public class WarehouseController : BaseController
    {
        public IActionResult Stocktaking()
        {
            return View();
        }
    }
}