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

    public async Task<Response<string>> AddPaymentAsync(Payment Payment)
    {
        try
        {
            _context.Payments.Add(Payment);
            await _context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Payment was added successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int PaymentId)
    {
        try
        {
            var delete = await _context.Payments.FindAsync(PaymentId);
            _context.RemoveRange(delete);
            await _context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Payment was deleted successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
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

    public async Task<Response<Payment>> GetPaymentByIdAsync(int PaymentId)
    {
        try
        {
            var res = await _context.Payments.FindAsync(PaymentId);
            return new Response<Payment>(HttpStatusCode.OK,"The data that you were searching for:",res);
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<Payment>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<List<Payment>> GetPaymentByOrderIdAsync(int OrderId)
    {
        return await _context.Payments.Include(a => a.Order).Where(a => a.OrderId == OrderId).ToListAsync();
    }

    public async Task<Response<string>> UpdateAsync(int PaymentId,Payment Payment)
    {
        try
        {
            var res = await _context.Payments.FindAsync(PaymentId);
            _context.Update(Payment);
            if (res == null)
            {
                return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
            }
            else
            {
                await _context.SaveChangesAsync();
                return new Response<string>(HttpStatusCode.OK, "Payment was updated successfully");
            }
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }
}