using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    public class UserBookController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
