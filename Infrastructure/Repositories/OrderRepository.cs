using System.Net;
using Microsoft.Extensions.Logging;
using Dapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApi.DTOs;

public class OrderRepository(ApplicationDBContext applicationDBContext,ILogger<OrderRepository> logger) : IOrderRepository
{
    private readonly ApplicationDBContext _context = applicationDBContext;
    private readonly ILogger<OrderRepository> _logger = logger;

    public async Task<Response<string>> AddOrderAsync(Order Order)
    {
        try
        {
            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();
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
            var delete = await _context.Orders.FindAsync(OrderId);
            _context.RemoveRange(delete);
            await _context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Order was deleted successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<PagedResult<Order>> GetAllOrdersAsync(OrderFilter filter, PagedQuery pagedQuery)
    {
        var page = pagedQuery.Page <= 0 ? 1 : pagedQuery.Page;
        var pageSize = pagedQuery.PageSize <= 0 ? 10 : pagedQuery.PageSize;

        IQueryable<Order> query = _context.Orders.AsNoTracking();

        if (filter?.Status != null)
        {
            query = query.Where(x => x.Status == filter.Status);
        }

        var totalCount = await query.CountAsync();

        query = query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);

        var items = await query.ToListAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedResult<Order>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }

    public async Task<Response<Order>> GetOrderByIdAsync(int OrderId)
    {
        try
        {
            var res = await _context.Orders.FindAsync(OrderId);
            return new Response<Order>(HttpStatusCode.OK,"The data that you were searching for:",res);
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<Order>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<List<Order>> GetOrderByWaiterIdAsync(int WaiterId)
    {
        return await _context.Orders.Include(a => a.Waiter).Include(a => a.Table).Where(a => a.WaiterId==WaiterId).ToListAsync();
    }

    public async Task<Response<string>> UpdateAsync(int OrderId,Order Order)
    {
        try
        {
            var res = await _context.Orders.FindAsync(OrderId);
            _context.Update(Order);
            if (res == null)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
            else
            {
                await _context.SaveChangesAsync();
                return new Response<string>(HttpStatusCode.OK, "Order was updated successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}