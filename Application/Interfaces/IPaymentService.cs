using WebApi.DTOs;

public interface IPaymentService
{
    Task<Response<string>> AddPaymentAsync(PaymentInsertDto PaymentInsertDto);
    Task<Response<PaymentGetDto>> GetPaymentByIdAsync(int PaymentId);
    Task<PagedResult<PaymentGetDto>> GetAllPaymentsAsync(PaymentFilter filter, PagedQuery query);
    Task<Response<string>> DeleteAsync(int PaymentId);
    Task<Response<string>> UpdateAsync(int PaymentId,PaymentUpdateDto PaymentUpdateDto);
    Task<List<PaymentGetWithOrderDto>> GetPaymentByOrderIdAsync(int OrderId);
}