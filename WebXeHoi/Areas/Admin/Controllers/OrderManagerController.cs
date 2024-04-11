using Microsoft.AspNetCore.Mvc;

namespace WebXeHoi.Areas.Admin.Controllers
{
    public class OrderManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
