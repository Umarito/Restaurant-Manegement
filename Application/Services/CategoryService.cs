using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WebApi.DTOs;

public class CategoryService(IMapper mapper,ICategoryRepository CategoryRepository,ILogger<CategoryService> logger) : ICategoryService
{
    private readonly ICategoryRepository _CategoryRepository = CategoryRepository;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<CategoryService> _logger = logger;

    public async Task<Response<string>> AddCategoryAsync(CategoryInsertDto CategoryInsertDto)
    {
        try
        {
            var Category = _mapper.Map<Category>(CategoryInsertDto);
            await _CategoryRepository.AddAsync(Category);
            return new Response<string>(HttpStatusCode.OK, "Category was added successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> DeleteAsync(int CategoryId)
    {
        try
        {
            await _CategoryRepository.DeleteAsync(CategoryId);
            return new Response<string>(HttpStatusCode.OK, "Category was deleted successfully");
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<PagedResult<CategoryGetDto>> GetAllCategoriesAsync(CategoryFilter filter, PagedQuery query)
    {
        var Categories = await _CategoryRepository.GetAllCategoriesAsync(filter,query);
        return _mapper.Map<PagedResult<CategoryGetDto>>(Categories);
    }

    public async Task<Response<CategoryGetDto>> GetCategoryByIdAsync(int CategoryId)
    {
        try
        {
            var Category = await _CategoryRepository.GetByIdAsync(CategoryId);
            var res = _mapper.Map<CategoryGetDto>(Category);
            return new Response<CategoryGetDto>(HttpStatusCode.OK,"The data that you were searching for:",res);
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex.Message);
            return new Response<CategoryGetDto>(HttpStatusCode.InternalServerError, "Internal Server Error");
        }
    }

    public async Task<Response<string>> UpdateAsync(int categoryId, CategoryUpdateDto category)
    {
        try
        {
            var res = await _CategoryRepository.GetByIdAsync(categoryId);

            if (res == null)
            {   
                return new Response<string>(HttpStatusCode.NotFound,"Category not found");
            }
            else
            {
                _mapper.Map(category, res);
                await _CategoryRepository.UpdateAsync(res);
                return new Response<string>(HttpStatusCode.OK,"Category updated successfully");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new Response<string>(HttpStatusCode.InternalServerError,"Internal Server Error");
        }
    }
}