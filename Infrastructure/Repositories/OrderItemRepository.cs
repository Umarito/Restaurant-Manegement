using System.Net;
using Microsoft.Extensions.Logging;
using Dapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApi.DTOs;

public class OrderItemRepository(ApplicationDBContext applicationDBContext,ILogger<OrderItemRepository> logger) : IOrderItemRepository
{
    private readonly ApplicationDBContext _context = applicationDBContext;
    private readonly ILogger<OrderItemRepository> _logger = logger;

    public async Task<Response<string>> AddOrderItemAsync(OrderItem OrderItem)
    {
        try
        {
            _context.OrderItems.Add(OrderItem);
            await _context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "OrderItem was added successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int OrderItemId)
    {
        try
        {
            var delete = await _context.OrderItems.FindAsync(OrderItemId);
            _context.RemoveRange(delete);
            await _context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "OrderItem was deleted successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<PagedResult<OrderItem>> GetAllOrderItemsAsync(OrderItemFilter filter, PagedQuery pagedQuery)
    {
        var page = pagedQuery.Page <= 0 ? 1 : pagedQuery.Page;
        var pageSize = pagedQuery.PageSize <= 0 ? 10 : pagedQuery.PageSize;

        IQueryable<OrderItem> query = _context.OrderItems.AsNoTracking();

        if (filter?.Price > 0)
        query = query.Where(x => x.Price >= filter.Price);

        var totalCount = await query.CountAsync();

        query = query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);

        var items = await query.ToListAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedResult<OrderItem>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }

    public async Task<Response<OrderItem>> GetOrderItemByIdAsync(int OrderItemId)
    {
        try
        {
            var res = await _context.OrderItems.FindAsync(OrderItemId);
            return new Response<OrderItem>(HttpStatusCode.OK,"The data that you were searching for:",res);
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<OrderItem>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(int OrderItemId,OrderItem OrderItem)
    {
        try
        {
            var res = await _context.OrderItems.FindAsync(OrderItemId);
            _context.Update(OrderItem);
            if (res == null)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
            else
            {
                await _context.SaveChangesAsync();
                return new Response<string>(HttpStatusCode.OK, "OrderItem was updated successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}