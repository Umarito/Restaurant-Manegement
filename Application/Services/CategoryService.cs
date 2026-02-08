using System.Net;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WebApi.DTOs;

public class CategoryService(IMapper mapper,ICategoryRepository CategoryRepository) : ICategoryService
{
    private readonly ICategoryRepository _CategoryRepository = CategoryRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Response<string>> AddCategoryAsync(CategoryInsertDto CategoryInsertDto)
    {
        var Category = _mapper.Map<Category>(CategoryInsertDto);
        return await _CategoryRepository.AddCategoryAsync(Category);
    }

    public async Task<Response<string>> DeleteAsync(int CategoryId)
    {
        return await _CategoryRepository.DeleteAsync(CategoryId);
    }

    public async Task<PagedResult<CategoryGetDto>> GetAllCategoriesAsync(CategoryFilter filter, PagedQuery query)
    {
        var Categories = await _CategoryRepository.GetAllCategoriesAsync(filter,query);
        return _mapper.Map<PagedResult<CategoryGetDto>>(Categories);
    }

    public async Task<Response<CategoryGetDto>> GetCategoryByIdAsync(int CategoryId)
    {
        var Category = await _CategoryRepository.GetCategoryByIdAsync(CategoryId);
        return _mapper.Map<Response<CategoryGetDto>>(Category);
    }

    public async Task<Response<string>> UpdateAsync(int CategoryId,CategoryUpdateDto CategoryUpdateDto)
    {
        var Category = _mapper.Map<Category>(CategoryUpdateDto); 
        return await _CategoryRepository.UpdateAsync(CategoryId,Category);
    }
}