using System.Net;
using Microsoft.Extensions.Logging;
using Dapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApi.DTOs;

public class TableRepository(ApplicationDBContext applicationDBContext,ILogger<TableRepository> logger) : ITableRepository
{
    private readonly ApplicationDBContext _context = applicationDBContext;
    private readonly ILogger<TableRepository> _logger = logger;

    public async Task<Response<string>> AddTableAsync(Table Table)
    {
        try
        {
            _context.Tables.Add(Table);
            await _context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Table was added successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int TableId)
    {
        try
        {
            var delete = await _context.Tables.FindAsync(TableId);
            _context.RemoveRange(delete);
            await _context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Table was deleted successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<PagedResult<Table>> GetAllTablesAsync(TableFilter filter, PagedQuery pagedQuery)
    {
        var page = pagedQuery.Page <= 0 ? 1 : pagedQuery.Page;
        var pageSize = pagedQuery.PageSize <= 0 ? 10 : pagedQuery.PageSize;

        IQueryable<Table> query = _context.Tables.AsNoTracking();

        if (filter?.IsAvailable != null)
        query = query.Where(x => x.IsAvailable == filter.IsAvailable);

        var totalCount = await query.CountAsync();

        query = query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);

        var items = await query.ToListAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedResult<Table>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }

    public async Task<Response<Table>> GetTableByIdAsync(int TableId)
    {
        try
        {
            var res = await _context.Tables.FindAsync(TableId);
            return new Response<Table>(HttpStatusCode.OK,"The data that you were searching for:",res);
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<Table>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(int TableId,Table Table)
    {
        try
        {
            var res = await _context.Tables.FindAsync(TableId);
            _context.Update(Table);
            if (res == null)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
            else
            {
                await _context.SaveChangesAsync();
                return new Response<string>(HttpStatusCode.OK, "Table was updated successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}