using Microsoft.EntityFrameworkCore;
using WebXeHoi.DataAccess;
using WebXeHoi.Models;
using WebXeHoi.Repositories;

namespace WebXeHoi.Repositories
{
    public class EFOrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public EFOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }
    }
}
