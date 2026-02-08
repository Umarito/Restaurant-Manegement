using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTOs;

[ApiController]
[Route("api/[controller]")]
public class MenuItemsController(IMenuItemService MenuItemService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<string>> AddMenuItemAsync(MenuItemInsertDto MenuItem)
    {
        return await MenuItemService.AddMenuItemAsync(MenuItem);
    }
    [HttpPut("{MenuItemId}")]
    public async Task<Response<string>> UpdateAsync(int MenuItemId,MenuItemUpdateDto MenuItem)
    {
        return await MenuItemService.UpdateAsync(MenuItemId,MenuItem);
    }
    [HttpDelete("{MenuItemId}")]
    public async Task<Response<string>> DeleteAsync(int MenuItemId)
    {
        return await MenuItemService.DeleteAsync(MenuItemId);
    }
    [HttpGet]
    public async Task<PagedResult<MenuItemGetDto>> GetAllMenuItems([FromQuery] MenuItemFilter filter, [FromQuery] PagedQuery pagedQuery)
    {
        return await MenuItemService.GetAllMenuItemsAsync(filter, pagedQuery);   
    }
    
    [HttpGet("{MenuItemId}")]
    public async Task<Response<MenuItemGetDto>> GetMenuItemByIdAsync(int MenuItemId)
    {
        return await MenuItemService.GetMenuItemByIdAsync(MenuItemId);
    }
}