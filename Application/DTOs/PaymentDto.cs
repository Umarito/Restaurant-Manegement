public class PaymentInsertDto
{
    public int OrderId{get;set;}
    public decimal Amount{get;set;}
    public PaymentType PaymentType{get;set;}
    public DateTime PaidAt{get;set;}=DateTime.UtcNow;
}
public class PaymentUpdateDto
{
    public int Id{get;set;}
    public int OrderId{get;set;}
    public decimal Amount{get;set;}
    public PaymentType PaymentType{get;set;}
    public DateTime PaidAt{get;set;}=DateTime.UtcNow;
}
public class PaymentGetDto
{
    public int Id{get;set;}
    public int OrderId{get;set;}
    public decimal Amount{get;set;}
    public PaymentType PaymentType{get;set;}
    public DateTime PaidAt{get;set;}=DateTime.UtcNow;
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
}
public class PaymentGetWithOrderDto
{
    public int Id{get;set;}
    public int OrderId{get;set;}
    public Order? Order{get;set;}
    public decimal Amount{get;set;}
    public PaymentType PaymentType{get;set;}
    public DateTime PaidAt{get;set;}=DateTime.UtcNow;
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
}