public class TableInsertDto
{
    public int TableNumber{get;set;}
    public int Capacity{get;set;}
    public bool IsAvailable{get;set;}=true;
}
public class TableUpdateDto
{
    public int Id{get;set;}
    public int TableNumber{get;set;}
    public int Capacity{get;set;}
    public bool IsAvailable{get;set;}=true;
}
public class TableGetDto
{
    public int Id{get;set;}
    public int TableNumber{get;set;}
    public int Capacity{get;set;}
    public bool IsAvailable{get;set;}=true;
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
}