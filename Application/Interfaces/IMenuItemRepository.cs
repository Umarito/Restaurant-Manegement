using WebApi.DTOs;

public interface IMenuItemRepository
{
    Task AddAsync(MenuItem MenuItem);
    Task<MenuItem?> GetByIdAsync(int id);
    Task DeleteAsync(int MenuItem);
    Task UpdateAsync(MenuItem MenuItem);
    Task<PagedResult<MenuItem>> GetAllMenuItemsAsync(MenuItemFilter filter, PagedQuery query);
    Task<List<MenuItem>> GetMenuItemsByCategoryIdAsync(int categoryId);
}