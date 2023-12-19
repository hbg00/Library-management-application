using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
