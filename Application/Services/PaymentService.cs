using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WebApi.DTOs;

public class PaymentService(IMapper mapper,IPaymentRepository PaymentRepository,ILogger<PaymentService> logger) : IPaymentService
{
    private readonly IPaymentRepository _PaymentRepository = PaymentRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<PaymentService> _logger = logger;

    public async Task<Response<string>> AddPaymentAsync(PaymentInsertDto PaymentInsertDto)
    {
        try
        {
            var Payment = _mapper.Map<Payment>(PaymentInsertDto);
            await _PaymentRepository.AddAsync(Payment);
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
            await _PaymentRepository.DeleteAsync(PaymentId);
            return new Response<string>(HttpStatusCode.OK, "Payment was deleted successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<PaymentGetDto>> GetPaymentByIdAsync(int PaymentId)
    {
        try
        {
            var Payment = await _PaymentRepository.GetByIdAsync(PaymentId);
            var res = _mapper.Map<PaymentGetDto>(Payment);
            return new Response<PaymentGetDto>(HttpStatusCode.OK,"The data that you were searching for:",res);
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<PaymentGetDto>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(int PaymentId, PaymentUpdateDto Payment)
    {
        try
        {
            var res = await _PaymentRepository.GetByIdAsync(PaymentId);

            if (res == null)
            {   
                return new Response<string>(HttpStatusCode.NotFound,"Payment not found");
            }
            else
            {
                _mapper.Map(Payment, res);
                await _PaymentRepository.UpdateAsync(res);
                return new Response<string>(HttpStatusCode.OK,"Payment updated successfully");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }

    public async Task<PagedResult<PaymentGetDto>> GetAllPaymentsAsync(PaymentFilter filter, PagedQuery query)
    {
        var Payments = await _PaymentRepository.GetAllPaymentsAsync(filter,query);
        return _mapper.Map<PagedResult<PaymentGetDto>>(Payments);
    }
    
    public async Task<List<PaymentGetWithOrderDto>> GetPaymentByOrderIdAsync(int OrderId)
    {
        var Payment = await _PaymentRepository.GetPaymentByOrderIdAsync(OrderId);
        return _mapper.Map<List<PaymentGetWithOrderDto>>(Payment);
    }
}