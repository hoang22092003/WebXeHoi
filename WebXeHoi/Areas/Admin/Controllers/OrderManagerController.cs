using WebXeHoi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebXeHoi.Repository;

namespace WebXeHoi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class OrderManagerController : Controller
    {
        private readonly IOrderRepositorycs _orderRepositorycs;

        public OrderManagerController(IOrderRepositorycs orderRepositorycs)
        {
            _orderRepositorycs = orderRepositorycs;

        }
        public async Task<IActionResult> Index()
        {
            var orders = await _orderRepositorycs.GetAllAsync();
            return View(orders);
        }
    }
}