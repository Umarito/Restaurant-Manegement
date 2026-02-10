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

    public async Task AddAsync(Table Table)
    {
        _context.Tables.Add(Table);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int TableId)
    {
        var delete = await _context.Tables.FindAsync(TableId);
        _context.RemoveRange(delete);
        await _context.SaveChangesAsync();
    }

    public async Task<Table?> GetByIdAsync(int id)
    {
        return await _context.Tables.FindAsync(id);
    }

    public async Task UpdateAsync(Table Table)
    {
        _context.Tables.Update(Table);
        await _context.SaveChangesAsync();
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
}