using WebXeHoi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebXeHoi.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class OrderManagerController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderManagerController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;

        }
        public async Task<IActionResult> Index()
        {
            var orders = await _orderRepository.GetAllAsync();
            return View(orders);
        }
    }
}