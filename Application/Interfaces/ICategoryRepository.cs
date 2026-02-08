using WebApi.DTOs;

public interface ICategoryRepository
{
    Task<Response<string>> AddCategoryAsync(Category Category);
    Task<Response<Category>> GetCategoryByIdAsync(int CategoryId);
    Task<PagedResult<Category>> GetAllCategoriesAsync(CategoryFilter filter, PagedQuery query);
    Task<Response<string>> DeleteAsync(int CategoryId);
    Task<Response<string>> UpdateAsync(int CategoryId,Category Category);
}