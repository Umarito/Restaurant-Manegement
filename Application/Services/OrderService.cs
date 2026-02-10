using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WebApi.DTOs;

public class OrderService(IMapper mapper,IOrderRepository OrderRepository,ILogger<OrderService> logger) : IOrderService
{
    private readonly IOrderRepository _OrderRepository = OrderRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<OrderService> _logger = logger;

    public async Task<Response<string>> AddOrderAsync(OrderInsertDto OrderInsertDto)
    {
        try
        {
            var Order = _mapper.Map<Order>(OrderInsertDto);
            await _OrderRepository.AddAsync(Order);
            return new Response<string>(HttpStatusCode.OK, "Order was added successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int OrderId)
    {
        try
        {
            await _OrderRepository.DeleteAsync(OrderId);
            return new Response<string>(HttpStatusCode.OK, "Order was deleted successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<OrderGetDto>> GetOrderByIdAsync(int OrderId)
    {
        try
        {
            var Order = await _OrderRepository.GetByIdAsync(OrderId);
            var res = _mapper.Map<OrderGetDto>(Order);
            return new Response<OrderGetDto>(HttpStatusCode.OK,"The data that you were searching for:",res);
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<OrderGetDto>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(int OrderId, OrderUpdateDto Order)
    {
        try
        {
            var res = await _OrderRepository.GetByIdAsync(OrderId);

            if (res == null)
            {   
                return new Response<string>(HttpStatusCode.NotFound,"Order not found");
            }
            else
            {
                _mapper.Map(Order, res);
                await _OrderRepository.UpdateAsync(res);
                return new Response<string>(HttpStatusCode.OK,"Order updated successfully");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }

    public async Task<PagedResult<OrderGetDto>> GetAllOrdersAsync(OrderFilter filter, PagedQuery query)
    {
        var Orders = await _OrderRepository.GetAllOrdersAsync(filter,query);
        return _mapper.Map<PagedResult<OrderGetDto>>(Orders);
    }
    
    public async Task<List<OrderGetWithTableAndWaiterJoinDto>> GetOrderByWaiterIdAsync(int WaiterId)
    {
        var Order = await _OrderRepository.GetOrderByWaiterIdAsync(WaiterId);
        return _mapper.Map<List<OrderGetWithTableAndWaiterJoinDto>>(Order);
    }
}