using WebApi.DTOs;

public interface IOrderService
{
    Task<Response<string>> AddOrderAsync(OrderInsertDto OrderInsertDto);
    Task<Response<OrderGetDto>> GetOrderByIdAsync(int OrderId);
    Task<PagedResult<OrderGetDto>> GetAllOrdersAsync(OrderFilter filter, PagedQuery query);
    Task<Response<string>> DeleteAsync(int OrderId);
    Task<Response<string>> UpdateAsync(int OrderId,OrderUpdateDto OrderUpdateDto);
    Task<List<OrderGetWithTableAndWaiterJoinDto>> GetOrderByWaiterIdAsync(int WaiterId);
}