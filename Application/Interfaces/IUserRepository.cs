using WebApi.DTOs;

public interface IUserRepository
{
    Task<Response<string>> AddUserAsync(User User);
    Task<Response<User>> GetUserByIdAsync(int UserId);
    Task<PagedResult<User>> GetAllUsersAsync(UserFilter filter, PagedQuery query);
    Task<Response<string>> DeleteAsync(int UserId);
    Task<Response<string>> UpdateAsync(int UserId,User User);
    Task<List<User>> GetAllOrdersOfWaiterByIdAsync(int WaiterId);
}