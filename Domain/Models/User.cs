public class User : BaseEntity
{
    public string FullName{get;set;}=null!;
    public string Phone{get;set;}=null!;
    public UserRoles Role{get;set;}
    public List<Order> OrdersAsWaiter{get;set;}=new();
}