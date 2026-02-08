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

    public async Task<Response<string>> AddCategoryAsync(Category Category)
    {
        try
        {
            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Category was added successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int CategoryId)
    {
        try
        {
            var delete = await _context.Categories.FindAsync(CategoryId);
            _context.RemoveRange(delete);
            await _context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Category was deleted successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
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

    public async Task<Response<Category>> GetCategoryByIdAsync(int CategoryId)
    {
        try
        {
            var res = await _context.Categories.FindAsync(CategoryId);
            return new Response<Category>(HttpStatusCode.OK,"The data that you were searching for:",res);
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<Category>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(int CategoryId,Category Category)
    {
        try
        {
            var res = await _context.Categories.FindAsync(CategoryId);
            _context.Categories.Update(Category);
            if (res == null)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
            else
            {
                await _context.SaveChangesAsync();
                return new Response<string>(HttpStatusCode.OK, "Category was updated successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}