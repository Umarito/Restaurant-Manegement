using WebApi.DTOs;

public interface IMenuItemService
{
    Task<Response<string>> AddMenuItemAsync(MenuItemInsertDto MenuItemInsertDto);
    Task<Response<MenuItemGetDto>> GetMenuItemByIdAsync(int MenuItemId);
    Task<PagedResult<MenuItemGetDto>> GetAllMenuItemsAsync(MenuItemFilter filter, PagedQuery query);
    Task<Response<string>> DeleteAsync(int MenuItemId);
    Task<Response<string>> UpdateAsync(int MenuItemId,MenuItemUpdateDto MenuItemUpdateDto);
    Task<List<MenuItemGetWithCategoryDto>> GetMenuItemsByCategoryIdAsync(int categoryId);
}