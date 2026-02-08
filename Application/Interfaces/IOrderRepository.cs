using WebApi.DTOs;

public interface IOrderRepository
{
    Task<Response<string>> AddOrderAsync(Order Order);
    Task<Response<Order>> GetOrderByIdAsync(int OrderId);
    Task<PagedResult<Order>> GetAllOrdersAsync(OrderFilter filter, PagedQuery query);
    Task<Response<string>> DeleteAsync(int OrderId);
    Task<Response<string>> UpdateAsync(int OrderId,Order Order);
    Task<List<Order>> GetOrderByWaiterIdAsync(int WaiterId);
}