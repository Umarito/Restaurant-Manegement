using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WebApi.DTOs;

public class MenuItemService(IMapper mapper,IMenuItemRepository MenuItemRepository) : IMenuItemService
{
    private readonly IMenuItemRepository _MenuItemRepository = MenuItemRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Response<string>> AddMenuItemAsync(MenuItemInsertDto MenuItemInsertDto)
    {
        var MenuItem = _mapper.Map<MenuItem>(MenuItemInsertDto);
        return await _MenuItemRepository.AddMenuItemAsync(MenuItem);
    }

    public async Task<Response<string>> DeleteAsync(int MenuItemId)
    {
        return await _MenuItemRepository.DeleteAsync(MenuItemId);
    }

    public async Task<PagedResult<MenuItemGetDto>> GetAllMenuItemsAsync(MenuItemFilter filter, PagedQuery query)
    {
        var MenuItems = await _MenuItemRepository.GetAllMenuItemsAsync(filter,query);
        return _mapper.Map<PagedResult<MenuItemGetDto>>(MenuItems);
    }

    public async Task<Response<MenuItemGetDto>> GetMenuItemByIdAsync(int MenuItemId)
    {
        var MenuItem = await _MenuItemRepository.GetMenuItemByIdAsync(MenuItemId);
        return _mapper.Map<Response<MenuItemGetDto>>(MenuItem);
    }

    public async Task<List<MenuItemGetWithCategoryDto>> GetMenuItemsByCategoryIdAsync(int categoryId)
    {
        var MenuItem = await _MenuItemRepository.GetMenuItemsByCategoryIdAsync(categoryId);
        return _mapper.Map<List<MenuItemGetWithCategoryDto>>(MenuItem);
    }

    public async Task<Response<string>> UpdateAsync(int MenuItemId,MenuItemUpdateDto MenuItemUpdateDto)
    {
        var MenuItem = _mapper.Map<MenuItem>(MenuItemUpdateDto); 
        return await _MenuItemRepository.UpdateAsync(MenuItemId,MenuItem);
    }
}