using Microsoft.AspNetCore.Mvc;

namespace HardwareStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}