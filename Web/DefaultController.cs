using Microsoft.AspNetCore.Mvc;

namespace CoreApp
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
             return File("~/index.html", "text/html");
        }
    }
}