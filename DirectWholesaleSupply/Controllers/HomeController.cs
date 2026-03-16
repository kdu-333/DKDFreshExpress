using Microsoft.AspNetCore.Mvc;

namespace DirectWholesaleSupply.Controllers
{
    public class HomeController : Controller
    {
        // Redirect root to Dashboard
        public IActionResult Index() => RedirectToAction("Index", "Dashboard");

        public IActionResult Error() => View();
    }
}
