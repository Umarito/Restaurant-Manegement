using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WebApi.DTOs;

public class MenuItemService(IMapper mapper,IMenuItemRepository MenuItemRepository,ILogger<MenuItemService> logger) : IMenuItemService
{
    private readonly IMenuItemRepository _MenuItemRepository = MenuItemRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<MenuItemService> _logger = logger;

    public async Task<Response<string>> AddMenuItemAsync(MenuItemInsertDto MenuItemInsertDto)
    {
        try
        {
            var MenuItem = _mapper.Map<MenuItem>(MenuItemInsertDto);
            await _MenuItemRepository.AddAsync(MenuItem);
            return new Response<string>(HttpStatusCode.OK, "MenuItem was added successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int MenuItemId)
    {
        try
        {
            await _MenuItemRepository.DeleteAsync(MenuItemId);
            return new Response<string>(HttpStatusCode.OK, "MenuItem was deleted successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<MenuItemGetDto>> GetMenuItemByIdAsync(int MenuItemId)
    {
        try
        {
            var MenuItem = await _MenuItemRepository.GetByIdAsync(MenuItemId);
            var res = _mapper.Map<MenuItemGetDto>(MenuItem);
            return new Response<MenuItemGetDto>(HttpStatusCode.OK,"The data that you were searching for:",res);
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<MenuItemGetDto>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(int MenuItemId, MenuItemUpdateDto MenuItem)
    {
        try
        {
            var res = await _MenuItemRepository.GetByIdAsync(MenuItemId);

            if (res == null)
            {   
                return new Response<string>(HttpStatusCode.NotFound,"MenuItem not found");
            }
            else
            {
                _mapper.Map(MenuItem, res);
                await _MenuItemRepository.UpdateAsync(res);
                return new Response<string>(HttpStatusCode.OK,"MenuItem updated successfully");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }

    public async Task<List<MenuItemGetWithCategoryDto>> GetMenuItemsByCategoryIdAsync(int categoryId)
    {
        var MenuItem = await _MenuItemRepository.GetMenuItemsByCategoryIdAsync(categoryId);
        return _mapper.Map<List<MenuItemGetWithCategoryDto>>(MenuItem);
    }

    public async Task<PagedResult<MenuItemGetDto>> GetAllMenuItemsAsync(MenuItemFilter filter, PagedQuery query)
    {
        var MenuItems = await _MenuItemRepository.GetAllMenuItemsAsync(filter,query);
        return _mapper.Map<PagedResult<MenuItemGetDto>>(MenuItems);
    }
}