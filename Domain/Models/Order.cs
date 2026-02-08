public class Order : BaseEntity
{
    public int TableId{get;set;}
    public Table? Table{get;set;}
    public int WaiterId{get;set;}
    public User? Waiter{get;set;}
    public OrderStatus Status{get;set;}
    public List<OrderItem> OrderItems{get;set;}=new();
    public Payment? Payment{get;set;}
}