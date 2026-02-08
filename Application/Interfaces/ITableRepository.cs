using WebApi.DTOs;

public interface ITableRepository
{
    Task<Response<string>> AddTableAsync(Table Table);
    Task<Response<Table>> GetTableByIdAsync(int TableId);
    Task<PagedResult<Table>> GetAllTablesAsync(TableFilter filter, PagedQuery query);
    Task<Response<string>> DeleteAsync(int TableId);
    Task<Response<string>> UpdateAsync(int TableId,Table Table);
}