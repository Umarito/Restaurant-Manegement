using System.Net;
using Microsoft.Extensions.Logging;
using Dapper;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApi.DTOs;

public class PaymentRepository(ApplicationDBContext applicationDBContext,ILogger<PaymentRepository> logger) : IPaymentRepository
{
    private readonly ApplicationDBContext _context = applicationDBContext;
    private readonly ILogger<PaymentRepository> _logger = logger;

    public async Task AddAsync(Payment Payment)
    {
        _context.Payments.Add(Payment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int PaymentId)
    {
        var delete = await _context.Payments.FindAsync(PaymentId);
        _context.RemoveRange(delete);
        await _context.SaveChangesAsync();
    }

    public async Task<Payment?> GetByIdAsync(int id)
    {
        return await _context.Payments.FindAsync(id);
    }

    public async Task UpdateAsync(Payment Payment)
    {
        _context.Payments.Update(Payment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Payment>> GetPaymentByOrderIdAsync(int OrderId)
    {
        return await _context.Payments.Include(a => a.Order).Where(a => a.OrderId == OrderId).ToListAsync();
    }

    public async Task<PagedResult<Payment>> GetAllPaymentsAsync(PaymentFilter filter, PagedQuery pagedQuery)
    {
        var page = pagedQuery.Page <= 0 ? 1 : pagedQuery.Page;
        var pageSize = pagedQuery.PageSize <= 0 ? 10 : pagedQuery.PageSize;

        IQueryable<Payment> query = _context.Payments.AsNoTracking();

        if (filter?.Amount > 0)
        query = query.Where(x => x.Amount == filter.Amount);

        if (filter?.PaidAt != null)
        query = query.Where(x => x.PaidAt >= filter.PaidAt);

        if (filter?.PaymentType != null)
        {
            query = query.Where(x => x.PaymentType == filter.PaymentType);
        }

        var totalCount = await query.CountAsync();

        query = query.OrderBy(x => x.Id).Skip((page - 1) * pageSize).Take(pageSize);

        var items = await query.ToListAsync();

        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedResult<Payment>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages
        };
    }
}