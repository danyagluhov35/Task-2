using Microsoft.AspNetCore.Mvc;

namespace TestTask.Controllers
{
    /// <summary>
    ///     Начальная страница
    /// </summary>
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
