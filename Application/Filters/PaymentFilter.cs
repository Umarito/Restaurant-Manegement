public class PaymentFilter
{
    public decimal Amount{get;set;}
    public PaymentType PaymentType{get;set;}
    public DateTime PaidAt{get;set;}=DateTime.UtcNow;
}