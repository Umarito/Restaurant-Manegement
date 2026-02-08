public class MenuItem : BaseEntity
{
    public string Name{get;set;}=null!;
    public decimal Price{get;set;}
    public bool IsAvailable{get;set;}=true;
    public int CategoryId{get;set;}
    public Category? Category{get;set;}
    public List<OrderItem> OrderItems{get;set;}=new();
}