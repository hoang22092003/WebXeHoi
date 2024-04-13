using WebXeHoi.Models;

namespace WebXeHoi.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
    }
}