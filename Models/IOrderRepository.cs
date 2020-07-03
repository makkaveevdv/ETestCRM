using System.Linq;

namespace ETestCRM.Models
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
        bool UpdateOrderStatus(Order order);
        Order DeleteOrder(int orderId);
    }
}
