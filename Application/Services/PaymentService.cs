using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WebApi.DTOs;

public class PaymentService(IMapper mapper,IPaymentRepository PaymentRepository) : IPaymentService
{
    private readonly IPaymentRepository _PaymentRepository = PaymentRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Response<string>> AddPaymentAsync(PaymentInsertDto PaymentInsertDto)
    {
        var Payment = _mapper.Map<Payment>(PaymentInsertDto);
        return await _PaymentRepository.AddPaymentAsync(Payment);
    }

    public async Task<Response<string>> DeleteAsync(int PaymentId)
    {
        return await _PaymentRepository.DeleteAsync(PaymentId);
    }

    public async Task<PagedResult<PaymentGetDto>> GetAllPaymentsAsync(PaymentFilter filter, PagedQuery query)
    {
        var Payments = await _PaymentRepository.GetAllPaymentsAsync(filter,query);
        return _mapper.Map<PagedResult<PaymentGetDto>>(Payments);
    }

    public async Task<Response<PaymentGetDto>> GetPaymentByIdAsync(int PaymentId)
    {
        var Payment = await _PaymentRepository.GetPaymentByIdAsync(PaymentId);
        return _mapper.Map<Response<PaymentGetDto>>(Payment);
    }

    public async Task<List<PaymentGetWithOrderDto>> GetPaymentByOrderIdAsync(int OrderId)
    {
        var Payment = await _PaymentRepository.GetPaymentByOrderIdAsync(OrderId);
        return _mapper.Map<List<PaymentGetWithOrderDto>>(Payment);
    }

    public async Task<Response<string>> UpdateAsync(int PaymentId,PaymentUpdateDto PaymentUpdateDto)
    {
        var Payment = _mapper.Map<Payment>(PaymentUpdateDto); 
        return await _PaymentRepository.UpdateAsync(PaymentId,Payment);
    }
}