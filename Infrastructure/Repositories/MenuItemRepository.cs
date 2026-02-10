using System.Net;
using Microsoft.Extensions.Logging;
using Dapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApi.DTOs;

public class MenuItemRepository(ApplicationDBContext applicationDBContext,ILogger<MenuItemRepository> logger) : IMenuItemRepository
{
    private readonly ApplicationDBContext _context = applicationDBContext;
    private readonly ILogger<MenuItemRepository> _logger = logger;

    public async Task AddAsync(MenuItem MenuItem)
    {
        _context.MenuItems.Add(MenuItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int MenuItemId)
    {
        var delete = await _context.MenuItems.FindAsync(MenuItemId);
        _context.RemoveRange(delete);
        await _context.SaveChangesAsync();
    }

    public async Task<MenuItem?> GetByIdAsync(int id)
    {
        return await _context.MenuItems.FindAsync(id);
    }

    public async Task UpdateAsync(MenuItem MenuItem)
    {
        _context.MenuItems.Update(MenuItem);
        await _context.SaveChangesAsync();
    }

    public async Task<List<MenuItem>> GetMenuItemsByCategoryIdAsync(int categoryId)
    {
        return await _context.MenuItems.Include(a => a.Category).Where(a => a.CategoryId == categoryId).ToListAsync();
    }
    
    public async Task<PagedResult<MenuItem>> GetAllMenuItemsAsync(MenuItemFilter filter, PagedQuery pagedQuery)
    {
        var page = pagedQuery.Page <= 0 ? 1 : pagedQuery.Page;
        var pageSize = pagedQuery.PageSize <= 0 ? 10 : pagedQuery.PageSize;

        IQueryable<MenuItem> query = _context.MenuItems.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter?.Name))
            query = query.Where(x => x.Name.Contains(filter.Name));

        if (filter?.IsAvailable != null)
        query = query.Where(x => x.IsAvailable == filter.IsAvailable);

        if (filter?.Price > 0)
        query = query.Where(x => x.Price >= filter.Price);

        var totalCount = await query.CountAsync();

        query = query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);

        var items = await query.ToListAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedResult<MenuItem>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }
}