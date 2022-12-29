using Microsoft.AspNetCore.Mvc;

namespace ePizzaHub.UI.Areas.User.Controllers
{
    public class DashboardController : BaseController
    {
        [Area("User")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
