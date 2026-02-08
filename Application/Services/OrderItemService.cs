using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WebApi.DTOs;

public class OrderItemService(IMapper mapper,IOrderItemRepository OrderItemRepository) : IOrderItemService
{
    private readonly IOrderItemRepository _OrderItemRepository = OrderItemRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Response<string>> AddOrderItemAsync(OrderItemInsertDto OrderItemInsertDto)
    {
        var OrderItem = _mapper.Map<OrderItem>(OrderItemInsertDto);
        return await _OrderItemRepository.AddOrderItemAsync(OrderItem);
    }

    public async Task<Response<string>> DeleteAsync(int OrderItemId)
    {
        return await _OrderItemRepository.DeleteAsync(OrderItemId);
    }

    public async Task<PagedResult<OrderItemGetDto>> GetAllOrderItemsAsync(OrderItemFilter filter, PagedQuery query)
    {
        var OrderItems = await _OrderItemRepository.GetAllOrderItemsAsync(filter,query);
        return _mapper.Map<PagedResult<OrderItemGetDto>>(OrderItems);
    }

    public async Task<Response<OrderItemGetDto>> GetOrderItemByIdAsync(int OrderItemId)
    {
        var OrderItem = await _OrderItemRepository.GetOrderItemByIdAsync(OrderItemId);
        return _mapper.Map<Response<OrderItemGetDto>>(OrderItem);
    }

    public async Task<Response<string>> UpdateAsync(int OrderItemId,OrderItemUpdateDto OrderItemUpdateDto)
    {
        var OrderItem = _mapper.Map<OrderItem>(OrderItemUpdateDto); 
        return await _OrderItemRepository.UpdateAsync(OrderItemId,OrderItem);
    }
}