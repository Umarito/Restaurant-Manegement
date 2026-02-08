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

    public async Task<Response<string>> AddMenuItemAsync(MenuItem MenuItem)
    {
        try
        {
            _context.MenuItems.Add(MenuItem);
            await _context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "MenuItem was added successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int MenuItemId)
    {
        try
        {
            var delete = await _context.MenuItems.FindAsync(MenuItemId);
            _context.RemoveRange(delete);
            await _context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "MenuItem was deleted successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
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

    public async Task<Response<MenuItem>> GetMenuItemByIdAsync(int MenuItemId)
    {
        try
        {
            var res = await _context.MenuItems.FindAsync(MenuItemId);
            return new Response<MenuItem>(HttpStatusCode.OK,"The data that you were searching for:",res);
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<MenuItem>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<List<MenuItem>> GetMenuItemsByCategoryIdAsync(int categoryId)
    {
        return await _context.MenuItems.Include(a => a.Category).Where(a => a.CategoryId == categoryId).ToListAsync();
    }

    public async Task<Response<string>> UpdateAsync(int MenuItemId,MenuItem MenuItem)
    {
        try
        {
            var res = await _context.MenuItems.FindAsync(MenuItemId);
            _context.Update(MenuItem);
            if (res == null)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
            else
            {
                await _context.SaveChangesAsync();
                return new Response<string>(HttpStatusCode.OK, "MenuItem was updated successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}