using WebApi.DTOs;

public interface IMenuItemRepository
{
    Task<Response<string>> AddMenuItemAsync(MenuItem MenuItem);
    Task<Response<MenuItem>> GetMenuItemByIdAsync(int MenuItemId);
    Task<PagedResult<MenuItem>> GetAllMenuItemsAsync(MenuItemFilter filter, PagedQuery query);
    Task<Response<string>> DeleteAsync(int MenuItemId);
    Task<Response<string>> UpdateAsync(int MenuItemId,MenuItem MenuItem);
    Task<List<MenuItem>> GetMenuItemsByCategoryIdAsync(int categoryId);
}