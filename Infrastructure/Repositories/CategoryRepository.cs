using System.Net;
using Microsoft.Extensions.Logging;
using Dapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApi.DTOs;

public class CategoryRepository(ApplicationDBContext applicationDBContext,ILogger<CategoryRepository> logger) : ICategoryRepository
{
    private readonly ApplicationDBContext _context = applicationDBContext;
    private readonly ILogger<CategoryRepository> _logger = logger;

    public async Task AddAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int CategoryId)
    {
        var delete = await _context.Categories.FindAsync(CategoryId);
        _context.RemoveRange(delete);
        await _context.SaveChangesAsync();
    }

    public async Task<PagedResult<Category>> GetAllCategoriesAsync(CategoryFilter filter, PagedQuery pagedQuery)
    {
        var page = pagedQuery.Page <= 0 ? 1 : pagedQuery.Page;
        var pageSize = pagedQuery.PageSize <= 0 ? 10 : pagedQuery.PageSize;

        IQueryable<Category> query = _context.Categories.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter?.Name))
            query = query.Where(x => x.Name.Contains(filter.Name));

        var totalCount = await query.CountAsync();

        query = query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);

        var items = await query.ToListAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedResult<Category>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }
}