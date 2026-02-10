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

    public async Task AddAsync(OrderItem OrderItem)
    {
        _context.OrderItems.Add(OrderItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int OrderItemId)
    {
        var delete = await _context.OrderItems.FindAsync(OrderItemId);
        _context.RemoveRange(delete);
        await _context.SaveChangesAsync();
    }

    public async Task<OrderItem?> GetByIdAsync(int id)
    {
        return await _context.OrderItems.FindAsync(id);
    }

    public async Task UpdateAsync(OrderItem OrderItem)
    {
        _context.OrderItems.Update(OrderItem);
        await _context.SaveChangesAsync();
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
}