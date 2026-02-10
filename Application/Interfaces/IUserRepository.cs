using WebApi.DTOs;

public interface IUserRepository
{
    Task AddAsync(User User);
    Task<User?> GetByIdAsync(int id);
    Task DeleteAsync(int User);
    Task UpdateAsync(User User);
    Task<PagedResult<User>> GetAllUsersAsync(UserFilter filter, PagedQuery query);
    Task<List<User>> GetAllOrdersOfWaiterByIdAsync(int WaiterId);
}