using Microsoft.AspNetCore.Mvc;

namespace Web
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
             return File("~/index.html", "text/html");
        }
    }
}