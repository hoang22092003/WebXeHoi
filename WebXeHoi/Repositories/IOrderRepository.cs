using WebXeHoi.Models;

namespace WebXeHoi.Repository
{
    public interface IOrderRepositorycs
    {
        Task<IEnumerable<Order>> GetAllAsync();
    }
}