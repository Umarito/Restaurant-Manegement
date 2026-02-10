using WebApi.DTOs;

public interface ITableRepository
{
    Task AddAsync(Table Table);
    Task<Table?> GetByIdAsync(int id);
    Task DeleteAsync(int Table);
    Task UpdateAsync(Table Table);
    Task<PagedResult<Table>> GetAllTablesAsync(TableFilter filter, PagedQuery query);
}