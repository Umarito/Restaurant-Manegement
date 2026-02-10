using WebApi.DTOs;

public interface IPaymentRepository
{
    Task AddAsync(Payment Payment);
    Task<Payment?> GetByIdAsync(int id);
    Task DeleteAsync(int Payment);
    Task UpdateAsync(Payment Payment);
    Task<PagedResult<Payment>> GetAllPaymentsAsync(PaymentFilter filter, PagedQuery query);
    Task<List<Payment>> GetPaymentByOrderIdAsync(int OrderId);
}