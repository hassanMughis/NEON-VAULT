using Microsoft.AspNetCore.Mvc;

namespace Neon_vault.Controllers
{
    /// <summary>
    /// Home controller redirects to the Store page.
    /// </summary>
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Store");
        }
    }
}
