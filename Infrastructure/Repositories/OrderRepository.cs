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

    public async Task AddAsync(Order Order)
    {
        _context.Orders.Add(Order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int OrderId)
    {
        var delete = await _context.Orders.FindAsync(OrderId);
        _context.RemoveRange(delete);
        await _context.SaveChangesAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task UpdateAsync(Order Order)
    {
        _context.Orders.Update(Order);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Order>> GetOrderByWaiterIdAsync(int WaiterId)
    {
        return await _context.Orders.Include(a => a.Waiter).Include(a => a.Table).Where(a => a.WaiterId==WaiterId).ToListAsync();
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
}