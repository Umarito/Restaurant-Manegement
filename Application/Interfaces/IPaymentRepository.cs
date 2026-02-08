using WebApi.DTOs;

public interface IPaymentRepository
{
    Task<Response<string>> AddPaymentAsync(Payment Payment);
    Task<Response<Payment>> GetPaymentByIdAsync(int PaymentId);
    Task<PagedResult<Payment>> GetAllPaymentsAsync(PaymentFilter filter, PagedQuery query);
    Task<Response<string>> DeleteAsync(int PaymentId);
    Task<Response<string>> UpdateAsync(int PaymentId,Payment Payment);
    Task<List<Payment>> GetPaymentByOrderIdAsync(int OrderId);
}