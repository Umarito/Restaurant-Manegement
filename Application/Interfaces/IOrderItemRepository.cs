using WebApi.DTOs;

public interface IOrderItemRepository
{
    Task<Response<string>> AddOrderItemAsync(OrderItem OrderItem);
    Task<Response<OrderItem>> GetOrderItemByIdAsync(int OrderItemId);
    Task<PagedResult<OrderItem>> GetAllOrderItemsAsync(OrderItemFilter filter, PagedQuery query);
    Task<Response<string>> DeleteAsync(int OrderItemId);
    Task<Response<string>> UpdateAsync(int OrderItemId,OrderItem OrderItem);
}