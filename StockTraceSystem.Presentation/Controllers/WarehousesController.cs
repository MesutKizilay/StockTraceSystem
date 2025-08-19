using Microsoft.AspNetCore.Mvc;

namespace StockTraceSystem.Presentation.Controllers
{
    public class WarehousesController : BaseController
    {
        public IActionResult Stocktaking()
        {
            return View();
        }
    }
}