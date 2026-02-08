public class UserInsertDto
{
    public string FullName{get;set;}=null!;
    public string Phone{get;set;}=null!;
    public UserRoles Role{get;set;}
}
public class UserUpdateDto
{
    public int Id{get;set;}
    public string FullName{get;set;}=null!;
    public string Phone{get;set;}=null!;
    public UserRoles Role{get;set;}
}
public class UserGetDto
{
    public int Id{get;set;}
    public string FullName{get;set;}=null!;
    public string Phone{get;set;}=null!;
    public UserRoles Role{get;set;}
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
}
public class UserGetAsWaiterDto
{
    public int Id{get;set;}
    public string FullName{get;set;}=null!;
    public string Phone{get;set;}=null!;
    public UserRoles Role{get;set;}
    public List<Order> OrdersAsWaiter{get;set;}=new();
    public DateTime CreatedAt{get;set;}=DateTime.UtcNow;
}