public class Table : BaseEntity
{
    public int TableNumber{get;set;}
    public int Capacity{get;set;}
    public bool IsAvailable{get;set;}=true;
    public List<Order> Orders{get;set;}=new();
}