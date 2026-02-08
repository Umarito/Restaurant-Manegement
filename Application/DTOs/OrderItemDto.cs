public class OrderItemInsertDto
{
    public int OrderId{get;set;}
    public int MenuItemId{get;set;}
    public int Quantity{get;set;}
    public decimal Price{get;set;}
}
public class OrderItemUpdateDto
{
    public int Id{get;set;}
    public int OrderId{get;set;}
    public int MenuItemId{get;set;}
    public int Quantity{get;set;}
    public decimal Price{get;set;}
}
public class OrderItemGetDto
{
    public int Id{get;set;}
    public int OrderId{get;set;}
    public int MenuItemId{get;set;}
    public int Quantity{get;set;}
    public decimal Price{get;set;}
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
}
public class OrderItemGetWithOrderDto
{
    public int Id{get;set;}
    public int OrderId{get;set;}
    public Order? Order{get;set;}
    public int MenuItemId{get;set;}
    public MenuItem? MenuItem{get;set;}
    public int Quantity{get;set;}
    public decimal Price{get;set;}
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
}