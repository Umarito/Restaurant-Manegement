public class Payment : BaseEntity
{
    public int OrderId{get;set;}
    public Order? Order{get;set;}
    public decimal Amount{get;set;}
    public PaymentType PaymentType{get;set;}
    public DateTime PaidAt{get;set;}
}