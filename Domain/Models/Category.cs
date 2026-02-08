public class Category : BaseEntity
{
    public string Name{get;set;}=null!;
    public List<MenuItem> MenuItems{get;set;}=new();
}