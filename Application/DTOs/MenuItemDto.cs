public class MenuItemInsertDto
{
    public string Name{get;set;}=null!;
    public decimal Price{get;set;}
    public bool IsAvailable{get;set;}=true;
    public int CategoryId{get;set;}
}
public class MenuItemUpdateDto
{
    public int Id{get;set;}
    public string Name{get;set;}=null!;
    public decimal Price{get;set;}
    public bool IsAvailable{get;set;}=true;
    public int CategoryId{get;set;}
}
public class MenuItemGetDto
{
    public int Id{get;set;}
    public string Name{get;set;}=null!;
    public decimal Price{get;set;}
    public bool IsAvailable{get;set;}=true;
    public int CategoryId{get;set;}
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
}
public class MenuItemGetWithCategoryDto
{
    public int Id{get;set;}
    public string Name{get;set;}=null!;
    public decimal Price{get;set;}
    public bool IsAvailable{get;set;}=true;
    public int CategoryId{get;set;}
    public Category? Category{get;set;}
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
}