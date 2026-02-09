using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService OrderService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> AddOrderAsync(OrderInsertDto Order)
    {
        return await OrderService.AddOrderAsync(Order);
    }
    [HttpPut("{OrderId}")]
    public async Task<Response<string>> UpdateAsync(int OrderId,OrderUpdateDto Order)
    {
        return await OrderService.UpdateAsync(OrderId,Order);
    }
    [HttpDelete("{OrderId}")]
    public async Task<Response<string>> DeleteAsync(int OrderId)
    {
        return await OrderService.DeleteAsync(OrderId);
    }
    [HttpGet]
    public async Task<PagedResult<OrderGetDto>> GetAllOrders([FromQuery] OrderFilter filter, [FromQuery] PagedQuery pagedQuery)
    {
        return await OrderService.GetAllOrdersAsync(filter, pagedQuery);   
    }
    
    [HttpGet("{OrderId}")]
    public async Task<List<OrderGetWithTableAndWaiterJoinDto>> GetOrderByWaiterIdAsync(int WaiterId)
    {
        return await OrderService.GetOrderByWaiterIdAsync(WaiterId);
    }
    
    // [HttpGet("{OrderId}")]
    // public async Task<Response<OrderGetDto>> GetOrderByIdAsync(int OrderId)
    // {
    //     return await OrderService.GetOrderByIdAsync(OrderId);
    // }
}