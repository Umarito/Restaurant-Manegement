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

    public async Task<Response<string>> AddUserAsync(User User)
    {
        try
        {
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "User was added successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int UserId)
    {
        try
        {
            var delete = await _context.Users.FindAsync(UserId);
            _context.RemoveRange(delete);
            await _context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "User was deleted successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
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

    public async Task<Response<User>> GetUserByIdAsync(int UserId)
    {
        try
        {
            var res = await _context.Users.FindAsync(UserId);
            return new Response<User>(HttpStatusCode.OK,"The data that you were searching for:",res);
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<User>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(int UserId,User User)
    {
        try
        {
            var res = await _context.Users.FindAsync(UserId);
            _context.Update(User);
            if (res == null)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
            else
            {
                await _context.SaveChangesAsync();
                return new Response<string>(HttpStatusCode.OK, "User was updated successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}