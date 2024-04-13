using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using WebXeHoi.Helper;
using WebXeHoi.Models;
using WebXeHoi.Repositories;
using WebXeHoi.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace WebXeHoi.Controllers
{
    public class ShoppingCartController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly IProductRepository _productRepository;
		private readonly UserManager<ApplicationUser> _userManager;

		public ShoppingCartController(IProductRepository productRepository, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _productRepository = productRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            // Giả sử bạn có phương thức lấy thông tin sản phẩm từ productId
            Product product = await GetProductFromDatabase(productId);
            var cartItem = new CartItem
             {
                 ProductId = productId,
                 Name = product.Name,
                 Price = product.Price,
                 Quantity = quantity
             };
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            cart.AddItem(cartItem);
             HttpContext.Session.SetObjectAsJson("Cart", cart);
             return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            return View(cart);
        }

		[Authorize]
		[HttpGet]
		public IActionResult Checkout()
		{
			return View(new Order());
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Checkout(Order order)
		{
			var cart =
			HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
			if (cart == null || !cart.Items.Any())
			{
				// Xử lý giỏ hàng trống...
				return RedirectToAction("Index");
			}
			var user = await _userManager.GetUserAsync(User);
			order.UserId = user.Id;
			order.OrderDate = DateTime.UtcNow;
			order.TotalPrice = cart.Items.Sum(i => i.Price * i.Quantity);
			order.OrderDetails = cart.Items.Select(i => new OrderDetail
			{
				ProductId = i.ProductId,
				Quantity = i.Quantity,
				Price = i.Price
			}).ToList();
			_context.Orders.Add(order);
			await _context.SaveChangesAsync();
			HttpContext.Session.Remove("Cart");
			return View("OrderCompleted", order.Id); // Trang xác nhận hoànthành đơn hàng
        }
		private async Task<Product> GetProductFromDatabase(int productId)
        {
            return await _productRepository.GetByIdAsync(productId);
        }
		public IActionResult RemoveFromCart(int productID) 
		{
			var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
			if(cart is not null)
			{
				cart.RemoveItem(productID);
				HttpContext.Session.SetObjectAsJson("Cart", cart);
			}
			return RedirectToAction("Index");
		}
        public async Task<IActionResult> UpdateQuantityAsync(int productId, int quantity)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            cart.UpdateQuantity(productId, quantity);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index");
        }
    }
}

