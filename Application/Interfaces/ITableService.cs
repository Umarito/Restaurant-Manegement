using WebApi.DTOs;

public interface ITableService
{
    Task<Response<string>> AddTableAsync(TableInsertDto TableInsertDto);
    Task<PagedResult<TableGetDto>> GetAllTablesAsync(TableFilter filter, PagedQuery query);
    Task<Response<TableGetDto>> GetTableByIdAsync(int tableId);
    Task<Response<string>> DeleteAsync(int TableId);
    Task<Response<string>> UpdateAsync(int TableId,TableUpdateDto TableUpdateDto);
}