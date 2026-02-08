public class OrderInsertDto
{
    public int TableId{get;set;}
    public int WaiterId{get;set;}
    public OrderStatus Status{get;set;}
}
public class OrderUpdateDto
{
    public int Id{get;set;}
    public int TableId{get;set;}
    public int WaiterId{get;set;}
    public OrderStatus Status{get;set;}
}
public class OrderGetDto
{
    public int Id{get;set;}
    public int TableId{get;set;}
    public int WaiterId{get;set;}
    public OrderStatus Status{get;set;}
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
}
public class OrderGetWithTableAndWaiterJoinDto
{
    public int Id{get;set;}
    public int TableId{get;set;}
    public Table? Table{get;set;}
    public int WaiterId{get;set;}
    public User? Waiter{get;set;}
    public List<OrderItem> OrderItems{get;set;}=new();
    public Payment? Payment{get;set;}
    public OrderStatus Status{get;set;}
}