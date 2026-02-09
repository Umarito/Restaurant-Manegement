using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController(IPaymentService PaymentService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> AddPaymentAsync(PaymentInsertDto Payment)
    {
        return await PaymentService.AddPaymentAsync(Payment);
    }
    [HttpPut("{PaymentId}")]
    public async Task<Response<string>> UpdateAsync(int PaymentId,PaymentUpdateDto Payment)
    {
        return await PaymentService.UpdateAsync(PaymentId,Payment);
    }
    [HttpDelete("{PaymentId}")]
    public async Task<Response<string>> DeleteAsync(int PaymentId)
    {
        return await PaymentService.DeleteAsync(PaymentId);
    }
    [HttpGet]
    public async Task<PagedResult<PaymentGetDto>> GetAllPayments([FromQuery] PaymentFilter filter, [FromQuery] PagedQuery pagedQuery)
    {
        return await PaymentService.GetAllPaymentsAsync(filter, pagedQuery);   
    }
    
    [HttpGet("{PaymentId}")]
    public async Task<List<PaymentGetWithOrderDto>> GetPaymentByOrderIdAsync(int OrderId)
    {
        return await PaymentService.GetPaymentByOrderIdAsync(OrderId);
    }
    // [HttpGet("{PaymentId}")]
    // public async Task<Response<PaymentGetDto>> GetPaymentByIdAsync(int PaymentId)
    // {
    //     return await PaymentService.GetPaymentByIdAsync(PaymentId);
    // }
}