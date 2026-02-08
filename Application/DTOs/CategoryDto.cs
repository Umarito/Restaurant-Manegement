public class CategoryInsertDto
{
    public string Name{get;set;}=null!;
}
public class CategoryUpdateDto
{
    public int Id{get;set;}
    public string Name{get;set;}=null!;
}
public class CategoryGetDto
{
    public int Id{get;set;}
    public string Name{get;set;}=null!;
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
}
public class CategoryGetWithMenuItemsDto
{
    public int Id{get;set;}
    public string Name{get;set;}=null!;
    public List<MenuItem> MenuItems{get;set;}=new();
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
}