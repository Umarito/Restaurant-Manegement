using System.Net;
using Microsoft.Extensions.Logging;
using Dapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApi.DTOs;

public class UserRepository(ApplicationDBContext applicationDBContext,ILogger<UserRepository> logger) : IUserRepository
{
    private readonly ApplicationDBContext _context = applicationDBContext;
    private readonly ILogger<UserRepository> _logger = logger;

    public async Task AddAsync(User User)
    {
        _context.Users.Add(User);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int UserId)
    {
        var delete = await _context.Users.FindAsync(UserId);
        _context.RemoveRange(delete);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task UpdateAsync(User User)
    {
        _context.Users.Update(User);
        await _context.SaveChangesAsync();
    }
    public async Task<List<User>> GetAllOrdersOfWaiterByIdAsync(int WaiterId)
    {
        return await _context.Users.Include(a=>a.OrdersAsWaiter).Where(a => a.Id==WaiterId && a.Role == UserRoles.Waiter).ToListAsync();
    }

    public async Task<PagedResult<User>> GetAllUsersAsync(UserFilter filter, PagedQuery pagedQuery)
    {
        var page = pagedQuery.Page <= 0 ? 1 : pagedQuery.Page;
        var pageSize = pagedQuery.PageSize <= 0 ? 10 : pagedQuery.PageSize;

        IQueryable<User> query = _context.Users.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter?.FullName))
            query = query.Where(x => x.FullName.Contains(filter.FullName));

        if (!string.IsNullOrWhiteSpace(filter?.Phone))
            query = query.Where(x => x.Phone.Contains(filter.Phone));  
        
        if (filter?.Role != null)
        {
            query = query.Where(x => x.Role == filter.Role);
        }

        var totalCount = await query.CountAsync();

        query = query
            .OrderBy(x => x.Id) 
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        var items = await query.ToListAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedResult<User>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }
}