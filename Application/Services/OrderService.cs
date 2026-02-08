using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WebApi.DTOs;

public class OrderService(IMapper mapper,IOrderRepository OrderRepository) : IOrderService
{
    private readonly IOrderRepository _OrderRepository = OrderRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Response<string>> AddOrderAsync(OrderInsertDto OrderInsertDto)
    {
        var Order = _mapper.Map<Order>(OrderInsertDto);
        return await _OrderRepository.AddOrderAsync(Order);
    }

    public async Task<Response<string>> DeleteAsync(int OrderId)
    {
        return await _OrderRepository.DeleteAsync(OrderId);
    }

    public async Task<PagedResult<OrderGetDto>> GetAllOrdersAsync(OrderFilter filter, PagedQuery query)
    {
        var Orders = await _OrderRepository.GetAllOrdersAsync(filter,query);
        return _mapper.Map<PagedResult<OrderGetDto>>(Orders);
    }

    public async Task<Response<OrderGetDto>> GetOrderByIdAsync(int OrderId)
    {
        var Order = await _OrderRepository.GetOrderByIdAsync(OrderId);
        return _mapper.Map<Response<OrderGetDto>>(Order);
    }

    public async Task<List<OrderGetWithTableAndWaiterJoinDto>> GetOrderByWaiterIdAsync(int WaiterId)
    {
        var Order = await _OrderRepository.GetOrderByWaiterIdAsync(WaiterId);
        return _mapper.Map<List<OrderGetWithTableAndWaiterJoinDto>>(Order);
    }

    public async Task<Response<string>> UpdateAsync(int OrderId,OrderUpdateDto OrderUpdateDto)
    {
        var Order = _mapper.Map<Order>(OrderUpdateDto); 
        return await _OrderRepository.UpdateAsync(OrderId,Order);
    }
}